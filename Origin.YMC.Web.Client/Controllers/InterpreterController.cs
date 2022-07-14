using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Components.ViewModels;
using Origin.YMC.Business.Entities.Domain.Patients.Enums;
using Origin.YMC.Business.Entities.Domain.Patients.ViewModel;

namespace Origin.YMC.Web.Client.Controllers
{
    public class InterpreterController : BaseController
    {
        private readonly IAttachmentComponent _attachmentComponent;
        private readonly IInterpreterComponent _interpreterComponent;
        private readonly ICaseComponent _caseComponent;
        private readonly ILogComponent _logComponent;
        protected ApplicationUserManager _userManager;

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
            Guid interpreterId = _interpreterComponent.GetInterpreterId(Guid.Parse(User.Identity.GetUserId()));

            return View(_caseComponent.GetCasesByInterpreterId(interpreterId));
        }

        public InterpreterController(IAttachmentComponent attachmentComponent, IInterpreterComponent interpreterComponent, ICaseComponent caseComponent, ILogComponent logComponent) : base(attachmentComponent)
        {
            _attachmentComponent = attachmentComponent;
            _interpreterComponent = interpreterComponent;
            _caseComponent = caseComponent;
            _logComponent = logComponent;
        }
        public ActionResult changeInterpreter(Guid caseId)
        {
            var interpreterId = _interpreterComponent.GetInterpreterId(Guid.Parse(User.Identity.GetUserId()));

            return Json(_interpreterComponent.ChangeInterpreter(caseId,interpreterId));
        }
        public ActionResult GetInterpretationFees(int interpretationTypeId)
        {
            return Json(_interpreterComponent.GetInterpretationFees(interpretationTypeId));
        }
        [HttpGet]
        [Authorize(Roles = "Interpreter")]
        public ActionResult ProfileOpenCase(Guid caseId)
        {
            try
            {
                if (caseId != Guid.Empty)
                {
                    var result = _caseComponent.CaseDetails(caseId);
                    var interpreterId = _interpreterComponent.GetInterpreterId(Guid.Parse(User.Identity.GetUserId()));
                    if (interpreterId == result.CaseInfo.AssignedToInterpreterId)
                    {
                        //ViewBag._caseDetilsInfo = result;
                        return View(result);
                    }
                    else
                        return RedirectToAction("Index","Interpreter");
                }

                return RedirectToAction("index", "Home");

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return RedirectToAction("index", "Home");
            }

        }

        [HttpPost]
        [Authorize(Roles = "Interpreter")]
        public ActionResult ProfileOpenCase(CaseDetailsViewModel CaseDetailsViewModel)
        {
            try
            {
                if (CaseDetailsViewModel != null)
                {
                    var currentUserId = Guid.Parse(User.Identity.GetUserId());
                    _caseComponent.AddInterpretation(CaseDetailsViewModel.CasePatientQuestions, CaseDetailsViewModel.CaseInfo.CaseId);
                    var interpreterId = _interpreterComponent.GetInterpreterId(Guid.Parse(User.Identity.GetUserId()));
                    //await UserManager.SendEmailAsync(currentUserId, "Interpreter Task", $"A new case needs an interpreter has been assigned to you ");

                    //if (interpreterId == result.CaseInfo.AssignedToInterpreterId)
                    //{
                    //    //ViewBag._caseDetilsInfo = result;
                    //    return View(result);
                    //}
                    //else
                    //    return RedirectToAction("Index", "Interpreter");
                }

                return RedirectToAction("index", "Interpreter");

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return RedirectToAction("index", "Home");
            }

        }
        public async Task<ActionResult> SaveDoctorReportDiagnosis(string DoctorReportDiagnosis, Guid caseId,string AttachmentCertificates)
        {
            try
            {
                if (caseId != Guid.Empty && caseId != null)
                {
                    _caseComponent.AddDoctorCaseReportInterpreter(DoctorReportDiagnosis, caseId);

                    var certs = AttachmentCertificates.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < certs.Length; i++)
                    {
                        var AttachmentPath = certs[i];
                        if (AttachmentPath != null)
                        {

                            //4 - Certificate 
                            AddAttachment(AttachmentPath, caseId, (int)AttachmentTypeEnum.MedicalReportDocument);

                        }
                    }
                    var caseDetails = _caseComponent.CaseDetails(caseId);
                    if (caseDetails.CaseInfo.Status == StatusEnum.MedicalReportUnderInterpretation &&
                        caseDetails.CaseInfo.AssignedToInterpreterId.HasValue)
                    {
                        await UserManager.SendEmailAsync(
                            _interpreterComponent.GetUserId(caseDetails.CaseInfo.AssignedToInterpreterId.Value),
                            "Interpreter Task", $"A medical report needs to be interpreted has been"
                                                + $" assigned to you ");

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