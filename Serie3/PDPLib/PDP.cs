using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDPLib.Models;
using Action = PDPLib.Models.Action;
using NPoco;

namespace PDPLib
{
    public class PDP
    {
        public static string ConnStringName { get; set; }
        public static List<Role> listRoles = null;

        internal static Database GetDB()
        {
            return new Database(ConnStringName);
        }

        #region DB Queries

        public List<User> getUsersWithPermission(String actionName, String resourceName)
        {
            using (IDatabase db = GetDB())
            {
                Action act = db.SingleOrDefault<Action>("where actionname = @0", actionName);
                Resource res = db.SingleOrDefault<Resource>("where resourcename = @0", resourceName);
                return db.Fetch<User>("SELECT [User].* " +
                                      "FROM [User] INNER JOIN UserAssignment " +
                                      "ON ([User].userId = UserAssignment.userId) " +
                                      "INNER JOIN PermissionAssignment " +
                                      "ON (UserAssignment.roleId = PermissionAssignment.roleId) " +
                                      "WHERE (PermissionAssignment.actionId = @0 AND PermissionAssignment.resourceId = @1)",
                                      act != null ? (int?)act.ActionId : null,
                                      res != null ? (int?)res.ResourceId : null);
            }
        }

        public List<User> getUsersWithRole(String roleName)
        {
            using (IDatabase db = GetDB())
            {
                Role r = db.Single<Role>("where rolename = @0", roleName);
                return db.Fetch<User>("SELECT [User].*" +
                                      "FROM [User] INNER JOIN UserAssignment" +
                                      "ON ([User].userId = UserAssignment.userId)" +
                                      "WHERE UserAssignment.roleId = @0)",
                                      r.RoleId);
            }
        }

        public List<Role> getRolesOfUser(String userName)
        {
            using (IDatabase db = GetDB())
            {
                if (listRoles == null)
                    this.getRolesHierarchy(db);
                //User u = db.Single<User>("where username = @0", userName);
                List<Role> list = db.Fetch<Role>("SELECT Role.* " +
                                         "FROM Role INNER JOIN UserAssignment " +
                                         "ON (Role.roleId = UserAssignment.roleId) " +
                                         "INNER JOIN [User] " +
                                         "ON ([User].userId = UserAssignment.userID) " +
                                         "WHERE [User].username = @0;",
                                         userName);
                IEnumerable<Role> tmp = list;

                foreach (Role r in list)
                {
                    tmp = tmp.Concat<Role>(getJuniorRolesofRole(r));
                }

                return tmp.Distinct().ToList<Role>();

            }
        }

        public List<Permission> getPermissionsOfUser(String userName)
        {
            using (IDatabase db = GetDB())
            {
                List<Role> list = getRolesOfUser(userName);
                List<Permission> result = null;

                foreach (Role r in list)
                {
                    List<Permission> tmp = db.Fetch<Permission>("SELECT Permission.*"
                                        + " FROM Permission INNER JOIN PermissionAssignment"
                                        + " ON ((Permission.actionId = PermissionAssignment.actionId)"
                                        + " AND (Permission.resourceId = PermissionAssignment.resourceId))"
                                        + " INNER JOIN Role"
                                        + " ON (PermissionAssignment.roleId = Role.roleId)"
                                        + " INNER JOIN UserAssignment"
                                        + " ON (UserAssignment.roleId = Role.roleId)"
                                        + " INNER JOIN [User]"
                                        + " ON ([User].userId = UserAssignment.userId)"
                                        + " WHERE (Role.roleId = @0);",
                                          r.RoleId);
                    if (result == null)
                        result = tmp;
                    else result.AddRange(tmp);
                }
                return result;
            }
        }

        public IList<Action> getActionsAllowedOfUserWithResource(String userName, String resourceName)
        {
            return
                (
                    from Permission p in getPermissionsOfUser(userName)
                    join Action a in GetActions() on p.ActionId equals a.ActionId
                    select a
                ).ToList();
        }

        
        public IList<Resource> getResourcesOfAuthorizedUser(String userName, String actionName)
        {
            using (var db = GetDB())
            {
                db.OpenSharedConnection();
                db.KeepConnectionAlive = true;

                var resources = (
                                    from Permission p in getPermissionsOfUser(userName)
                                    join Resource r in GetResources() on p.ResourceId equals r.ResourceId
                                    select r
                                ).ToList();

                db.KeepConnectionAlive = false;
                db.CloseSharedConnection();
                return resources;
            }
        }

        public Boolean isActionAllowedOfUserWithResource(String actionName, String userName, String resourceName)
        {
            return
                ((
                    from Action a in getActionsAllowedOfUserWithResource(userName,resourceName)
                    where a.ActionName == actionName
                    select a
                    
                ).Count<Action>() != 0);
        }

        public IList<User> GetUsers()
        {
            using (IDatabase db = GetDB())
            {
                return db.Fetch<User>();
            }
        }

        public IList<Resource> GetResources()
        {
            using (IDatabase db = GetDB())
            {
                return db.Fetch<Resource>();
            }
        }

        public IList<Action> GetActions()
        {
            using (IDatabase db = GetDB())
            {
                return db.Fetch<Action>();
            }
        }

        #endregion

        #region Support Methods

        //Support method to retrieve Roles Hierarchy from database
        private void getRolesHierarchy(IDatabase db)
        {
            listRoles = db.Fetch<Role>("SELECT Role.* FROM Role");

            foreach (Role parentRole in listRoles)
                getRolesHierarchyRecursive(db, parentRole);

        }

        private void getRolesHierarchyRecursive(IDatabase db, Role parentRole)
        {
            if (parentRole == null)
                return;


            parentRole.juniorRolesList = db.Fetch<Role>(
                                "SELECT Role.* FROM Role "
                                + "INNER JOIN RoleHierarchy ON Role.roleId = RoleHierarchy.juniorRoleId "
                                + "WHERE RoleHierarchy.roleId = @0;",
                                parentRole.RoleId
            );

            foreach (Role juniorRole in parentRole.juniorRolesList)
                getRolesHierarchyRecursive(db, juniorRole);

        }

        // Podem surgir resultados em duplicado. Aplicar sobre o IEnumberable retornado Distict.
        private IEnumerable<Role> getJuniorRolesofRole(Role parentRole)
        {
            if (parentRole == null)
                yield break;

            Role parentRoleTmp = listRoles.Find(x => parentRole.RoleName == x.RoleName);

            foreach (Role r in parentRoleTmp.juniorRolesList)
            {
                yield return r;
                foreach (Role juniorRole in getJuniorRolesofRole(r))

               
                {
                    yield return juniorRole;
                }
                
            }
        }


        #endregion
    }
}