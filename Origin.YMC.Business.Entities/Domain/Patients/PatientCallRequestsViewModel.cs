using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.Patients
{
  public class PatientCallRequestsViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public Guid CaseId { get; set; }

        public decimal Fees { get; set; }
        [DisplayName("Meeting URL")]
        public string ZoomUrl { get; set; }
        public bool IsConfirmed { get; set; }
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }
        public string CaseCode { get; set; }
        public int TotalDatalenght { get; set; }
        public int FilterDatalenght { get; set; }
        public string DoctorName { get; set; }
        [DisplayName("Meeting Date")]
        public DateTime? MeetingDate { get; set; }
        [DisplayName("Meeting Date")]

        public string MeetingDateStr { get; set; }
        [DisplayName("Meeting Time")]
        public TimeSpan? MeetingTime { get; set; }

        public List<PatientCallRequestsViewModel> Items { get; set; }
    }
}
