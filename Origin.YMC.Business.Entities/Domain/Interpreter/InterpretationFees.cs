using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.Interpreter
{
   public class InterpretationFees: EntityBase
    {
        public int InterpretationTypeId { get; set; }
        public decimal Fees { get; set; }
    }
}
