using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Origin.YMC.Web.Admin.Controllers
{
    /// <summary>
    ///  This is base controll as parent controller
    /// </summary>
    [Authorize(Roles ="Admin")]
    public class OriginBaseController : Controller
    {
        public OriginBaseController()
        {

        }

    }
}