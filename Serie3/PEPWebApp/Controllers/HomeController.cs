using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PDPLib;
using PDPLib.Models;
using PEPLib;
using WebMatrix.WebData;

namespace PEPWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "PEP - Policy Enforcement Point.";

            var pdp = new PDP();
            IList<User> users = pdp.GetUsers();
            var userRoles = new Dictionary<int, string>();

            foreach (var user in users)
            {
                List<Role> roles = pdp.getRolesOfUser(user.UserName);
                var sb = new StringBuilder();
                foreach (var role in roles)
                {
                    if (sb.Length != 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(role.RoleName);
                }
                userRoles[user.UserId] = sb.ToString();
            }

            ViewBag.Users = users;
            ViewBag.UserRoles = userRoles;
            return View();
        }

        public ActionResult Resources()
        {
            if (WebSecurity.IsAuthenticated)
            {
                ViewBag.Resources = new PDP().getResourcesOfAuthorizedUser(WebSecurity.CurrentUserName,
                                                                           HttpVerbs.Get.ToString());
            }
            return View();
        }

        [PolicyEnforcement]
        public ActionResult Resource(string id)
        {
            ViewBag.Username = id;
            
            // Get user page if user exists
            var users = new PDP().GetUsers();
            if (!users.Select(u => u.UserName).Contains(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Exemplo de uma página de acesso livre.";

            return View();
        }

        public ActionResult Chatroom()
        {
            string chatServiceHost;
#if DEBUG
            chatServiceHost = "https://nodejslab-c9-lbras.c9.io/";
#else
            chatServiceHost = "https://chatroom-js.herokuapp.com/";
#endif
            ViewBag.Host = chatServiceHost;
            return View();
        }
    }
}
