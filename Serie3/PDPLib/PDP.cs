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

        internal static IDatabase GetDB()
        {
            return new Database(ConnStringName);
        }
        
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

        //TODO getRolesOfUser(String userName)
        //public List<Role> getRolesOfUser(String username)
        //{
        //    using (IDatabase db = GetDB())
        //    {
        //        //User u = db.Single<User>("where username = @0", userName);
        //        List<Role> result = new List<Role>();

        //        Role r = db.Single<Role>("SELECT Role.*" +
        //                                 "FROM Role INNER JOIN UserAssignment" +
        //                                 "ON (Role.roleId = UserAssignment.roleId)" +
        //                                 "INNER JOIN [User]" +
        //                                 "ON ([User].userId = UserAssignment.userID)" +
        //                                 "WHERE [User].userId = @0",
        //                                 username);
        //        //
        //        r.juniorRolesList = db.FetchOneToMany
        //    }
        //}

        //public List<Role> iterateJuniorRoles(Role r)
        //{
        //    if r.juniorRolesList = null ;
        //}

        //TODO getPermissionsOfUser(String userName)
        public List<Permission> getPermissionsOfUser(String userName)
        {
            using (IDatabase db = GetDB())
            {
                Permission p = db.Single<Permission>("where permissionName = @0", permissionName);
                return db.Fetch<User>("SELECT UserAssignment.USER_ID" +
                                      "FROM UserAssignment INNER JOIN PermissionAssignment" +
                                      "ON (UserAssignment.ROLE_ID = PermissionAssignment.ROLE_ID)" +
                                      "WHERE PermissionAssignment = @0)",
                                      p.PermissionId);
            }
        }

        //TODO getActionsAllowedOfUserWithResource(String userName,String resource)
        public List<Action> getActionsAllowedOfUserWithResource(String userName,String resource)
        {
            using (IDatabase db = GetDB())
            {
                Permission p = db.Single<Permission>("where permissionName = @0", permissionName);
                return db.Fetch<User>("SELECT UserAssignment.USER_ID" +
                                      "FROM UserAssignment INNER JOIN PermissionAssignment" +
                                      "ON (UserAssignment.ROLE_ID = PermissionAssignment.ROLE_ID)" +
                                      "WHERE PermissionAssignment = @0)",
                                      p.PermissionId);
            }
        }

        //TODO isActionAllowedOfUserWithResource(String actionName, String userName, String resource)
        public Boolean isActionAllowedOfUserWithResource(String actionName, String userName, String resource)
        {
            using (IDatabase db = GetDB())
            {
                Permission p = db.Single<Permission>("where permissionName = @0", permissionName);
                return db.Fetch<User>("SELECT UserAssignment.USER_ID" +
                                      "FROM UserAssignment INNER JOIN PermissionAssignment" +
                                      "ON (UserAssignment.ROLE_ID = PermissionAssignment.ROLE_ID)" +
                                      "WHERE PermissionAssignment = @0)",
                                      p.PermissionId);
            }
        }

        public IList<User> GetUsers()
        {
            using (IDatabase db = GetDB())
            {
                return db.Fetch<User>();
            }
        }
    }
}
