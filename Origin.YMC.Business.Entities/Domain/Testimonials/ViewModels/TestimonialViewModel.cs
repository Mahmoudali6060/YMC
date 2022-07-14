using System;
using System.Collections.Generic;

namespace Origin.YMC.Business.Entities.Domain.Testimonials.ViewModels
{
    public class TestimonialViewModel : EntityBase
    {
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }
        public string TextReview { get; set; }
        public string PatientImageURL { get; set; }
    }

    public class ListTestimonialViewModel
    {
        public List<TestimonialViewModel> Items { get; set; }
        public int TotalDatalenght { get; set; }
        public int FilterDatalenght { get; set; }
        public ListTestimonialViewModel()
        {
            Items = new List<TestimonialViewModel>();
        }
    }
}

