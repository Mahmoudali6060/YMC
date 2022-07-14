using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.SocialUrl.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Origin.YMC.Web.Admin.Controllers
{
    public class SocialController : OriginBaseController
    {
        private readonly ISocialComponent _socialComponent;

        public SocialController(ISocialComponent socialComponent)
        {
            _socialComponent = socialComponent;
        }

        // GET: Mission
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SocialViewModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id != null && model.Id != Guid.Empty)
                        _socialComponent.Update(model);
                    else
                        _socialComponent.Create(model);

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpStatusCodeResult(404, $"model is not valid ,Resone {string.Join("-", ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList())}");
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(404, $"model is not valid ,Resone {ex.Message}");
            }

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
            var data = _socialComponent.GetPageData(request.Search.Value, request.Start, request.Length, orderColumsName, orderColumsDirect);
            var response = DataTablesResponse.Create(request, data.TotalDatalenght, data.FilterDatalenght, data.Items);
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get(Guid id)
        {
            try
            {
                if (id != null && id != Guid.Empty)
                {
                    var mdel = _socialComponent.Get(id);
                    return Json(mdel, JsonRequestBehavior.AllowGet);
                }
                return new HttpStatusCodeResult(404, "not found this index");
            }
            catch (Exception ex)
            {

                return new HttpStatusCodeResult(404, ex.Message);
            }

        }

        [HttpGet]
        public ActionResult Deactive_Activate(Guid id)
        {
            try
            {
                if (id != null && id != Guid.Empty)
                {
                    _socialComponent.Deactive_Activate(id);
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