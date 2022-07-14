using Origin.YMC.Business.Components.Interfaces;
using System;
using System.Web.Mvc;
using Origin.YMC.Business.Entities.Domain.QuestionsThreads.ViewModels;
using System.Net;
using Origin.YMC.Business.Entities.Domain.QuestionsThreads;
using System.Threading.Tasks;

namespace Origin.YMC.Web.Client.Controllers
{
    [Authorize]
    public class QuestionThreadsController : BaseController
    {
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
        private readonly IQuestionThreadComponent _questionThreadComponentComponent;

        public QuestionThreadsController(IUserComponent userComponent
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
                                , IQuestionThreadComponent questionThreadComponentComponent
                                , ICaseQuestionsComponent caseQuestionsComponent) : base(attachmentComponent)
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
            _questionThreadComponentComponent = questionThreadComponentComponent;
         }

        // GET: QuestionThreads
        public ActionResult Questions(Guid caseId)
        {
            try
            {
                if (caseId != null && caseId != Guid.Empty)
                {
                    var result = _questionThreadComponentComponent.GetQuestionsThreads(caseId);
                    return View(result);
                }
                else return Redirect("/home");
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Redirect("/home");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddQuestionThread(QuestionThreadViewModel model)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(model.ChatText) && model.CaseId != null && model.CaseId != Guid.Empty && model.OwnerId != null && model.OwnerId != Guid.Empty && (model.OwnerType == OwnerType.Doctor || model.OwnerType == OwnerType.Patient))
                {
                    _questionThreadComponentComponent.Add(model);
                   var details =  _caseComponent.CaseDetails(model.CaseId);
                    Guid targetID = Guid.Empty;
                    if (model.OwnerType == OwnerType.Doctor)
                    {
                        targetID = details.CaseInfo.PatientUserId;
                      await  UserManager.SendEmailAsync(targetID, "New Comment Submitted ", details.CaseInfo.DoctorName + " has submitted new comment \" " + model.ChatText + " \"");

                    }
                    else
                    {
                        targetID = details.CaseInfo.DoctorUserId;
                      await  _userManager.SendEmailAsync(targetID, "New Comment Submitted ", details.CaseInfo.PatientName + " has submitted new comment \" " + model.ChatText + " \"");

                    }

                    return base.ResponesWithMessage(HttpStatusCode.OK, "done");
                }else
                    return base.ResponesWithMessage(HttpStatusCode.InternalServerError, "invalid Input");
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return base.ResponesWithMessage(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}