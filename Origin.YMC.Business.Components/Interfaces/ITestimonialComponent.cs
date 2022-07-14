using Origin.YMC.Business.Entities.Domain.Testimonials;
using Origin.YMC.Business.Entities.Domain.Testimonials.ViewModels;
using System;
using System.Collections.Generic;

namespace Origin.YMC.Business.Components.Interfaces
{
    public interface ITestimonialComponent
    {
        int Add(Testimonial page);
        void Update(TestimonialViewModel vision);
        void Create(TestimonialViewModel model);
        TestimonialViewModel Get(Guid id);
        void Deactive_Activate(Guid id);
        List<TestimonialViewModel> GetList(string query);
        ListTestimonialViewModel GetPageData(string searchValue, int requestStart, int requestLenght, string orderColumsName, int orderColumsDirect);
    }
}
