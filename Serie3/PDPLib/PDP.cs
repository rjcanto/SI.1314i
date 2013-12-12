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
        
        public List<User> getUsersWithPermission(String permissionName)
        {
            using (IDatabase db = GetDB())
            {
                return db.Fetch<User>("select * from User");
            }
            
        }

        public List<User> getUsersWithRole(String roleName)
        {
            return null;
        }

        public List<Role> getRolesOfUser(String userName)
        { 
            return null;
        }

        public List<Permission> getPermissionsOfUser(String userName)
        {
            return null;
        }

        public List<Action> getActionsAllowedOfUserWithResource(String userName,String resource)
        {
            return null;
        }

        public Boolean isActionAllowedOfUserWithResource(String actionName, String userName, String resource)
        {
            return false;
        }



    }
}
