using Microsoft.AspNet.Identity.Owin;
using Origin.YMC.Business.Components.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Origin.YMC.Business.Entities.Domain.Patients.ViewModel;
using Origin.YMC.Business.Entities.Domain.Payments.ViewModel;
using Origin.YMC.Business.Entities.Domain;
using Origin.YMC.Business.Entities.Domain.Doctors.ViewModel;
using System.IO;
using Origin.YMC.Business.Components.Implementation;
using Origin.YMC.Business.Components.ViewModels;
using System.Globalization;
using System.Net;

namespace Origin.YMC.Web.Client.Controllers
{
    public class HomeController : BaseController
    {
        protected ApplicationUserManager _userManager;
        private readonly IUserComponent _userComponent;
        private readonly ICountryComponent _countryComponent;
        private readonly ICityComponent _cityComponent;
        private readonly IPatientComponent _patientComponent;
        private readonly IDoctorComponent _doctorComponent;
        private readonly IPaymentComponent _paymentComponent;
        private readonly ILogComponent _logComponent;
        private readonly IAttachmentComponent _attachmentComponent;
        private readonly ISocialComponent _socialComponent;
        private readonly IPartationComponent _partationComponent;
        private readonly ITestimonialComponent _testimonialComponent;
        private readonly ISpecialtyComponent _specialtyComponent;
        private readonly IStatisticsComponent _statisticsComponent;


        public HomeController(IUserComponent userComponent
                             , ICountryComponent countryComponent
                             , ICityComponent cityComponent
                             , IPatientComponent patientComponent
                             , IDoctorComponent doctorComponent
                             , IPaymentComponent paymentComponent
                             , IAttachmentComponent attachmentComponent
                             , ILogComponent logComponent
                             , ISocialComponent socialComponent
                             , IPartationComponent partationComponent
                             , ITestimonialComponent testimonialComponent
                             , ISpecialtyComponent specialtyComponent, IStatisticsComponent statisticsComponent) : base(attachmentComponent)
        {
            _userComponent = userComponent;
            _countryComponent = countryComponent;
            _cityComponent = cityComponent;
            _patientComponent = patientComponent;
            _doctorComponent = doctorComponent;
            _paymentComponent = paymentComponent;
            _logComponent = logComponent;
            _attachmentComponent = attachmentComponent;
            _socialComponent = socialComponent;
            _partationComponent = partationComponent;
            _testimonialComponent = testimonialComponent;
            _specialtyComponent = specialtyComponent;
            _statisticsComponent = statisticsComponent;
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

        public ActionResult Index()
        {
            ViewBag._whyYMCPartation = _partationComponent.GetByPartationTypeName("WHY YMC");
            ViewBag._getYOURSECONDOPINIONREPORTPartation = _partationComponent.GetByPartationTypeName("GET YOUR SECOND OPINION REPORT");
            ViewBag._payANDSENDYOURQUESTIONSANDDOCUMENTSPartation = _partationComponent.GetByPartationTypeName("PAY AND SEND YOUR QUESTIONS AND DOCUMENTS");
            ViewBag._findADOCTORPartation = _partationComponent.GetByPartationTypeName("FIND A DOCTOR");
            ViewBag._howITWORKSPartation = _partationComponent.GetByPartationTypeName("HOW IT WORKS");
            ViewBag._doctors = _doctorComponent.List(string.Empty).ToList();
            ViewBag._testimonials = _testimonialComponent.GetList(string.Empty);
            ViewBag._specialties = _specialtyComponent.GetList(string.Empty).ToList();
            ViewBag.Facts = _statisticsComponent.GetAllFacts();


            return View();
        }

        [Authorize]
        public ActionResult DoctorProfileSteps()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    List<SelectListItem> countries = new List<SelectListItem>();
                    countries.Add(new SelectListItem() { Value = string.Empty, Text = $"-- {Resources.GlobalStrings.Select} --" });
                    countries.AddRange(_countryComponent.GetAll().Select(c => new SelectListItem() { Value = c.Code, Text = c.Name }).ToList());
                    ViewBag.countries = countries;
                    ViewBag.userEmail = _userComponent.GetByUsername(User.Identity.GetUserName())?.Email;
                    ViewBag._specialties = _specialtyComponent.GetList(null);

                    return View();
                }
                else
                {
                    return Redirect("/Home/Index");
                }
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        public ActionResult PatientProfileSteps()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    List<SelectListItem> countries = new List<SelectListItem>();
                    countries.Add(new SelectListItem() { Value = string.Empty, Text = $"-- {Resources.GlobalStrings.Select} --" });
                    countries.AddRange(_countryComponent.GetAll().Select(c => new SelectListItem() { Value = c.Code, Text = c.Name }).ToList());
                    ViewBag.countries = countries;
                    ViewBag.userEmail = _userComponent.GetByUsername(User.Identity.GetUserName())?.Email;
                    ViewBag._specialties = _specialtyComponent.GetList(null);

                    return View();
                }
                else
                {
                    return Redirect("/Home/Index");
                }
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Redirect("/Home/Index");
            }

        }

        [Authorize]
        [HttpPost]
        public ActionResult PatientProfileSteps(PatientViewModels patientViewModel)
        {
            try
            {
                patientViewModel.PaymentInfoExpirationDate = DateTime.ParseExact((patientViewModel.PaymentInfoExpirationMonth.Length == 1 ? "0" + patientViewModel.PaymentInfoExpirationMonth : patientViewModel.PaymentInfoExpirationMonth) + "/" + patientViewModel.PaymentInfoExpirationYear, "MM/yy", CultureInfo.InvariantCulture);

                var ctrUser = UserManager.FindByName(User.Identity.GetUserName());
                ctrUser.FirstName = patientViewModel.FirstName;
                ctrUser.LastName = patientViewModel.LastName;
                ctrUser.Address1 = patientViewModel.Address1;
                ctrUser.HeardAboutUsId = patientViewModel.HeardAboutUsId;
                ctrUser.GenderEnum = (GenderEnum)patientViewModel.GenderId;
                ctrUser.HeardAboutUsId = patientViewModel.HeardAboutUsId;
                ctrUser.SecurityQuestionId = patientViewModel.SecurityQuestionId;
                ctrUser.SecurityQuestionAnswer = patientViewModel.SecurityQuestionAnswer;
                ctrUser.NonUsStateOrProvince = patientViewModel.NonUsStateOrProvince;
                ctrUser.CityId = patientViewModel.CityId;
                ctrUser.Phone = patientViewModel.Phone;
                ctrUser.Address2 = patientViewModel.Address2;
                ctrUser.ZipPostalCode = patientViewModel.ZipPostalCode;
                ctrUser.Mobile = patientViewModel.MobilePhone;
                //ctrUser.BirthDate = DateTime.ParseExact($"{patientViewModel.Day}/{patientViewModel.Month}/{patientViewModel.Year}", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ctrUser.BirthDate = patientViewModel.BirthDate;

                var result = UserManager.Update(ctrUser);
                if (result.Succeeded)
                {
                    var paymentId = _paymentComponent.Add(new PaymentViewModel
                    {
                        Address1 = patientViewModel.PaymentInfoAddress1,
                        Address2 = patientViewModel.PaymentInfoAddress2,
                        CardholderFullName = patientViewModel.PaymentInfoCardholderFullName,
                        CreditCardNumber = patientViewModel.PaymentInfoCreditCardNumber,
                        CreditCardType = patientViewModel.PaymentInfoCreditCardType,
                        CVV = patientViewModel.PaymentInfoCVV,
                        ExpirationDate = patientViewModel.PaymentInfoExpirationDate,
                        State = patientViewModel.PaymentInfoState,
                        City = patientViewModel.PaymentInfoCity,
                        Country = patientViewModel.PaymentInfoCountry,
                    });

                    _patientComponent.Add(new PatientViewModels
                    {
                        PaymentId = paymentId ?? Guid.Empty,
                        ApplicationUserId = ctrUser.Id
                    });
                }

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetCities(string countryCode)
        {
            List<SelectListItem> CityNames = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(countryCode))
            {
                CityNames = _cityComponent.GetByCode(countryCode).Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();
            }
            return Json(CityNames, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DoctorProfileSteps(DoctorViewModels doctorViewModels, string Refrances, string Experinces, string AttachmentCertificates, string OnlineProfileLink)
        {
            try
            {
                var ctrUser = UserManager.FindByName(User.Identity.GetUserName());

                var spId = Guid.Parse(ctrUser.FirstName);
                doctorViewModels.PaymentInfoExpirationDate = DateTime.ParseExact((doctorViewModels.PaymentInfoExpirationMonth.Length == 1 ? "0" + doctorViewModels.PaymentInfoExpirationMonth : doctorViewModels.PaymentInfoExpirationMonth) + "/" + doctorViewModels.PaymentInfoExpirationYear, "MM/yy", CultureInfo.InvariantCulture);

                ctrUser.FirstName = doctorViewModels.FirstName;
                ctrUser.LastName = doctorViewModels.LastName;
                ctrUser.Address1 = doctorViewModels.Address1;
                ctrUser.Address2 = doctorViewModels.Address2;
                ctrUser.HeardAboutUsId = doctorViewModels.HeardAboutUsId;
                ctrUser.GenderEnum = (GenderEnum)doctorViewModels.GenderId;
                ctrUser.HeardAboutUsId = doctorViewModels.HeardAboutUsId;
                ctrUser.SecurityQuestionId = doctorViewModels.SecurityQuestionId;
                ctrUser.SecurityQuestionAnswer = doctorViewModels.SecurityQuestionAnswer;
                ctrUser.NonUsStateOrProvince = doctorViewModels.NonUsStateOrProvince;
                ctrUser.CityId = doctorViewModels.CityId;
                ctrUser.Phone = doctorViewModels.Phone;
                ctrUser.Mobile = doctorViewModels.MobilePhone;
                ctrUser.ZipPostalCode = doctorViewModels.ZipPostalCode;
                DateTime dateOfBirth = DateTime.MinValue;
                ctrUser.BirthDate =  doctorViewModels.Birthdate ;


                var result = UserManager.Update(ctrUser);
                if (result.Succeeded)
                {


                    var paymentId = _paymentComponent.Add(new PaymentViewModel
                    {
                        Address1 = doctorViewModels.PaymentInfoAddress1,
                        Address2 = doctorViewModels.PaymentInfoAddress2,
                        CardholderFullName = doctorViewModels.PaymentInfoCardholderFullName,
                        CreditCardNumber = doctorViewModels.PaymentInfoCreditCardNumber,
                        CreditCardType = doctorViewModels.PaymentInfoCreditCardType,
                        CVV = doctorViewModels.PaymentInfoCVV,
                        ExpirationDate = doctorViewModels.PaymentInfoExpirationDate,
                        State = doctorViewModels.PaymentInfoState,
                        City = doctorViewModels.PaymentInfoCity,
                        Country = doctorViewModels.PaymentInfoCountry,
                    });

                    var ownerId = _doctorComponent.AddThenGet(new DoctorViewModels
                    {
                        PaymentId = paymentId ?? Guid.Empty,
                        ApplicationUserId = ctrUser.Id,
                        SpecialtyId = spId,
                        Refrances = Refrances,
                        Experinces = Experinces,
                        OnlineProfileLink = OnlineProfileLink
                    });

                    if (!string.IsNullOrEmpty(AttachmentCertificates))
                    {
                        var certs = AttachmentCertificates.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < certs.Length; i++)
                        {
                            var AttachmentPath = certs[i];
                            if (AttachmentPath != null)
                            {

                                //4 - Certificate 
                                AddAttachment(AttachmentPath, ownerId, (int)AttachmentTypeEnum.Certifications);

                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public string uploadcertificate()
        {
            try
            {
                return UploadFile(Request.Files[0], (int)AttachmentTypeEnum.Certifications);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return ex.Message;
            }


        }

        [HttpGet]
        public ActionResult GetContactInfo()
        {

            try
            {
                var result = _socialComponent.GetList(string.Empty);
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }


        }

        [HttpGet]
        public ActionResult Search(string specialtyName, string price)
        {
            if (!string.IsNullOrWhiteSpace(specialtyName))
            {
                price = string.IsNullOrWhiteSpace(price) ? $"0_{int.MaxValue}" : price;
                if (price.Split('_').Length > 1)
                {
                    List<DoctorViewModels> doctors = _doctorComponent.Search(specialtyName, price).ToList();
                    ViewBag._specialtyKey = specialtyName;
                    ViewBag._specialties = _specialtyComponent.GetList(string.Empty).ToList();

                    ViewBag.doctors = doctors;
                    return View();
                }
                else
                    return Redirect("/Home");
            }
            else
                return Redirect("/Home");
        }

        public ActionResult AboutUs()
        {
            ViewBag._aboutUsPartation = _partationComponent.GetByPartationTypeName("ABOUT US");
            ViewBag._testimonials = _testimonialComponent.GetList(string.Empty).ToList();
            ViewBag._specialties = _specialtyComponent.GetList(string.Empty).ToList();
            return View();
        }

        public ActionResult Howitworks()
        {
            ViewBag._getYOURSECONDOPINIONREPORTPartation = _partationComponent.GetByPartationTypeName("GET YOUR SECOND OPINION REPORT");
            ViewBag._payANDSENDYOURQUESTIONSANDDOCUMENTSPartation = _partationComponent.GetByPartationTypeName("PAY AND SEND YOUR QUESTIONS AND DOCUMENTS");
            ViewBag._findADOCTORPartation = _partationComponent.GetByPartationTypeName("FIND A DOCTOR");
            ViewBag._howITWORKSPartation = _partationComponent.GetByPartationTypeName("HOW IT WORKS");
            ViewBag._testimonials = _testimonialComponent.GetList(string.Empty).ToList();
            ViewBag._specialties = _specialtyComponent.GetList(string.Empty).ToList();
            return View();
        }

        public ActionResult help()
        {
            ViewBag._getYOURSECONDOPINIONREPORTPartation = _partationComponent.GetByPartationTypeName("GET YOUR SECOND OPINION REPORT");
            ViewBag._payANDSENDYOURQUESTIONSANDDOCUMENTSPartation = _partationComponent.GetByPartationTypeName("PAY AND SEND YOUR QUESTIONS AND DOCUMENTS");
            ViewBag._findADOCTORPartation = _partationComponent.GetByPartationTypeName("FIND A DOCTOR");
            ViewBag._howITWORKSPartation = _partationComponent.GetByPartationTypeName("HOW IT WORKS");
            ViewBag._testimonials = _testimonialComponent.GetList(string.Empty).ToList();
            ViewBag._specialties = _specialtyComponent.GetList(string.Empty).ToList();
            return View();
        }
    }
}