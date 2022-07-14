using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.Testimonials.ViewModels;
using Origin.YMC.Web.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace Origin.OGS.Web.Admin.Controllers
{
    public class TestimonialController : OriginBaseController
    {
        private readonly ITestimonialComponent _testimonialComponent;

        public TestimonialController(ITestimonialComponent testimonialComponent)
        {
            _testimonialComponent = testimonialComponent;
        }

        // GET: Testimonial
        public ActionResult Index()
        {
            return View();
        }

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
            var data = _testimonialComponent.GetPageData(request.Search.Value, request.Start, request.Length, orderColumsName, orderColumsDirect);
            var response = DataTablesResponse.Create(request, data.TotalDatalenght, data.FilterDatalenght, data.Items);
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Deactive_Activate(Guid id)
        {
            try
            {
                if (id != null && id != Guid.Empty)
                {
                    _testimonialComponent.Deactive_Activate(id);
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