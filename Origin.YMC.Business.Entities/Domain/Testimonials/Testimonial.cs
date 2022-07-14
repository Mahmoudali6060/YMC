
using Origin.YMC.Business.Entities.Domain.Patients;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Origin.YMC.Business.Entities.Domain.Testimonials
{
    public class Testimonial : EntityBase
    {
        [ForeignKey("Patient")]
        public Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public string TextReview { get; set; }
    }
}
