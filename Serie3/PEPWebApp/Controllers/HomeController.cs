using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PDPLib;
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

        public ActionResult Resource(string id)
        {
            ViewBag.Username = id;
            if (WebSecurity.CurrentUserName != String.Empty)
            {
                ViewBag.Resources = new PDP().getResourcesOfAuthorizedUser(WebSecurity.CurrentUserName,
                                                                           HttpVerbs.Get.ToString());
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
