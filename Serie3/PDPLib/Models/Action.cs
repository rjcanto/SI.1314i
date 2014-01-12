using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDPLib.Models
{
    public class Action : IEquatable<Action>
    {
        public int ActionId { get; set; }
        public String ActionName { get; set; }

        public override string ToString()
        {
            return ActionName;
        }

        public bool Equals(Action other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ActionId == other.ActionId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Action) obj);
        }

        public override int GetHashCode()
        {
            return ActionId;
        }
    }
}