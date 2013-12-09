using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ArrayList;

namespace PDP.Models
{
    public class Role
    {
        
        private List<Role> juniorRolesList;
        private List<Permission> permissionList;

        public Role()
        {
            juniorRolesList = new List<Role>();
            permissionList = new List<Permission>();
        }

        public int RoleId { get; set; }
        public String RoleName { get; set; }
    }
}