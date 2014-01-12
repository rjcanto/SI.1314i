using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PDPLib;
using PDPLib.Models;

namespace PEPLib
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class PolicyEnforcementAttribute : AuthorizeAttribute
    {
        private readonly bool _caseSensitive;

        public static string ConnStringName
        {
            get { return PDP.ConnStringName; }
            set { PDP.ConnStringName = value; }
        }

        public PolicyEnforcementAttribute(string connectionName, bool caseSensitive = false) : this(caseSensitive)
        {
            ConnStringName = connectionName;
        }

        public PolicyEnforcementAttribute(bool caseSensitive = false)
        {
            _caseSensitive = caseSensitive;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var pdp = new PDP();

            string action = httpContext.Request.HttpMethod;
            string username = httpContext.User.Identity.Name;
            string resource = httpContext.Request.Url.AbsolutePath;



            if (!_caseSensitive)
            {
                action = action.ToLower();
                resource = resource.ToLower();
            }

            try
            {
                return pdp.IsUserAuthorized(username, action, resource);
            }
            catch (Exception e)
            {
                if (!(e is ActionNotFoundException) && !(e is ResourceNotFoundException))
                {
                    throw;
                }

                // Action or resource unknown to the policy repository. Forward to default AuthorizeAttribute implementation.
                System.Diagnostics.Debug.WriteLine(e.Message);
                return base.AuthorizeCore(httpContext);
            }

            
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new HttpStatusCodeResult((int)HttpStatusCode.Forbidden);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
