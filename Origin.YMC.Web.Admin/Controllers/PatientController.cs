using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebSockets;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Origin.YMC.Web.Admin.Models;
using Origin.YMC.Business.Entities.Domain;
using Origin.YMC.Business.Entities;
using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.Patients.ViewModel;
using System.IO;
using PagedList;
using System.Collections.Generic;
using System.Net;
using System.Globalization;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;


namespace Origin.YMC.Web.Admin.Controllers
{
    public class PatientController : OriginBaseController
    {
        protected ApplicationUserManager _userManager;
        private readonly IUserComponent _userComponent;
        private readonly ICountryComponent _countryComponent;
        private readonly ICityComponent _cityComponent;
        private readonly IPatientComponent _patientComponent;
        private readonly IPaymentComponent _paymentComponent;
        private readonly ILogComponent _logComponent;

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

        public PatientController(IUserComponent userComponent
                             , ICountryComponent countryComponent
                             , ICityComponent cityComponent
                             , IPatientComponent patientComponent
                             , IPaymentComponent paymentComponent
                             , ILogComponent logComponent)
        {
            _userComponent = userComponent;
            _countryComponent = countryComponent;
            _cityComponent = cityComponent;
            _patientComponent = patientComponent;
            _paymentComponent = paymentComponent;
            _logComponent = logComponent;
        }

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
            var data = _patientComponent.GetPageData(request.Search.Value, request.Start, request.Length, orderColumsName, orderColumsDirect);
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
                    _patientComponent.ActivateDeactivatePatient(id);
                    return Json("ok", JsonRequestBehavior.AllowGet);
                }
                return new HttpStatusCodeResult(404, "not found this index");
            }
            catch (Exception ex)
            {

                return new HttpStatusCodeResult(404, ex.Message);
            }

        }

        [Authorize]
        public ActionResult PatientProfile(Guid ProfileId)
        {
            var patient = _patientComponent.GetPatientProfileId(ProfileId);
            return View(patient);
        }
    }
}