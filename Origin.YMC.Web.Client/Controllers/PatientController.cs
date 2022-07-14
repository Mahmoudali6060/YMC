using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Components.ViewModels;
using Origin.YMC.Business.Entities.Domain;
using Origin.YMC.Business.Entities.Domain.Interpreter.Enums;
using Origin.YMC.Business.Entities.Domain.Patients;
using Origin.YMC.Business.Entities.Domain.Patients.ViewModel;
using Origin.YMC.Business.Entities.Domain.Payments.ViewModel;
using Origin.YMC.Business.Entities.Domain.Testimonials;
using Origin.YMC.Core.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Origin.YMC.Web.Client.Controllers
{
    public class PatientController : BaseController
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
        private readonly ICaseComponent _caseComponent;
        private readonly ICaseQuestionsComponent _caseQuestionsComponent;
        private readonly IIdentityMessageService _IdentityMessageService;
        private readonly ITestimonialComponent _testimonialComponent;
        private readonly ISpecialtyComponent _specialtyComponent;
        private readonly IInterpreterComponent _interpreterComponent;
        private readonly IPatientCallRequestsComponent _patientCallRequestsComponent;

        public PatientController(IUserComponent userComponent
                               , ICountryComponent countryComponent
                               , ICityComponent cityComponent
                               , IPatientComponent patientComponent
                               , IDoctorComponent doctorComponent
                               , IPaymentComponent paymentComponent
                               , IAttachmentComponent attachmentComponent
                               , ITestimonialComponent testimonialComponent
                               , ILogComponent logComponent
                               , IIdentityMessageService IdentityMessageService
                               , ICaseComponent caseComponent
                               , ICaseQuestionsComponent caseQuestionsComponent
                               , ISpecialtyComponent specialtyComponent
                               , IInterpreterComponent interpreterComponent
                               , IPatientCallRequestsComponent patientCallRequestsComponent) : base(attachmentComponent)
        {
            _userComponent = userComponent;
            _countryComponent = countryComponent;
            _cityComponent = cityComponent;
            _patientComponent = patientComponent;
            _doctorComponent = doctorComponent;
            _paymentComponent = paymentComponent;
            _logComponent = logComponent;
            _attachmentComponent = attachmentComponent;
            _caseComponent = caseComponent;
            _caseQuestionsComponent = caseQuestionsComponent;
            _IdentityMessageService = IdentityMessageService;
            _testimonialComponent = testimonialComponent;
            _specialtyComponent = specialtyComponent;
            _interpreterComponent = interpreterComponent;
            _patientCallRequestsComponent = patientCallRequestsComponent;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            protected set => _userManager = value;
        }

        // GET: Notifications
        [Authorize(Roles = "Patient")]
        public ActionResult Notifications()
        {
            ViewBag.CallRequests = _patientCallRequestsComponent.GetList(Guid.Parse(User.Identity.GetUserId()));
            return View();
        }

        [Authorize(Roles = "Patient")]
        public ActionResult CasePayment(Guid DoctorId)
        {
            decimal doctor = _doctorComponent.GetDoctorFees(DoctorId);

            ViewBag.Price = doctor;
            Guid patientId = _patientComponent.GetPatientId(Guid.Parse(User.Identity.GetUserId()));
            PatientViewModels patient = _patientComponent.GetPatientProfileId(patientId);
            Origin.YMC.Web.Client.Models.CaseViewModel caseViewModel = new Origin.YMC.Web.Client.Models.CaseViewModel
            {
                PaymentInfoAddress1 = patient.PaymentInfoAddress1,
                PaymentInfoCardholderFullName = patient.PaymentInfoCardholderFullName,
                PaymentInfoCreditCardNumber = patient.PaymentInfoCreditCardNumber,
                PaymentInfoCVV = patient.PaymentInfoCVV,
                PaymentInfoCity = patient.PaymentInfoCity,
                PaymentInfoCreditCardType = patient.PaymentInfoCreditCardType,
                PaymentInfoExpirationDate = patient.PaymentInfoExpirationDate,
                PaymentInfoExpirationMonth = patient.PaymentInfoExpirationDate.ToString("MM"),
                PaymentInfoExpirationYear = patient.PaymentInfoExpirationDate.ToString("yy"),
                PaymentInfoCountry = patient.PaymentInfoCountry,
                PaymentInfoAddress2 = patient.PaymentInfoAddress2,
                PaymentInfoState = patient.PaymentInfoState,
                DoctorId = DoctorId,
                InterpreterStatus = (int)InterpretationTypes.NoInterpretationService
            };
            return View(caseViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<ActionResult> CasePayment(CaseViewModel CaseViewModel)
        {

            if (ModelState.IsValid)
            {

                CaseViewModel.PaymentInfoExpirationDate = DateTime.ParseExact((CaseViewModel.PaymentInfoExpirationMonth.Length == 1 ? "0" + CaseViewModel.PaymentInfoExpirationMonth : CaseViewModel.PaymentInfoExpirationMonth) + "/" + CaseViewModel.PaymentInfoExpirationYear, "MM/yy", CultureInfo.InvariantCulture);

                Guid currentUserId = Guid.Parse(User.Identity.GetUserId());
                CaseViewModel.PatientId = _patientComponent.GetPatientId(currentUserId);

                CaseViewModel.CaseId = _caseComponent.Add(CaseViewModel);
                _paymentComponent.UpdatePatientPayment(new PaymentViewModel
                {
                    CardholderFullName = CaseViewModel.PaymentInfoCardholderFullName,
                    CreditCardNumber = CaseViewModel.PaymentInfoCreditCardNumber,
                    CVV = CaseViewModel.PaymentInfoCVV,
                    ExpirationDate = CaseViewModel.PaymentInfoExpirationDate,

                }, CaseViewModel.PatientId);
                if (CaseViewModel.InterpreterStatus == (int)InterpretationTypes.TranslateCaseAndMedicalReport)
                {
                    CaseViewModel.interpreterId = _interpreterComponent.AssignInterpreterToCase(CaseViewModel.CaseId);
                    //var userId = _interpreterComponent.GetUserId(CaseViewModel.interpreterId.Value);
                    //var body = $"Dear<br />A medical report needs to be interpreted has been assigned to you<br />Case url : <a href='{ ConfigurationManager.AppSettings["FrontPath"] + "Interpreter/ProfileOpenCase?caseId=" + CaseViewModel.CaseId}'>Click here</a>";


                    //await UserManager.SendEmailAsync(userId, "Interpreter Task", body);



                }
            }
            return RedirectToAction("CaseAttachement", new { CaseId = CaseViewModel.CaseId });
        }

        [Authorize(Roles = "Patient")]
        public ActionResult CaseAttachement(Guid CaseId)
        {
            if (!IsCurrentUserIsTheRequestPatient(_caseComponent.GetCasePatientId(CaseId)))
            {
                throw new Exception("Not Authorized Exception");
            }

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public ActionResult CaseAttachement(string AttachmentCertificates, Guid CaseId)
        {
            // var doctor = _doctorComponent.GetDoctorFees(DoctorId);
            if (!IsCurrentUserIsTheRequestPatient(_caseComponent.GetCasePatientId(CaseId)))
            {
                throw new Exception("Not Authorized Exception");
            }
            // ViewBag.Price = doctor;
            Guid patientId = _patientComponent.GetPatientId(Guid.Parse(User.Identity.GetUserId()));
            if (!string.IsNullOrEmpty(AttachmentCertificates))
            {
                var certs = AttachmentCertificates.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < certs.Length; i++)
                {
                    var AttachmentPath = certs[i];
                    if (AttachmentPath != null)
                    {

                        //4 - Certificate 
                        AddAttachment(AttachmentPath, CaseId, (int)AttachmentTypeEnum.PatientAttachments);

                    }
                }
            }


            return RedirectToAction("CasePatientInfo", new { CaseId = CaseId });
        }

        [Authorize(Roles = "Patient")]
        public ActionResult CaseReview(Guid CaseId)
        {

            if (!IsCurrentUserIsTheRequestPatient(_caseComponent.GetCasePatientId(CaseId)))
            {
                throw new Exception("Not Authorized Exception");
            }

            return View(_caseComponent.CaseDetails(CaseId));
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public ActionResult CaseReview(HttpPostedFileBase[] AttachmentCertificate, Guid CaseId)
        {
            // var doctor = _doctorComponent.GetDoctorFees(DoctorId);
            if (!IsCurrentUserIsTheRequestPatient(_caseComponent.GetCasePatientId(CaseId)))
            {
                throw new Exception("Not Authorized Exception");
            }
            // ViewBag.Price = doctor;
            Guid patientId = _patientComponent.GetPatientId(Guid.Parse(User.Identity.GetUserId()));
            foreach (HttpPostedFileBase file in AttachmentCertificate)
            {
                UploadFile(file, CaseId, (int)AttachmentTypeEnum.PatientAttachments);

            }

            return RedirectToAction("CasePatientInfo", new { CaseId = CaseId });
        }

        [Authorize(Roles = "Patient")]
        public ActionResult CasePatientInfo(Guid CaseId)
        {
            if (!IsCurrentUserIsTheRequestPatient(_caseComponent.GetCasePatientId(CaseId)))
            {
                throw new Exception("Not Authorized Exception");
            }
            var caseObj = _caseComponent.CaseDetails(CaseId);

            CasePatientInfoViewModel casePatientInfo = caseObj.CasePatientInfo;
            List<SelectListItem> countries = new List<SelectListItem>();
            if (ApplicationContext.IsArabic)
            {
                countries.Add(new SelectListItem() { Value = string.Empty, Text = $"-- {Resources.GlobalStrings.Select} --" });
            }
            else
            {
                countries.Add(new SelectListItem() { Value = string.Empty, Text = "-- Select --" });
            }
            if (ApplicationContext.IsArabic)
            {
                ViewBag.Salutions = new List<SelectListItem> { new SelectListItem { Text = "السيد", Value = "1" }, new SelectListItem { Text = "السيدة", Value = "2" },  new SelectListItem { Text = "الآنسة", Value = "3" } };
            }
            else
            {
                ViewBag.Salutions = new List<SelectListItem> { new SelectListItem { Text = "Mr", Value = "1" }, new SelectListItem { Text = "Mrs", Value = "2" },  new SelectListItem { Text = "Miss", Value = "3" } };
            }
            countries.AddRange(_countryComponent.GetAll().Select(c => new SelectListItem() { Value = c.Code, Text = c.Name }).ToList());
            ViewBag.countries = countries;
            var patientId = _caseComponent.GetCasePatientId(CaseId);

            if (casePatientInfo.FirstName == null)
            {

            var patientInfo = _patientComponent.GetPatientProfileId(patientId);

            casePatientInfo.FirstName =  patientInfo.FirstName;
            casePatientInfo.LastName = patientInfo.LastName;
            casePatientInfo.Address1 = patientInfo.Address1;
            casePatientInfo.Address2 = patientInfo.Address2;
            casePatientInfo.CountryId = patientInfo.CountryCode ;
            casePatientInfo.CityId = patientInfo.CityId;
            casePatientInfo.Region = patientInfo.NonUsStateOrProvince;
            casePatientInfo.PrimaryPhone = patientInfo.MobilePhone;
            casePatientInfo.SecondaryPhone = patientInfo.Phone;
            casePatientInfo.Email  = patientInfo.Email;
            casePatientInfo.BirthDate  = patientInfo.BirthDate;
            casePatientInfo.Gender  = (GenderEnum)patientInfo.GenderId;

            }
            return View(casePatientInfo);
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public ActionResult CasePatientInfo(CasePatientInfoViewModel casePatientInfo)
        {
            if (!IsCurrentUserIsTheRequestPatient(_caseComponent.GetCasePatientId(casePatientInfo.CaseId)))
            {
                throw new Exception("Not Authorized Exception");
            }

            _caseComponent.UpdateCase(casePatientInfo);
            return RedirectToAction("PatientCaseQuestions", new { CaseId = casePatientInfo.CaseId });
        }

        [Authorize(Roles = "Patient")]
        public ActionResult PatientCaseQuestions(Guid CaseId)
        {
            if (!IsCurrentUserIsTheRequestPatient(_caseComponent.GetCasePatientId(CaseId)))
            {
                throw new Exception("Not Authorized Exception");
            }

            CasePatientQuestionsViewModel casePatientQuestionsViewModel = new CasePatientQuestionsViewModel
            {
                CaseId = CaseId
            };

            return View(casePatientQuestionsViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public ActionResult PatientCaseQuestions(CasePatientQuestionsViewModel casePatientQuestionsViewModel)
        {
            if (!IsCurrentUserIsTheRequestPatient(_caseComponent.GetCasePatientId(casePatientQuestionsViewModel.CaseId)))
            {
                throw new Exception("Not Authorized Exception");
            }

            /// heaiba it's wrong solution 
            if (casePatientQuestionsViewModel.PatientPhysicians != null && casePatientQuestionsViewModel.PatientPhysicians.Count > 0)
                foreach (var item in casePatientQuestionsViewModel.PatientPhysicians)
                {
                    item.Name = item.Name ?? " ";
                    item.MailingAddress = item.MailingAddress ?? " ";
                    item.Phone = item.Phone ?? " ";
                    item.Fax = item.Fax ?? " ";
                    item.Email = item.Email ?? " ";
                }

            _caseQuestionsComponent.UpdatePatientAnswers(casePatientQuestionsViewModel);

            return RedirectToAction("PatientCaseReviewSubmit", new { CaseId = casePatientQuestionsViewModel.CaseId });
        }


        [Authorize(Roles = "Patient")]
        public ActionResult PatientCaseReviewSubmit(Guid caseId)
        {
            var result = _caseComponent.CaseDetails(caseId);
            return View(result);
        }


        [Authorize(Roles = "Patient")]
        public ActionResult PatientCaseSubmit(Guid CaseId)
        {
            if (!IsCurrentUserIsTheRequestPatient(_caseComponent.GetCasePatientId(CaseId)))
            {
                throw new Exception("Not Authorized Exception");
            }

            CaseDetailsViewModel currentCase = _caseComponent.CaseDetails(CaseId);
            Business.Entities.Domain.Doctors.ViewModel.DoctorViewModels doctor = _doctorComponent.GetDoctorProfileId(currentCase.CaseInfo.DoctorID);
            decimal interpretationFees = 0;
            if (currentCase.CaseInfo.InterpretationTypeId != InterpretationTypes.NoInterpretationService)
            {
                interpretationFees = _interpreterComponent.GetInterpretationFees((int)currentCase.CaseInfo.InterpretationTypeId);
            }
            CaseSubmitViewModel case1 = new CaseSubmitViewModel
            {
                CaseId = CaseId,
                DoctorId = currentCase.CaseInfo.DoctorID,
                DoctorName = doctor.FirstName + " " + doctor.LastName,
                DoctorSpeciality = doctor.SpecialtyName,
                DoctorPrice = (doctor.fees + interpretationFees).ToString(CultureInfo.InvariantCulture),
                DoctorResponseTime = doctor.ResponsTime
            };

            return View(case1);
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<ActionResult> PatientSubmit(CaseSubmitViewModel Case)
        {
            if (!IsCurrentUserIsTheRequestPatient(_caseComponent.GetCasePatientId(Case.CaseId)))
            {
                throw new Exception("Not Authorized Exception");
            }

            _caseComponent.UpdateCaseStatus(Case.CaseId);
            var caseDetails = _caseComponent.CaseDetails(Case.CaseId);
            Business.Entities.Domain.Doctors.ViewModel.DoctorViewModels doctor = _doctorComponent.GetDoctorProfileId(Case.DoctorId);

            await _IdentityMessageService.SendAsync(new IdentityMessage
            {
                Subject = "YMC New Case Registered",
                Body = string.Format($"Dear Dr.{doctor.FirstName}<br />Kindly be informed that a new case has been registered  <a href='{System.Configuration.ConfigurationManager.AppSettings["FrontPath"]}/Doctor/ProfileOpenCase?caseId={Case.CaseId}'> click here </a> to view case details <br />Thanks"),
                Destination = doctor.Email
            });
            await _IdentityMessageService.SendAsync(new IdentityMessage
            {
                Subject = "YMC New Case Registered",
                Body = string.Format($"Dear {caseDetails.CasePatientInfo.FirstName}<br />Kindly be informed that your case has been registered  <br />Thanks"),
                Destination = caseDetails.CasePatientInfo.Email
            });
            if (caseDetails.CaseInfo.InterpretationTypeId == InterpretationTypes.TranslateCaseAndMedicalReport && caseDetails.CaseInfo.AssignedToInterpreterId.HasValue)
            {
                //var userId = _interpreterComponent.GetUserId(CaseViewModel.interpreterId.Value);
                var body = $"Dear<br />A medical report needs to be interpreted has been assigned to you<br />Case url : <a href='{ ConfigurationManager.AppSettings["FrontPath"] + "Interpreter/ProfileOpenCase?caseId=" + caseDetails.CaseInfo.CaseId}'>Click here</a>";


                await UserManager.SendEmailAsync(_interpreterComponent.GetUserId(caseDetails.CaseInfo.AssignedToInterpreterId.Value), "Interpreter Task", body);



            }
            return RedirectToAction("index", "home");
        }

        [Authorize(Roles = "Patient")]
        public ActionResult LatestConsultations(Guid? PatientId)
        {

            if (PatientId == null)
            {
                Guid currentUserId = Guid.Parse(User.Identity.GetUserId());
                PatientId = _patientComponent.GetPatientId(currentUserId);
            }
            List<CaseListingViewModel> patientCase = _caseComponent.GetCasesByPatientId(PatientId.Value);
            ViewBag._testimonials = _testimonialComponent.GetList(string.Empty).ToList();
            ViewBag._specialties = _specialtyComponent.GetList(string.Empty).ToList();
            ViewBag._patientUserInfo = _patientComponent.GetPatientProfileId(PatientId.Value);
            return View(patientCase);
        }

        private bool IsCurrentUserIsTheRequestPatient(Guid PatientId)
        {
            Guid currentUserId = Guid.Parse(User.Identity.GetUserId());
            return PatientId == _patientComponent.GetPatientId(currentUserId);
        }

        [HttpGet]
        public ActionResult CheckUserCompleteProfile(Guid? patientId)
        {

            try
            {
                if (patientId != null && patientId != Guid.Empty)
                {
                    bool result = _patientComponent.CheckUserCompleteProfile(patientId.Value);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return RedirectToAction("index", "Home");
            }

        }

        [HttpGet]
        public ActionResult GetCallRequestsCount()
        {

            try
            {
                int result = _patientCallRequestsComponent.GetNotificationCount(Guid.Parse(User.Identity.GetUserId()));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return RedirectToAction("index", "Home");
            }

        }

        [HttpPost]
        public JsonResult AddTestimonial(string testimonial)
        {
            int status = _testimonialComponent.Add(new Testimonial()
            {
                CreationDate = DateTime.Now,
                IsActive = false,
                Id = Guid.NewGuid(),
                CreatedBy = Guid.Parse(User.Identity.GetUserId()),
                PatientId = _patientComponent.GetPatientId(Guid.Parse(User.Identity.GetUserId())),
                TextReview = testimonial
            });
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddCallRequest(Guid caseId)
        {
            int status = _patientCallRequestsComponent.Add(new PatientCallRequestsViewModel()
            {
                CaseId = caseId,
                PatientId = _patientComponent.GetPatientId(Guid.Parse(User.Identity.GetUserId()))
            });
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}