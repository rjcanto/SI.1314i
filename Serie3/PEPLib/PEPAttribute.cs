using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PDPLib;
using PDPLib.Models;

namespace PEPLib
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PEPAttribute : AuthorizeAttribute
    {
        private readonly bool _caseSensitive;

        public PEPAttribute(string connectionName, bool caseSensitive = false)
        {
            PDP.ConnStringName = connectionName;
            _caseSensitive = caseSensitive;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Passar a conn string por parametro ao constructor
            var pdp = new PDP();

            string action = httpContext.Request.HttpMethod;
            string resource = httpContext.Request.Url.AbsolutePath;

            if (!_caseSensitive)
            {
                action = action.ToLower();
                resource = resource.ToLower();
            }

            var authorized = (
                                 from User u in pdp.getUsersWithPermission(action, resource)
                                 where u.UserName == httpContext.User.Identity.Name
                                 select u
                             );

            return authorized.Any() || base.AuthorizeCore(httpContext);
        }
    }
}
