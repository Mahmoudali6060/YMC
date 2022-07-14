using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Origin.YMC.Web.Admin.Controllers
{
    public class HomeController : OriginBaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index","Patient");
        }

       
    }
}