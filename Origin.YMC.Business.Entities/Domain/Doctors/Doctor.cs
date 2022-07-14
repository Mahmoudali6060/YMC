using Origin.YMC.Business.Entities.Domain.Payments;
using Origin.YMC.Business.Entities.Domain.Specialties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.Doctors
{
    public class Doctor : EntityBase
    {
        [ForeignKey("Specialty")]
        public Guid SpecialtieId { get; set; }
        public virtual Specialty Specialty { get; set; }

        [ForeignKey("ApplicationUser")]
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Payment")]
        public Guid PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

        public string Refrances { get; set; }
        public string Experinces { get; set; }

        public string Bio { get; set; }
        public decimal Fees { get; set; }

        public int ResponsTime { get; set; }

        public string FocusOfScientific { get; set; }

        public string OnlineProfileLink { get; set; }
    }
}
