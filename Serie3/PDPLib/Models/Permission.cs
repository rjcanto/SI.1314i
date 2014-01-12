using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDPLib.Models
{
    public class Permission : IEquatable<Permission>
    {
        public int ResourceId { get; set; }
        public int ActionId { get; set; }

        public override string ToString()
        {
            return String.Format("ResourceId: {0}, ActionId: {1}", ResourceId, ActionId);
        }

        public bool Equals(Permission other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ResourceId == other.ResourceId && ActionId == other.ActionId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Permission) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (ResourceId*397) ^ ActionId;
            }
        }
    }
}