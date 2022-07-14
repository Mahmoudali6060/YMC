using Origin.YMC.Business.Entities.Domain.Patients;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.QuestionsThreads
{
    public class QuestionThread : EntityBase
    {
        [ForeignKey("Case")]
        public Guid CaseId { get; set; }
        public Case Case { get; set; }

        public Guid OwnerId { get; set; }
        public OwnerType OwnerType { get; set; }
        public string ChatText { get; set; }
    }

    public enum OwnerType
    {
        Doctor = 1,
        Patient = 2
    }
}
