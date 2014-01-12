using System;
using System.Collections.Generic;
using NPoco;

namespace PDPLib.Models
{
    public class Role : IEquatable<Role>
    {
        private List<Permission> permissionList;

        public Role()
        {
            permissionList = new List<Permission>();
        }

        public int RoleId { get; set; }
        public String RoleName { get; set; }

        [Ignore]
        public List<Role> juniorRolesList { get; set; }

        public bool Equals(Role other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return RoleId == other.RoleId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Role) obj);
        }

        public override int GetHashCode()
        {
            return RoleId;
        }

        public override string ToString()
        {
            return RoleName;
        }
    }
}