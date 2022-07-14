using Origin.YMC.Business.Entities.Domain.Patients.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origin.YMC.Business.Entities.Domain.Interpreter.Enums;

namespace Origin.YMC.Business.Entities.Domain.Patients.ViewModel
{
   public class CaseListingViewModel
    {
        public Guid CaseId { get; set; }

        public string PatientName { get; set; }
        public string DoctorName { get; set; }

        public string ConsultationType { get; set; }

        public StatusEnum Status { get; set; }
        public Guid PatientId { get; set; }
        public Guid PatientUserId { get; set; }
        public Guid DoctorUserId { get; set; }
        public Guid? InterpreterId { get; set; }
        public string StatusDisplayName { get; set; }

        public int OpinionID { get; set; }
        public Guid DoctorID { get; set; }
        public string DoctorReportDiagnosis { get; set; }
        public DateTime ConsultationDate { get; set; }
        public Guid? AssignedToInterpreterId { get; set; }
        public InterpretationTypes InterpretationTypeId { get; set; }
     
    }
}
