using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDPLib.Models
{
    public class Permission
    {
        public int ResourceId { get; set; }
        public int ActionId { get; set; }

        public override string ToString()
        {
            return String.Format("ResourceId: {0}, ActionId: {1}", ResourceId, ActionId);
        }
    }
}