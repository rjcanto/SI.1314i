using System;
using System.Collections.Generic;

namespace PDPLib.Models
{
    public class Role
    {
        private List<Permission> permissionList;

        public Role()
        {
            permissionList = new List<Permission>();
        }

        public int RoleId { get; set; }
        public String RoleName { get; set; }
        public List<Role> juniorRolesList { get; set; }
    }
}