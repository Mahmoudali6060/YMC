using Origin.YMC.Business.Entities.Domain.Patients.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.Patients
{
   public class CasePatientQuestionsAnswers:EntityBase
    {
        public Guid CaseId { get; set; }

        public string PrimaryCheifProblem { get; set; }
        public string PrimaryCheifProblemInterpreted { get; set; }

        public string MedicalDiagnosis { get; set; }
        public string MedicalDiagnosisInterpreted { get; set; }

        public DateTime DiagnosisDate { get; set; }

        public bool DoYouHadWorkerCompensation { get; set; }
        public bool DoYouHadLitigation { get; set; }
        public bool AreYouCurrentlyHospitalized { get; set; }
        public bool DoYouHaveSurgery { get; set; }
        public string SurgeryDates { get; set; }
        public bool NoneOfTheQuestions { get; set; }

        public string TreatmentRecommendations { get; set; }

        public string FirstQuestion { get; set; }
        public string FirstQuestionInterpreted { get; set; }

        public string SecondQuestion { get; set; }
        public string SecondQuestionInterpreted { get; set; }

        public string ThirdQuestion { get; set; }
        public string ThirdQuestionInterpreted { get; set; }

        public string PatientAge { get; set; }

        public string PatientHeight { get; set; }

        public string PatientWeight { get; set; }

        public EthnicOriginEnum EthnicOrigin { get; set; }

        public string OtherEthinicOrigin { get; set; }
        public virtual List<PatientPhysician> PatientPhysicians { get; set; }
    }
}
