using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.Patients;
using Origin.YMC.Business.Entities.Domain.Patients.ViewModel;
using Origin.YMC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Components.Implementation
{
    public class CaseQuestionsComponent : ICaseQuestionsComponent
    {

        private readonly IRepository<CasePatientQuestionsAnswers> _caseQuestionsRepository;
        private readonly IRepository<PatientPhysician> _PatientPhysicianRepository;

        private readonly IUserComponent _userComponent;
        private readonly ILogComponent _logComponent;
        public CaseQuestionsComponent(
            IRepository<CasePatientQuestionsAnswers> caseQuestionsRepository,
            IRepository<PatientPhysician> PatientPhysicianRepository,
            IUserComponent userComponent,
            ILogComponent logComponent)
        {
            _userComponent = userComponent;
            _caseQuestionsRepository = caseQuestionsRepository;
            _logComponent = logComponent;
            _PatientPhysicianRepository = PatientPhysicianRepository;
        }

        public CaseQuestionsComponent()
        {

        }
        public void UpdatePatientAnswers(CasePatientQuestionsViewModel casePatientQuestionsViewModel)
        {
            try
            {

                _caseQuestionsRepository.Add(new CasePatientQuestionsAnswers
                {
                    CaseId = casePatientQuestionsViewModel.CaseId,
                    AreYouCurrentlyHospitalized = casePatientQuestionsViewModel.AreYouCurrentlyHospitalized,
                    CreationDate = DateTime.Now,
                    CreatedBy = Guid.NewGuid(),
                    DiagnosisDate = casePatientQuestionsViewModel.DiagnosisDate,
                    DoYouHadLitigation=  casePatientQuestionsViewModel.DoYouHadLitigation,
                    DoYouHadWorkerCompensation = casePatientQuestionsViewModel.DoYouHadWorkerCompensation,
                    DoYouHaveSurgery = casePatientQuestionsViewModel.DoYouHaveSurgery,
                    EthnicOrigin = casePatientQuestionsViewModel.EthnicOrigin,
                    FirstQuestion = casePatientQuestionsViewModel.FirstQuestion,
                    SecondQuestion = casePatientQuestionsViewModel.SecondQuestion,
                    ThirdQuestion = casePatientQuestionsViewModel.ThirdQuestion,
                    IsActive = true,
                    MedicalDiagnosis = casePatientQuestionsViewModel.MedicalDiagnosis,
                    NoneOfTheQuestions = casePatientQuestionsViewModel.NoneOfTheQuestions,
                    OtherEthinicOrigin= casePatientQuestionsViewModel.OtherEthinicOrigin,
                    PatientWeight = casePatientQuestionsViewModel.PatientWeight,
                    PatientAge = casePatientQuestionsViewModel.PatientAge,
                    PatientHeight = casePatientQuestionsViewModel.PatientHeight,
                    PatientPhysicians = casePatientQuestionsViewModel.PatientPhysicians,
                    PrimaryCheifProblem = casePatientQuestionsViewModel.PrimaryCheifProblem,
                    SurgeryDates = casePatientQuestionsViewModel.SurgeryDates,
                    TreatmentRecommendations = casePatientQuestionsViewModel.TreatmentRecommendations
                });
                _caseQuestionsRepository.UnitOfWork.SaveChanges();

               
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
