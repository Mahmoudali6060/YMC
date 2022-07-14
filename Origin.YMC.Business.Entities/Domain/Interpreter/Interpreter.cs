using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origin.YMC.Business.Entities.Domain.Patients;
using Origin.YMC.Business.Entities.Domain.Specialties;

namespace Origin.YMC.Business.Entities.Domain.Interpreter
{
  public class Interpreter: EntityBase
    {
        //[ForeignKey("Specialty")]
        //public Guid? SpecialtieId { get; set; }
        //public virtual Specialty Specialty { get; set; }

        [ForeignKey("ApplicationUser")]
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual List<Case> Cases { get; set; }
    }
}
