using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PDPLib;
using PEPLib;
using WebMatrix.WebData;

namespace PEPWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "PEP - Policy Enforcement Point.";
            ViewBag.Users = new PDP().GetUsers();
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
    }
}
