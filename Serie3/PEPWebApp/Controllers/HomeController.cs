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

            return View();
        }

        public ActionResult Resource(string id)
        {
            ViewBag.Username = id;
            //ViewBag.Resources = new PDP().getPermissionsOfUser(WebSecurity.CurrentUserName).Select(p => p.)
            return (id == null) ? View("About") : View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Exemplo de uma página de acesso livre.";

            return View();
        }
    }
}
