using Origin.YMC.Business.Entities.Domain.QuestionsThreads.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Components.Interfaces
{
    public interface IQuestionThreadComponent
    {
        void Add(QuestionThreadViewModel questionThread );
        QuestionViewModel GetQuestionsThreads(Guid caseId);
    }
}
