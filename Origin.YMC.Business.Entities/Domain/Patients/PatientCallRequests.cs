using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.Patients
{
   public class PatientCallRequests : EntityBase
    {
        [ForeignKey("Case")]
        public Guid CaseId { get; set; }
        public decimal Fees { get; set; }
        public string ZoomUrl { get; set; }
        public bool IsConfirmed { get; set; }
        public virtual Case Case { get; set; }
        public DateTime? MeetingDate { get; set; }
        public TimeSpan? MeetingTime { get; set; }
    }
}
