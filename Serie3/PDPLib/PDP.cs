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

        public static IDatabase GetDB()
        {
            return new Database(ConnStringName);
        }

        public List<User> getUsersWithPermission(String actionName, String resourceName)
        {
            using (IDatabase db = GetDB())
            {
                Action act = db.Single<Action>("where actionname = @0", actionName);
                Resource res = db.Single<Resource>("where resourcename = @0", resourceName);
                return db.Fetch<User>("SELECT User.*" +
                                      "FROM User INNER JOIN UserAssignment" +
                                      "ON ([User].userId = UserAssignment.userId)" +
                                      "INNER JOIN PermissionAssignment" +
                                      "ON (UserAssignment.roleId = PermissionAssignment.roleId)" +
                                      "WHERE (PermissionAssignment.actionId = @0 AND PermissionAssignment.resourceId = @1)",
                                      act.ActionId, res.ResourceId);
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
        // Version 0.1 (does not iterate through the role hierarchy)
        public List<Role> getRolesOfUser(String username)
        {
            using (IDatabase db = GetDB())
            {
                //User u = db.Single<User>("where username = @0", userName);
                return db.Fetch<Role>("SELECT Role.*" +
                                         "FROM Role INNER JOIN UserAssignment" +
                                         "ON (Role.roleId = UserAssignment.roleId)" +
                                         "INNER JOIN [User]" +
                                         "ON ([User].userId = UserAssignment.userID)" +
                                         "WHERE [User].userId = @0",
                                         username);
                
            }
        }


        //TODO getPermissionsOfUser(String userName)
        //Version 0.1 (does not iterate through the role hierarchy)
        public List<Permission> getPermissionsOfUser(String userName)
        {
            using (IDatabase db = GetDB())
            {
                return db.Fetch<Permission>("SELECT Permission.*"
                                    + "FROM Permission INNER JOIN PermissionAssignment"
                                    + "ON ((Permission.actionId = PermissionAssignment.actionId)"
                                    + "	AND (Permission.resourceId = PermissionAssignment.resourceId))"
                                    + "INNER JOIN Role"
                                    + "ON (PermissionAssignment.roleId = Role.roleId)"
                                    + "INNER JOIN UserAssignment"
                                    + "ON (UserAssignment.roleId = Role.roleId)"
                                    + "INNER JOIN [User]"
                                    + "ON ([User].userId = UserAssignment.userId)"
                                    + "WHERE ([User].username = @0);",
                                      userName);
            }
        }

        //TODO getActionsAllowedOfUserWithResource(String userName,String resource)
        // Version 0.1 (does not iterate through the role hierarchy)
        public List<Action> getActionsAllowedOfUserWithResource(String userName, String resourceName)
        {
            using (IDatabase db = GetDB())
            {
                User usr = db.Single<User>("where userName = @0", userName);
                Resource res = db.Single<Resource>("where resourceName = @0", resourceName);
                return db.Fetch<Action>("SELECT Action.*" +
                                        "FROM Action INNER JOIN PermissionAssignment" +
                                        "ON (Action.actionId = PermissionAssignment.actionId)" +
                                        "INNER JOIN Role" +
                                        "ON (PermissionAssignment.roleId = Role.roleId)" +
                                        "INNER JOIN UserAssignment" +
                                        "ON (UserAssignment.roleId = Role.roleId)" +
                                        "INNER JOIN [User]" +
                                        "ON ([User].userId = UserAssignment.userId)" +
                                        "WHERE (PermissionAssignment.resourceId = @0" +
                                        "AND [User].username = @1)",
                                        res.ResourceId, usr.UserId);
            }
        }

        //TODO isActionAllowedOfUserWithResource(String actionName, String userName, String resource)
        // Version 0.1 (does not iterate through the role hierarchy)
        public Boolean isActionAllowedOfUserWithResource(String actionName, String userName, String resourceName)
        {
            using (IDatabase db = GetDB())
            {
                Resource res = db.Single<Resource>("where resourceName = @0", resourceName);
                Action act = db.Single<Action>("where actionName = @0", actionName);

                User usr = db.Single<User>("SELECT [User].*" +
                                      "FROM [User] INNER JOIN UserAssignment!" +
                                      "ON ([User].userId = UserAssignment.userId)" +
                                      "INNER JOIN Role" +
                                      "ON (UserAssignment.roleId = Role.roleId)" +
                                      "INNER JOIN PermissionAssignment" +
                                      "ON (PermissionAssignment.roleId = Role.roleId)" +
                                      "WHERE (PermissionAssignment.resourceId = @0" +
                                      "AND PermissionAssignment.actionId = @1" +
                                      "AND [User].username = @2)",
                                      res.ResourceId, act.ActionId, userName);
                return (usr != null);
            }
        }

    }
}
