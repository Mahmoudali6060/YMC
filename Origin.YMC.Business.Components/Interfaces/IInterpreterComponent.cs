using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origin.YMC.Business.Entities.Domain.Interpreter.ViewModels;

namespace Origin.YMC.Business.Components.Interfaces
{
   public interface IInterpreterComponent
   {
       void Add(InterpreterViewModels interpreter);
       ListInterpreterViewModel GetPageData(string searchValue, int requestStart, int requestLenght, string orderColumsName, int orderColumsDirect);

       bool ActivateDeactivateInterpreter(Guid id);
       decimal GetInterpretationFees(int interpretationTypeId);
       int ChangeInterpreter(Guid caseId, Guid interpreterId);
       Guid AssignInterpreterToCase(Guid caseId);
       Guid GetInterpreterId(Guid userId);
       Guid GetUserId(Guid interpreterId);

    }
}
