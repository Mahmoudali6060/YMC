using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.Patients.Enums
{
    public enum StatusEnum
    {
        Initializing = 0,
        Paid = 1,
        CaseUnderInterpretation=2,
        CaseInterpreted=3,
        Submitted = 4,
        Reviewed = 5,
        MedicalReportUnderInterpretation=6,
        Closed = 7
    }
}
