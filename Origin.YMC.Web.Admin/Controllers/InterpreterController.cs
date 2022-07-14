using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using Microsoft.AspNet.Identity.Owin;
using Origin.YMC.Business.Components.Interfaces;

namespace Origin.YMC.Web.Admin.Controllers
{
    public class InterpreterController : OriginBaseController
    {
        protected ApplicationUserManager _userManager;
        private readonly IInterpreterComponent _interpreterComponent;

        public InterpreterController( IInterpreterComponent interpreterComponent)
        {
            _interpreterComponent = interpreterComponent;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            protected set
            {
                _userManager = value;
            }
        }
        // GET: Interpreter
        public ActionResult Index()
        {
            return View();
        }

        // GET: Patient

        [HttpPost]
        public ActionResult PageData(IDataTablesRequest request)
        {
            var orderColums = request.Columns.FirstOrDefault(x => x.Sort != null);
            var orderColumsName = string.Empty;
            var orderColumsDirect = (int)DataTables.AspNet.Core.SortDirection.Ascending;
            if (orderColums != null)
            {
                orderColumsName = orderColums.Name;
                orderColumsDirect = (int)orderColums.Sort.Direction;
            }
            var data = _interpreterComponent.GetPageData(request.Search.Value, request.Start, request.Length, orderColumsName, orderColumsDirect);
            var response = DataTablesResponse.Create(request, data.TotalDatalenght, data.FilterDatalenght, data.Items);
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DeActive_Activate(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    _interpreterComponent.ActivateDeactivateInterpreter(id);
                    return Json("ok", JsonRequestBehavior.AllowGet);
                }
                return new HttpStatusCodeResult(404, "not found this index");
            }
            catch (Exception ex)
            {

                return new HttpStatusCodeResult(404, ex.Message);
            }

        }
    }
}