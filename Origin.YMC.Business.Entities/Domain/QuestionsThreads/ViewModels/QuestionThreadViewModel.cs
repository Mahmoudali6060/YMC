using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.QuestionsThreads.ViewModels
{
    public class QuestionThreadViewModel
    {
        public OwnerInfo ownerInfo;
        public Guid CaseId { get; set; }
        public Guid OwnerId { get; set; }
        public OwnerType OwnerType { get; set; }
        public string ChatText { get; set; }
        public DateTime CreationDate { get; set; }
    }

    public class OwnerInfo
    {
        public string Name { get; set; }
        public string ProfileImage { get; set; }

    }
}
