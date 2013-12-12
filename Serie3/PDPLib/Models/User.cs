using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDPLib.Models
{
    public class User
    {
        public int UserId { get; set; }
        public String UserName { get; set; }
        public override String ToString()
        {
            return UserName;
        }
    }
}