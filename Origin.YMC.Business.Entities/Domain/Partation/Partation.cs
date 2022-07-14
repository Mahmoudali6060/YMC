using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace Origin.YMC.Business.Entities.Domain.Partation
{
    public class Partation : EntityBase
    {

        public string ContentAr { get; set; }
        public string ContentEn { get; set; }
        [ForeignKey("PartationType")]
        public Guid PartationTypeID { get; set; }
        public virtual PartationType PartationType { set; get; }
    }
}
