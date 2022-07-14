using Microsoft.AspNet.Identity.Owin;
using Origin.YMC.Business.Components.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Origin.YMC.Business.Entities.Domain.Patients.ViewModel;
using Origin.YMC.Business.Entities.Domain.Payments.ViewModel;
using Origin.YMC.Business.Entities.Domain;
using Origin.YMC.Business.Entities.Domain.Doctors.ViewModel;
using System.IO;
using System.Threading.Tasks;
using Origin.YMC.Business.Components.ViewModels;
using Origin.YMC.Business.Entities.Domain.Patients.Enums;
using Origin.YMC.Business.Entities.Domain.Testimonials;

namespace Origin.YMC.Web.Client.Controllers
{

    public class DoctorController : BaseController
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
        private readonly ICaseComponent _caseComponent;
        private readonly ICaseQuestionsComponent _caseQuestionsComponent;
        private readonly IInterpreterComponent _interpreterComponent;
        public DoctorController(IUserComponent userComponent
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
                                , ISpecialtyComponent specialtyComponent
                                , ICaseComponent caseComponent
                                , ICaseQuestionsComponent caseQuestionsComponent, IInterpreterComponent interpreterComponent) : base(attachmentComponent)
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
            _caseComponent = caseComponent;
            _caseQuestionsComponent = caseQuestionsComponent;
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

        // GET: Doctor
        [Authorize(Roles = "Doctor")]
        public ActionResult DoctorProfile()
        {
            try
            {
                var doctorId = _doctorComponent.GetDoctorId(Guid.Parse(User.Identity.GetUserId()));
                var result = _doctorComponent.GetDoctorProfileId(doctorId);
                ViewBag._specialties = _specialtyComponent.GetList(string.Empty).ToList();
                if (result != null)
                {
                    ViewBag._doctorProfileInfo = result;
                    return View();
                }
                else
                    return RedirectToAction("index", "Home");
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return RedirectToAction("index", "Home");
            }
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult EditProfile()
        {
            try
            {
                var doctorId = _doctorComponent.GetDoctorId(Guid.Parse(User.Identity.GetUserId()));
                var result = _doctorComponent.GetDoctorProfileId(doctorId);
                if (result != null)
                {
                    ViewBag._doctorProfileInfo = result;
                    ViewBag._specialties = _specialtyComponent.GetList(string.Empty).ToList();
                    return View();
                }
                else
                    return RedirectToAction("index", "Home");
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return RedirectToAction("index", "Home");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "Doctor")]
        public ActionResult EditProfile(string bio, string experinces, Guid specialtyID, string fess, string responsTime, string focusOfScientific,string onlineProfileLink)
        {
            try
            {
                var doctorId = _doctorComponent.GetDoctorId(Guid.Parse(User.Identity.GetUserId()));
                var result = _doctorComponent.GetDoctorProfileId(doctorId);
                ViewBag._specialties = _specialtyComponent.GetList(string.Empty).ToList();
                if (result != null)
                {
                    result.Bio = !string.IsNullOrWhiteSpace(bio) ? bio : result.Bio;
                    result.Experinces = !string.IsNullOrWhiteSpace(experinces) ? experinces : result.Experinces;
                    result.SpecialtyId = specialtyID;
                    result.fees = decimal.Parse(fess);
                    result.ResponsTime = int.Parse(responsTime);
                    result.FocusOfScientific = !string.IsNullOrWhiteSpace(focusOfScientific) ? focusOfScientific : result.FocusOfScientific;
                    result.OnlineProfileLink = !string.IsNullOrWhiteSpace(onlineProfileLink) ? onlineProfileLink : result.OnlineProfileLink;

                    _doctorComponent.Update(result);

                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(0, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return RedirectToAction("index", "Home");
            }
        }

        public ActionResult UploadProfileImage()
        {
            var doctorId = _doctorComponent.GetDoctorId(Guid.Parse(User.Identity.GetUserId()));
            var result = _doctorComponent.GetDoctorProfileId(doctorId);
            if (result != null)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var AttachmentPath = Request.Files[i];
                    if (AttachmentPath != null)
                    {
                        if (AttachmentPath.ContentLength > 0)
                        {
                            //4 - Certificate 
                            UploadFile(AttachmentPath, doctorId, (int)AttachmentTypeEnum.DoctorProfileImage);
                        }
                    }
                }
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Doctors(Guid? specialtyId)
        {
            ViewBag._specialties = _specialtyComponent.GetList(string.Empty).ToList();
            ViewBag._doctors = _doctorComponent.List(string.Empty).ToList();

            if (specialtyId != null && specialtyId != Guid.Empty)
                ViewBag._selectedSpecialtyId = specialtyId;


            return View();
        }

        [HttpGet]
        public ActionResult DoctorProfileInfo(Guid doctorId)
        {
            try
            {
                if (doctorId != Guid.Empty && doctorId != null)
                {
                    var result = _doctorComponent.GetDoctorProfileId(doctorId);
                    var doctors = _doctorComponent.GetDoctorsByspecialtyId(result.SpecialtyId).ToList();
                    if (result != null)
                    {
                        ViewBag._doctorProfileInfo = result;
                        doctors.Remove(result);
                        ViewBag._doctorsWithSamespecialty = doctors;
                        ViewBag._testimonials = _testimonialComponent.GetList(string.Empty).ToList();
                        ViewBag._specialties = _specialtyComponent.GetList(string.Empty).ToList();
                        return View();
                    }
                    else
                        return RedirectToAction("index", "Home");
                }
                else
                    return RedirectToAction("index", "Home");

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return RedirectToAction("index", "Home");
            }
        }

        [HttpPost]
        public ActionResult GetDoctors(Guid? specialtyId)
        {
            try
            {
                if (specialtyId != null && specialtyId != Guid.Empty)
                {
                    var doctors = _doctorComponent.GetDoctorsByspecialtyId(specialtyId.Value);
                    return Json(doctors, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = _doctorComponent.List(string.Empty).ToList();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return RedirectToAction("index", "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult CheckUserCompleteProfile(Guid? doctorId)
        {

            try
            {
                if (doctorId != null && doctorId != Guid.Empty)
                {
                    var result = _doctorComponent.CheckUserCompleteProfile(doctorId.Value);
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

        public ActionResult GetDoctorsCases(int lenght, int page)
        {

            try
            {
                var doctorId = _doctorComponent.GetDoctorId(Guid.Parse(User.Identity.GetUserId()));
                var cases = _caseComponent.GetCasesByDoctorId(doctorId);
                var patientCases = cases.Skip(page * lenght).Take(lenght).ToList();
                var casesCount = cases.Count();
                return Json(new { patientCases, casesCount }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return RedirectToAction("index", "Home");
            }

        }

        [Authorize(Roles = "Doctor,Interpreter,Patient")]
        public ActionResult ProfileOpenCase(Guid caseId)
        {
            try
            {
                if (caseId != Guid.Empty && caseId != null)
                {
                    var result = _caseComponent.CaseDetails(caseId);

                    var doctorId = _doctorComponent.GetDoctorId(Guid.Parse(User.Identity.GetUserId()));
                    var PatientId = _patientComponent.GetPatientId(Guid.Parse(User.Identity.GetUserId()));
                    var interpreterId = _interpreterComponent.GetInterpreterId(Guid.Parse(User.Identity.GetUserId()));
                    if( (doctorId == result.CaseInfo.DoctorID && User.IsInRole("Doctor")) ||
                        (PatientId == result.CaseInfo.PatientId && User.IsInRole("Patient"))
                        ||
                        (interpreterId == result.CaseInfo.InterpreterId && User.IsInRole("Interpreter")))
                    {
                        ViewBag._caseDetilsInfo = result;
                        ViewBag._specialties = _specialtyComponent.GetList(string.Empty).ToList();
                        return View();
                    }
                    else
                        return RedirectToAction("DoctorProfile");
                }
                else
                    return RedirectToAction("index", "Home");

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return RedirectToAction("index", "Home");
            }

        }

        public async Task<ActionResult> SaveDoctorReportDiagnosis( string DoctorReportDiagnosis, Guid caseId,string firstQuestion,string secondQuestion, string thirdQuestion,string AttachmentCertificates)
        {
            try
            {
                if (caseId != Guid.Empty && caseId != null)
                {
                 
                    _caseComponent.AddDoctorCaseReport(DoctorReportDiagnosis, caseId, firstQuestion, secondQuestion, thirdQuestion);

                    var certs = AttachmentCertificates.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < certs.Length; i++)
                    {
                        var AttachmentPath = certs[i];
                        if (AttachmentPath != null)
                        {

                            //4 - Certificate 
                            AddAttachment(AttachmentPath, caseId, (int)AttachmentTypeEnum.CaseDocuments);

                        }
                    }
                    var caseDetails = _caseComponent.CaseDetails(caseId);
                    if (caseDetails.CaseInfo.Status == StatusEnum.MedicalReportUnderInterpretation &&
                        caseDetails.CaseInfo.AssignedToInterpreterId.HasValue)
                    {
                        var body =
                            $"Dear<br />A medical report needs to be interpreted has been assigned to you<br />Case url : {ConfigurationManager.AppSettings["FrontPath"] + "Interpreter/ProfileOpenCase?caseId=" + caseDetails.CaseInfo.CaseId}";

                        await UserManager.SendEmailAsync(
                            _interpreterComponent.GetUserId(caseDetails.CaseInfo.AssignedToInterpreterId.Value),
                            "Interpreter Task", body);

                    }
                    return Json("ok", JsonRequestBehavior.AllowGet);
                }
                else
                    return RedirectToAction("index", "Home");

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return RedirectToAction("index", "Home");
            }

        }

    }
}