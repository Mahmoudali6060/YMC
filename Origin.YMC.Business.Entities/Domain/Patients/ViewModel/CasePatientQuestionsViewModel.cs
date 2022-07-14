using Origin.YMC.Business.Entities.Domain.Patients.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.Patients.ViewModel
{
   public class CasePatientQuestionsViewModel
    {
        public Guid CaseId { get; set; }
        [Required]
        public string PrimaryCheifProblem { get; set; }
        public string PrimaryCheifProblemInterpreted { get; set; }
        public string MedicalDiagnosisInterpreted { get; set; }
        public string FirstQuestionInterpreted { get; set; }
        public string SecondQuestionInterpreted { get; set; }
        public string ThirdQuestionInterpreted { get; set; }
        [Required]

        public string MedicalDiagnosis { get; set; }
        [Required]
        public DateTime DiagnosisDate { get; set; }

        public bool DoYouHadWorkerCompensation { get; set; }
        public bool DoYouHadLitigation { get; set; }
        public bool AreYouCurrentlyHospitalized { get; set; }
        public bool DoYouHaveSurgery { get; set; }

        public string SurgeryDates { get; set; }
        public bool NoneOfTheQuestions { get; set; }
        [Required]
        public string TreatmentRecommendations { get; set; }
        [Required]
        public string FirstQuestion { get; set; }
        [Required]
        public string SecondQuestion { get; set; }
        [Required]
        public string ThirdQuestion { get; set; }
        [Required]
 
        public string PatientAge { get; set; }
        [Required]
 
        public string PatientHeight { get; set; }

        [Required]
 
        public string PatientWeight { get; set; }

        public EthnicOriginEnum EthnicOrigin { get; set; }
        public string OtherEthinicOrigin { get; set; }
        public  List<PatientPhysician> PatientPhysicians { get; set; }
        public string EthnicOriginDisplayName { get; set; }
    }
}
