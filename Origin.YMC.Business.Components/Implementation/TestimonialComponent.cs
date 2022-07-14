using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.Testimonials;
using Origin.YMC.Business.Entities.Domain.Testimonials.ViewModels;
using Origin.YMC.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Origin.YMC.Business.Components.ViewModels;


namespace Origin.YMC.Business.Components.Implementation
{
    public class TestimonialComponent : ITestimonialComponent
    {
        private readonly IRepository<Testimonial> _testimonialRepository;
        private readonly ILogComponent _logComponent;
        private readonly IAttachmentComponent _attachmentComponent;

        public TestimonialComponent(
            IRepository<Testimonial> testimonialRepository,
            ILogComponent logComponent, IAttachmentComponent attachmentComponent)
        {
            _testimonialRepository = testimonialRepository;
            _logComponent = logComponent;
            _attachmentComponent = attachmentComponent;
        }

        public int Add(Testimonial testimonial)
        {
            _testimonialRepository.Add(testimonial);
          int status=  _testimonialRepository.UnitOfWork.SaveChanges();
          if (status==1)
          {
              var oldtestimonial = _testimonialRepository.Get(testimonial.Id);
              oldtestimonial.IsActive = false;
              _testimonialRepository.Update(oldtestimonial);
              _testimonialRepository.UnitOfWork.SaveChanges();

            }
            return status;
        }

        public void Create(TestimonialViewModel model)
        {
            try
            {

                var testimonial = new Testimonial
                {
                    PatientId = model.PatientId,
                    TextReview = model.TextReview,
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    LastUpdatedAt = DateTime.Now,
                    CreatedBy = Guid.NewGuid(),
                    LastUpdatedBy = Guid.NewGuid()
                };
                Add(testimonial);

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
            finally
            { }
        }

        public List<TestimonialViewModel> GetList(string query)
        {

            var lsTestimonial = _testimonialRepository.Query()
                .Where(c => c.IsActive)
                .Where(x => string.IsNullOrEmpty(query))
                .OrderByDescending(a => a.CreationDate)
                .Include(a=>a.Patient)
                .ToList();
            var lstTestimonialViewModel = new List<TestimonialViewModel>();
            foreach (var item in lsTestimonial)
            {
                lstTestimonialViewModel.Add(new TestimonialViewModel
                {
                    Id = item.Id,
                    PatientId = item.PatientId,
                    TextReview = item.TextReview,
                    PatientName = item.Patient.ApplicationUser.FirstName + " " + item.Patient.ApplicationUser.LastName,
                    CreatedBy = item.CreatedBy,
                    CreationDate = item.CreationDate,
                    IsActive = item.IsActive,
                    LastUpdatedAt = item.LastUpdatedAt,
                    LastUpdatedBy = item.LastUpdatedBy,
                    PatientImageURL =_attachmentComponent.GetFilesByOwnerId(item.Patient.ApplicationUserId,(int)AttachmentTypeEnum.PatientAttachments)?.GeneratedFileName
                });
                  
            }
                    
                ;
            return lstTestimonialViewModel;
        }

        public TestimonialViewModel Get(Guid id)
        {
            var md = _testimonialRepository.Query().FirstOrDefault(c => c.Id == id);
            return new TestimonialViewModel
            {
                Id = md.Id,
                PatientId = md.PatientId,
                PatientName = md.Patient.ApplicationUser.FirstName + " " + md.Patient.ApplicationUser.LastName,
                TextReview = md.TextReview,
                CreatedBy = md.CreatedBy,
                CreationDate = md.CreationDate,
                LastUpdatedAt = md.LastUpdatedAt,
                LastUpdatedBy = md.LastUpdatedBy
            };
        }

        public ListTestimonialViewModel GetPageData(string searchValue, int requestStart, int requestLenght, string orderColumsName, int orderColumsDirect)
        {
            var testimonials = _testimonialRepository.Query();

            var filteredData = String.IsNullOrWhiteSpace(searchValue) ?
                                   testimonials :
                                     testimonials.Where(x =>
                                         x.TextReview.ToLower().Contains(searchValue));

            switch (orderColumsName.ToLower())
            {
                case "textReview":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.TextReview) : filteredData.OrderByDescending(c => c.TextReview);
                    break;
                default:
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderByDescending(c => c.CreationDate) : filteredData.OrderBy(c => c.CreationDate);
                    break;
            }

            var dataPage = filteredData.Skip(requestStart).Take(requestLenght);

            var viewModel = new ListTestimonialViewModel();
            viewModel.TotalDatalenght = testimonials.Count();
            viewModel.FilterDatalenght = filteredData.Count();
            viewModel.Items = dataPage.Select(c => new TestimonialViewModel
            {
                Id = c.Id,
                PatientId = c.PatientId,
                PatientName = c.Patient.ApplicationUser.FirstName + " " + c.Patient.ApplicationUser.LastName,
                TextReview = c.TextReview,
                CreatedBy = c.CreatedBy,
                CreationDate = c.CreationDate,
                IsActive = c.IsActive,
                LastUpdatedAt = c.LastUpdatedAt,
                LastUpdatedBy = c.LastUpdatedBy
            }).ToList();
            return viewModel;
        }

        public void Update(TestimonialViewModel testimonial)
        {
            //Edit
            var oldtestimonial = _testimonialRepository.Get(testimonial.Id);

            oldtestimonial.PatientId = testimonial.PatientId;
            oldtestimonial.TextReview = testimonial.TextReview;
            oldtestimonial.LastUpdatedAt = DateTime.Now;


            _testimonialRepository.Update(oldtestimonial);
            _testimonialRepository.UnitOfWork.SaveChanges();
        }

        public void Deactive_Activate(Guid id)
        {
            //Edit
            var oldtestimonial = _testimonialRepository.Get(id);
            if (oldtestimonial.IsActive)
                oldtestimonial.IsActive = false;
            else
                oldtestimonial.IsActive = true;

            _testimonialRepository.Update(oldtestimonial);
            _testimonialRepository.UnitOfWork.SaveChanges();
        }
    }
}
