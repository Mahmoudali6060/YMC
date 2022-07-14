using Origin.YMC.Business.Entities.Domain.Patients.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Components.Interfaces
{
    public interface ICaseQuestionsComponent
    {
        void UpdatePatientAnswers(CasePatientQuestionsViewModel casePatientQuestionsViewModel);
    }
}
