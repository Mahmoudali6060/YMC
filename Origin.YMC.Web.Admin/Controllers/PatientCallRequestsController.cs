using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using Microsoft.AspNet.Identity.Owin;
using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.Patients;
using Origin.YMC.Business.Entities.Migrations;

namespace Origin.YMC.Web.Admin.Controllers
{
    public class PatientCallRequestsController : OriginBaseController
    {
        protected ApplicationUserManager _userManager;
        private readonly IPatientCallRequestsComponent _patientCallRequestsComponent;
        private readonly IPatientComponent _patientComponent;

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            protected set => _userManager = value;
        }
        public PatientCallRequestsController(IPatientCallRequestsComponent patientCallRequestsComponent, IPatientComponent patientComponent)
        {
            _patientCallRequestsComponent = patientCallRequestsComponent;
            _patientComponent = patientComponent;
        }

        // GET: PatientCallRequests
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
            var data = _patientCallRequestsComponent.GetListGetPageData(request.Search.Value, request.Start, request.Length, orderColumsName, orderColumsDirect);
            var response = DataTablesResponse.Create(request, data.TotalDatalenght, data.FilterDatalenght, data.Items);
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Get(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    var model = _patientCallRequestsComponent.Get(id);
                    model.Id = model.Id;
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                return new HttpStatusCodeResult(404, "not found this index");
            }
            catch (Exception ex)
            {

                return new HttpStatusCodeResult(404, ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult> Update(PatientCallRequestsViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                       var patientId= _patientCallRequestsComponent.Update(model);
                      
                       string Body = string.Format(
                           "Dear<br />Kindly be informed your request for call has been submitted<br />Scheduled Date is: {0},Time: {1}<br />Meeting url: {2} <br />Thanks",
                            model.MeetingDate.Value.ToShortDateString(), model.MeetingTime.Value,
                           model.ZoomUrl);

                       await UserManager.SendEmailAsync(
                       _patientComponent.GetUserId(patientId),
                       "Initiate call with doctor",Body);

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
    }
}