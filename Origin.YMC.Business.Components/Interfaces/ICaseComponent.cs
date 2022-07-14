using Origin.YMC.Business.Entities.Domain.Doctors.ViewModel;
using Origin.YMC.Business.Entities.Domain.Patients.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origin.YMC.Business.Entities.Domain.Patients;

namespace Origin.YMC.Business.Components.Interfaces
{
    public interface ICaseComponent
    {
        Guid Add(CaseViewModel Case);
        void UpdateCase(CasePatientInfoViewModel casePatientInfo);
        void UpdateCaseStatus(Guid caseId);
        //Case GetCaseById(Guid CaseId);

        Guid GetCaseDoctorId(Guid CaseId);
        Guid GetCasePatientId(Guid CaseId);
        List<CaseListingViewModel> GetCasesByPatientId(Guid PatientId);
        List<CaseListingViewModel> GetCasesByDoctorId(Guid doctorId);
        CaseDetailsViewModel CaseDetails(Guid caseId);
        void AddDoctorCaseReport(string doctorReportDiagnosis, Guid caseId, string firstQuestion, string secondQuestion, string thirdQuestion);
        void AddDoctorCaseReportInterpreter(string doctorReportDiagnosis, Guid caseId);

        List<CaseListingViewModel> GetCasesByInterpreterId(Guid interpreterId);
        bool AddInterpretation(CasePatientQuestionsViewModel casePatientQuestionsViewModel, Guid caseId);

    }
}
