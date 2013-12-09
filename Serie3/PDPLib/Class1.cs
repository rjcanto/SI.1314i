using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDP.Models;
using Action=PDP.Models.Action;

namespace PDPLib
{
    public class Class1
    {
        public List<User> getUsersWithPermission(String permissionName)
        {
            return null;
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
