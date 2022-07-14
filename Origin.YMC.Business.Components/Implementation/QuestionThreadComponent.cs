using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.QuestionsThreads.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Origin.YMC.Business.Components.ViewModels;
using Origin.YMC.Business.Entities.Domain.Doctors.ViewModel;
using Origin.YMC.Business.Entities.Domain.Patients;
using Origin.YMC.Business.Entities.Domain.Patients.ViewModel;
using Origin.YMC.Repositories;
using Origin.YMC.Business.Entities.Domain.QuestionsThreads;
using System.Data.Entity;
using Origin.YMC.Core.Common;

namespace Origin.YMC.Business.Components.Implementation
{
    public class QuestionThreadComponent : IQuestionThreadComponent
    {
        private readonly IRepository<QuestionThread> _questionThreadRepository;
        private readonly IRepository<Case> _caseRepository;
        private readonly IRepository<CasePatientQuestionsAnswers> _caseQuestionsRepository;
        private readonly IRepository<PatientPhysician> _PatientPhysicianRepository;
        private readonly IUserComponent _userComponent;
        private readonly ILogComponent _logComponent;
        private readonly IAttachmentComponent _attachmentComponent;

        public QuestionThreadComponent(
            IRepository<QuestionThread> questionThreadRepository,
            IRepository<CasePatientQuestionsAnswers> caseQuestionsRepository,
            IRepository<PatientPhysician> PatientPhysicianRepository,
            IRepository<Case> caseRepository,
            IUserComponent userComponent,
            IAttachmentComponent attachmentComponent,
            ILogComponent logComponent)
        {
            _userComponent = userComponent;
            _caseRepository = caseRepository;
            _caseQuestionsRepository = caseQuestionsRepository;
            _logComponent = logComponent;
            _PatientPhysicianRepository = PatientPhysicianRepository;
            _attachmentComponent = attachmentComponent;
            _questionThreadRepository = questionThreadRepository;
        }

        public void Add(QuestionThreadViewModel questionThread)
        {
            try
            {
                _questionThreadRepository.Add(new QuestionThread()
                {
                    Id = Guid.NewGuid(),
                    CaseId = questionThread.CaseId,
                    OwnerId = questionThread.OwnerId,
                    OwnerType = questionThread.OwnerType,
                    ChatText = questionThread.ChatText,
                    CreationDate = DateTime.Now,
                    CreatedBy = Guid.NewGuid(),
                    IsActive = true,
                    LastUpdatedAt = DateTime.Now,
                    LastUpdatedBy = Guid.NewGuid(),
                });
                _questionThreadRepository.UnitOfWork.SaveChanges(true);

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public QuestionViewModel GetQuestionsThreads(Guid caseId)
        {
            try
            {

                var _case = _caseRepository.GetAll()
                    .Include(c => c.Doctor)
                    .Include(c => c.Doctor.ApplicationUser)
                    .Include(c => c.Doctor.Specialty)
                    .Include(c => c.Patient)
                    .Include(c => c.Patient.ApplicationUser)
                    .FirstOrDefault(c => c.Id == caseId);
                var result = new QuestionViewModel();
                result.DoctorReportDiagnosis = _case.DoctorReportDiagnosis;
                result.CaseId = _case.Id;
                result.ClosureDate = _case.ClosureDate;
                result.CasseAttchment = GetCaseAttchments(_case.Id);
                result.CasseAttchmentInterpreted = GetCaseAttchmentsInterpreted(_case.Id);

                result.Doctor = new DoctorViewModels()
                {
                    FirstName = _case.Doctor.ApplicationUser.FirstName,
                    LastName = _case.Doctor.ApplicationUser.LastName,
                    ProfileImage = GetDoctroProfileImage(_case.DoctorId),
                    CityName = ApplicationContext.IsArabic ? _case.Doctor.ApplicationUser.City.NameAr : _case.Doctor.ApplicationUser.City.NameAr,
                    SpecialtyName = ApplicationContext.IsArabic ? _case.Doctor.Specialty.TitleAr : _case.Doctor.Specialty.TitleEn,
                    ResponsTime = _case.Doctor.ResponsTime
                };

                result.Patient = new PatientViewModels()
                {
                    FirstName = _case.Patient.ApplicationUser.FirstName,
                    LastName = _case.Patient.ApplicationUser.LastName,
                    CityName = ApplicationContext.IsArabic ? _case.Patient.ApplicationUser.City.NameAr : _case.Patient.ApplicationUser.City.NameAr,

                };

                result.Questions = _questionThreadRepository
                    .GetAll()
                    .Include(c => c.Case)
                    .Include(c => c.Case.Patient)
                    .Include(c => c.Case.Patient.ApplicationUser)
                    .Include(c => c.Case.Doctor)
                    .Include(c => c.Case.Doctor.ApplicationUser)
                    .Where(c => c.CaseId == caseId)
                    .OrderBy(c => c.CreationDate)
                    .ToList().Select(c => new QuestionThreadViewModel()
                    {
                        CaseId = c.CaseId,
                        ChatText = c.ChatText,
                        OwnerId = c.OwnerId,
                        OwnerType = c.OwnerType,
                        ownerInfo = c.OwnerType == OwnerType.Doctor ? new OwnerInfo()
                        {
                            Name = $"{c.Case.Doctor.ApplicationUser.FirstName} {c.Case.Doctor.ApplicationUser.LastName}",
                            ProfileImage = GetDoctroProfileImage(c.Case.DoctorId),
                        } : new OwnerInfo()
                        {
                            Name = $"{c.Case.Patient.ApplicationUser.FirstName} {c.Case.Patient.ApplicationUser.LastName}",
                            ProfileImage = string.Empty,
                        },
                        CreationDate = c.CreationDate
                    }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        private string GetDoctroProfileImage(Guid doctorId)
        {

            var userAttachements = _attachmentComponent.GetFilesByOwnerId(doctorId);
            return userAttachements.Where(x => x.AttachmentTypeId == (int)AttachmentTypeEnum.DoctorProfileImage).OrderByDescending(c => c.CreationDate).Select(x => x.GeneratedFileName).FirstOrDefault();
        }

        private List<string> GetCaseAttchments(Guid caseId)
        {

            var userAttachements = _attachmentComponent.GetFilesByOwnerId(caseId);
            return userAttachements.Where(x => x.AttachmentTypeId == (int)AttachmentTypeEnum.PatientAttachments).Select(x => x.GeneratedFileName).ToList();
        }
        private List<string> GetCaseAttchmentsInterpreted(Guid caseId)
        {
            List<Entities.Domain.Attachment> userAttachements = _attachmentComponent.GetFilesByOwnerId(caseId);
            return userAttachements.Where(x => x.AttachmentTypeId == (int)AttachmentTypeEnum.MedicalReportDocument).Select(x => x.GeneratedFileName).ToList();
        }
    }
}
