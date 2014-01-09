using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PEPLib;
using PEPWebApp.Filters;

namespace PEPWebApp.Controllers
{
    [PEP("Local")]
    [InitializeSimpleMembership]
    public class ProtectedController : Controller
    {
        //
        // GET: /Protected/U/user

        public ActionResult U(string id)
        {
            ViewBag.Username = id;

            return View();
        }
    }
}
