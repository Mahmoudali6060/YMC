using Origin.YMC.Business.Entities.Domain.Payments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Origin.YMC.Business.Entities.Domain.Patients
{
    public class Patient : EntityBase
    {

        [ForeignKey("ApplicationUser")]
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Payment")]
        public Guid PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
