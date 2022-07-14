using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Components.ViewModels;
using Origin.YMC.Business.Entities.Domain.Countries;
using Origin.YMC.Business.Entities.Domain.Interpreter.Enums;
using Origin.YMC.Business.Entities.Domain.Patients;
using Origin.YMC.Business.Entities.Domain.Patients.Enums;
using Origin.YMC.Business.Entities.Domain.Patients.ViewModel;
using Origin.YMC.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Origin.YMC.Business.Components.Implementation
{
    public class CaseComponent : ICaseComponent
    {
        private readonly IRepository<Case> _caseRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<CasePatientQuestionsAnswers> _caseQuestionsRepository;
        private readonly IRepository<PatientPhysician> _PatientPhysicianRepository;
        private readonly IUserComponent _userComponent;
        private readonly ILogComponent _logComponent;
        private readonly IAttachmentComponent _attachmentComponent;

        public CaseComponent(
            IRepository<CasePatientQuestionsAnswers> caseQuestionsRepository,
            IRepository<PatientPhysician> PatientPhysicianRepository,
            IRepository<Case> caseRepository,
            IUserComponent userComponent,
            IAttachmentComponent attachmentComponent,
            ILogComponent logComponent,
            IRepository<Country> countryRepository)
        {
            _userComponent = userComponent;
            _caseRepository = caseRepository;
            _caseQuestionsRepository = caseQuestionsRepository;
            _logComponent = logComponent;
            _PatientPhysicianRepository = PatientPhysicianRepository;
            _attachmentComponent = attachmentComponent;
            _countryRepository = countryRepository;
        }
        public void UpdateCaseStatus(Guid Id)
        {
            try
            {
                Case case1 = _caseRepository.Query().FirstOrDefault(x => x.Id == Id);
                if (case1.Status != StatusEnum.CaseUnderInterpretation)
                {
                    case1.Status = Entities.Domain.Patients.Enums.StatusEnum.Submitted;
                }
                _caseRepository.Update(case1);
                _caseRepository.UnitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<CaseListingViewModel> GetCasesByPatientId(Guid PatientId)
        {
            try
            {
                List<CaseListingViewModel> cases = _caseRepository.GetAll().Where(x => x.PatientId == PatientId).OrderByDescending(c => c.CreationDate).Select(x => new CaseListingViewModel
                {
                    CaseId = x.Id,
                    PatientName = x.Patient.ApplicationUser.FirstName + " " + x.Patient.ApplicationUser.LastName,
                    ConsultationType = x.Doctor.Specialty.TitleEn,
                    Status = x.Status,
                    OpinionID = x.OpinionID
                }).ToList();
                return cases;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<CaseListingViewModel> GetCasesByDoctorId(Guid doctorId)
        {
            try
            {
                List<CaseListingViewModel> cases = _caseRepository.GetAll().Where(x => x.DoctorId == doctorId).OrderByDescending(c => c.CreationDate).Select(x => new CaseListingViewModel
                {
                    CaseId = x.Id,
                    PatientName = x.Patient.ApplicationUser.FirstName + " " + x.Patient.ApplicationUser.LastName,
                    ConsultationType = x.Doctor.Specialty.TitleEn,
                    Status = x.Status,
                    StatusDisplayName = x.Status.ToString(),
                    OpinionID = x.OpinionID
                }).ToList();
                return cases;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Guid GetCasePatientId(Guid CaseId)
        {
            try
            {
                Guid PatientId = _caseRepository.Query().FirstOrDefault(x => x.Id == CaseId).PatientId;

                return PatientId;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public Guid GetCaseDoctorId(Guid CaseId)
        {
            try
            {
                Guid doctor = _caseRepository.Query().FirstOrDefault(x => x.Id == CaseId).DoctorId;

                return doctor;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Guid Add(CaseViewModel Case)
        {
            try
            {
                int? opinionId = _caseRepository.Query().OrderByDescending(a => a.OpinionID).FirstOrDefault()?.OpinionID;
                int opId = (opinionId ?? 0) + 133;
                var newCaseId = Guid.NewGuid();
                _caseRepository.Add(new Case
                {
                    Id = newCaseId,
                    DoctorId = Case.DoctorId,
                    Status = StatusEnum.Paid,
                    PatientId = Case.PatientId,
                    CreatedBy = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    OpinionID = opId,
                    InterpretationTypeId = (InterpretationTypes)Enum.Parse(typeof(InterpretationTypes), Case.InterpreterStatus.ToString())
                });
                _caseRepository.UnitOfWork.SaveChanges(true);

                return newCaseId;
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
        public void UpdateCase(CasePatientInfoViewModel casePatientInfo)
        {
            try
            {
                Case caseToUpdate = _caseRepository.Query().FirstOrDefault(x => x.Id == casePatientInfo.CaseId);
                caseToUpdate.FirstName = casePatientInfo.FirstName;
                caseToUpdate.MiddleName = casePatientInfo.MiddleName;
                caseToUpdate.LastName = casePatientInfo.LastName;
                caseToUpdate.Saluation = casePatientInfo.Saluation;
                caseToUpdate.Address1 = casePatientInfo.Address1;
                caseToUpdate.Address2 = casePatientInfo.Address2;
                caseToUpdate.CountryId = _countryRepository.Query().FirstOrDefault(x=>x.Code == casePatientInfo.CountryId)?.Id ;
                caseToUpdate.Region = casePatientInfo.Region;
                caseToUpdate.CityId = casePatientInfo.CityId;
                caseToUpdate.ZipCode = casePatientInfo.ZipCode;
                caseToUpdate.PrimaryPhoneType = casePatientInfo.PrimaryPhoneType;
                caseToUpdate.PrimaryPhone = casePatientInfo.PrimaryPhone;
                caseToUpdate.SecondaryPhoneType = casePatientInfo.SecondaryPhoneType;
                caseToUpdate.SecondaryPhone = casePatientInfo.SecondaryPhone;
                caseToUpdate.Email = casePatientInfo.Email;
                caseToUpdate.BirthDate = casePatientInfo.BirthDate;
                caseToUpdate.Gender = casePatientInfo.Gender;

                _caseRepository.Update(caseToUpdate);
                _caseRepository.UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }

        }

        public CaseDetailsViewModel CaseDetails(Guid caseId)
        {

            try
            {
                IQueryable<Case> caseInfo = _caseRepository.GetAll();
                CaseDetailsViewModel result = new CaseDetailsViewModel
                {
                    CaseInfo = caseInfo.Where(x => x.Id == caseId).Select(x => new CaseListingViewModel
                    {
                        CaseId = x.Id,
                        PatientName = x.Patient.ApplicationUser.FirstName + " " + x.Patient.ApplicationUser.LastName,
                        ConsultationType = x.Doctor.Specialty.TitleEn,
                        Status = x.Status,
                        StatusDisplayName = x.Status.ToString(),
                        OpinionID = x.OpinionID,
                        DoctorID = x.DoctorId,
                        PatientUserId = x.Patient.ApplicationUserId,
                        DoctorUserId = x.Doctor.ApplicationUserId,
                        DoctorName = x.Doctor.ApplicationUser.FirstName + " "+ x.Doctor.ApplicationUser.LastName,

                        PatientId = x.PatientId,
                        InterpreterId = x.AssignedToInterpreterId,
                        DoctorReportDiagnosis = x.DoctorReportDiagnosis,
                        AssignedToInterpreterId = x.AssignedToInterpreterId,
                        InterpretationTypeId = x.InterpretationTypeId,


                    }).FirstOrDefault(),
                    CasePatientInfo = caseInfo.Any(x=>x.Id == caseId)  ? caseInfo.Include(x => x.Patient).Where(x => x.Id == caseId)
                    .ToList().Select(x => new CasePatientInfoViewModel
                    {
                        FirstName = x.FirstName,
                        Email = x.Email,
                        Address1 = x.Address1,
Address2 = x.Address2,
BirthDate = x.BirthDate,
CaseId = caseId,
CityId = x.CityId,
CountryId = x.CountryId.HasValue ? _countryRepository.Query().FirstOrDefault(c=>c.Id ==  x.CountryId).Code:"",
Gender = x.Gender,
LastName = x.LastName,
MiddleName = x.MiddleName,
PrimaryPhone = x.PrimaryPhone,
PrimaryPhoneType = x.PrimaryPhoneType,
Region = x.Region,
Saluation= x.Saluation,
SecondaryPhone = x.SecondaryPhone,
SecondaryPhoneType = x.SecondaryPhoneType,
ZipCode = x.ZipCode
                    }).FirstOrDefault():new CasePatientInfoViewModel {CaseId = caseId },
                    CasePatientQuestions = _caseQuestionsRepository.Query().Where(c => c.CaseId == caseId).Select(c => new CasePatientQuestionsViewModel()
                    {
                        AreYouCurrentlyHospitalized = c.AreYouCurrentlyHospitalized,
                        DiagnosisDate = c.DiagnosisDate,
                        DoYouHadLitigation = c.DoYouHadLitigation,
                        DoYouHadWorkerCompensation = c.DoYouHadWorkerCompensation,
                        DoYouHaveSurgery = c.DoYouHaveSurgery,
                        EthnicOrigin = c.EthnicOrigin,
                        EthnicOriginDisplayName = c.EthnicOrigin.ToString(),
                        FirstQuestion = c.FirstQuestion,
                        SecondQuestion = c.SecondQuestion,
                        ThirdQuestion = c.ThirdQuestion,
                        MedicalDiagnosis = c.MedicalDiagnosis,
                        NoneOfTheQuestions = c.NoneOfTheQuestions,
                        OtherEthinicOrigin = c.OtherEthinicOrigin,
                        PatientWeight = c.PatientWeight,
                        PatientAge = c.PatientAge,
                        PatientHeight = c.PatientHeight,
                        PatientPhysicians = c.PatientPhysicians,
                        PrimaryCheifProblem = c.PrimaryCheifProblem,
                        SurgeryDates = c.SurgeryDates,
                        TreatmentRecommendations = c.TreatmentRecommendations,
                        CaseId = c.CaseId,
                        FirstQuestionInterpreted = c.FirstQuestionInterpreted,
                        SecondQuestionInterpreted = c.SecondQuestionInterpreted,
                        ThirdQuestionInterpreted = c.ThirdQuestionInterpreted,
                        MedicalDiagnosisInterpreted = c.MedicalDiagnosisInterpreted,
                        PrimaryCheifProblemInterpreted = c.PrimaryCheifProblemInterpreted
                    }).FirstOrDefault()
                };
                if (result.CasePatientQuestions != null)
                {
                    result.CasePatientQuestions.EthnicOriginDisplayName = Enum.GetName(typeof(EthnicOriginEnum), result.CasePatientQuestions.EthnicOrigin);
                }
                 result.Attchments = GetPatientAttchments(caseId);
                result.CaseDocuments = GetCaseAttchments(caseId);
                result.CaseDocumentsInterpreted = GetCaseAttchmentsInterpreted(caseId);

                return result;
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }


        }


        private List<string> GetPatientAttchments(Guid caseId)
        {
            List<Entities.Domain.Attachment> userAttachements = _attachmentComponent.GetFilesByOwnerId(caseId);
            return userAttachements.Where(x => x.AttachmentTypeId == (int)AttachmentTypeEnum.PatientAttachments).Select(x => x.GeneratedFileName).ToList();
        }

        private List<string> GetCaseAttchments(Guid caseId)
        {
            List<Entities.Domain.Attachment> userAttachements = _attachmentComponent.GetFilesByOwnerId(caseId);
            return userAttachements.Where(x => x.AttachmentTypeId == (int)AttachmentTypeEnum.CaseDocuments).Select(x => x.GeneratedFileName).ToList();
        }
        private List<string> GetCaseAttchmentsInterpreted(Guid caseId)
        {
            List<Entities.Domain.Attachment> userAttachements = _attachmentComponent.GetFilesByOwnerId(caseId);
            return userAttachements.Where(x => x.AttachmentTypeId == (int)AttachmentTypeEnum.MedicalReportDocument).Select(x => x.GeneratedFileName).ToList();
        }
        public void AddDoctorCaseReport(string doctorReportDiagnosis, Guid caseId, string firstQuestion, string secondQuestion, string thirdQuestion)
        {
            try
            {
                Case caseToUpdate = _caseRepository.Query().FirstOrDefault(x => x.Id == caseId);
                if (caseToUpdate != null)
                {
                    caseToUpdate.DoctorReportDiagnosis = doctorReportDiagnosis;
                    caseToUpdate.Status =
                        caseToUpdate.InterpretationTypeId == InterpretationTypes.TranslateCaseAndMedicalReport
                            ? StatusEnum.MedicalReportUnderInterpretation
                            : StatusEnum.Closed;
                    if (caseToUpdate.Status == StatusEnum.Closed)
                    {
                        caseToUpdate.ClosureDate = DateTime.Now.ToUniversalTime().AddHours(2);
                    }
                    var caseQuest = _caseQuestionsRepository.GetAll().FirstOrDefault(c => c.CaseId == caseId);
                    caseQuest.FirstQuestionInterpreted = firstQuestion;
                    caseQuest.SecondQuestionInterpreted = secondQuestion;
                    caseQuest.ThirdQuestionInterpreted = thirdQuestion;
                    _caseRepository.Update(caseToUpdate);
                }

                _caseRepository.UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public List<CaseListingViewModel> GetCasesByInterpreterId(Guid interpreterId)
        {
            try
            {
                List<CaseListingViewModel> cases = _caseRepository.GetAll().OrderByDescending(c => c.CreationDate).Where(x => x.AssignedToInterpreterId == interpreterId).Select(x => new CaseListingViewModel
                {
                    CaseId = x.Id,
                    PatientName = x.Patient.ApplicationUser.FirstName + " " + x.Patient.ApplicationUser.LastName,
                    ConsultationType = x.Doctor.Specialty.TitleEn,
                    Status = x.Status,
                    ConsultationDate = x.CreationDate
                }).ToList();
                return cases;
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);

                throw;
            }
        }

        public bool AddInterpretation(CasePatientQuestionsViewModel casePatientQuestionsViewModel, Guid caseId)
        {
            try
            {
                CasePatientQuestionsAnswers caseQuestionsToUpdate = _caseQuestionsRepository.Query().FirstOrDefault(x => x.CaseId == caseId);
                if (caseQuestionsToUpdate != null)
                {
                    caseQuestionsToUpdate.FirstQuestionInterpreted = casePatientQuestionsViewModel.FirstQuestionInterpreted;
                    caseQuestionsToUpdate.MedicalDiagnosisInterpreted = casePatientQuestionsViewModel.MedicalDiagnosisInterpreted;
                    caseQuestionsToUpdate.PrimaryCheifProblemInterpreted = casePatientQuestionsViewModel.PrimaryCheifProblemInterpreted;
                    caseQuestionsToUpdate.SecondQuestionInterpreted = casePatientQuestionsViewModel.SecondQuestionInterpreted;
                    caseQuestionsToUpdate.ThirdQuestionInterpreted = casePatientQuestionsViewModel.ThirdQuestionInterpreted;

                    _caseQuestionsRepository.Update(caseQuestionsToUpdate);

                    Case caseToUpdate = _caseRepository.Query()
                        .FirstOrDefault(x => x.Id == caseId);
                    if (caseToUpdate != null)
                    {
                        caseToUpdate.Status = StatusEnum.Submitted;
                        _caseRepository.Update(caseToUpdate);
                    }
                }

                return _caseRepository.UnitOfWork.SaveChanges() >= 0;
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }

        }

        public void AddDoctorCaseReportInterpreter(string doctorReportDiagnosis, Guid caseId)
        {
            try
            {
                Case caseToUpdate = _caseRepository.Query().FirstOrDefault(x => x.Id == caseId);
                if (caseToUpdate != null)
                {
                    caseToUpdate.DoctorReportDiagnosisInterpreted = doctorReportDiagnosis;
                    caseToUpdate.Status = StatusEnum.Closed;

                    _caseRepository.Update(caseToUpdate);
                }

                _caseRepository.UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}
