using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDPLib
{
    public class ActionNotFoundException : KeyNotFoundException
    {
        public ActionNotFoundException() : this(String.Empty)
        {}

        public ActionNotFoundException(string message)
            : base(String.Format("The specified action was not found on the policy repository.{0}{1}",
                                 message == String.Empty ? String.Empty : " Details: ",
                                 message))
        {}
    }

    public class ResourceNotFoundException : KeyNotFoundException
    {
        public ResourceNotFoundException() : this(String.Empty)
        {}

        public ResourceNotFoundException(string message)
            : base(String.Format("The specified resource was not found on the policy repository.{0}{1}",
                                 message == String.Empty ? String.Empty : " Details: ",
                                 message))
        {}
    }
}
