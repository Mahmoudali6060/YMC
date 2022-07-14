using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.Interpreter.Enums
{
   public enum InterpretationTypes
    {
        NoInterpretationService = 1,
        TranslateMedicalReportOnly=2,
        TranslateCaseAndMedicalReport=3
    }
}
