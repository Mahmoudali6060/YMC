using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Origin.YMC.Business.Entities.Domain.Patients.ViewModel
{
    public class CaseSubmitViewModel
    {
        public string DoctorName { get; set; }
        public string DoctorSpeciality { get; set; }
        public string DoctorPrice { get; set; }
        public Guid DoctorId { get; set; }
        public int DoctorResponseTime { get; set; }
        public Guid CaseId { get; set; }
    }
}