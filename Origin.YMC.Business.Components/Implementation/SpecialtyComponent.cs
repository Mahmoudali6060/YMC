using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.Specialties;
using Origin.YMC.Business.Entities.Domain.Specialties.ViewModels;
using Origin.YMC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Components.Implementation
{
    public class SpecialtyComponent : ISpecialtyComponent
    {
        private readonly IRepository<Specialty> _specialtyRepository;
        private readonly IAttachmentComponent _attachmentComponent;
        private readonly ILogComponent _logComponent;

        public SpecialtyComponent(
            IRepository<Specialty> specialtyRepository,
            IAttachmentComponent attachmentComponent,
            ILogComponent logComponent)
        {
            _specialtyRepository = specialtyRepository;
            _attachmentComponent = attachmentComponent;
            _logComponent = logComponent;
        }


        public void Add(Specialty model)
        {
            _specialtyRepository.Add(model);
            _specialtyRepository.UnitOfWork.SaveChanges();
        }

        public void Create(SpecialtyViewModels model)
        {
            try
            {

                var specialty_ = new Specialty
                {
                    TitleEn = model.TitleEn,
                    TitleAr = model.TitleAr,
                    DescriptionAr = model.DescriptionAr,
                    DescriptionEn = model.DescriptionEn,
                    AttachmentId = model.AttachmentId,
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    LastUpdatedAt = DateTime.Now,
                    CreatedBy = Guid.NewGuid(),
                    LastUpdatedBy = Guid.NewGuid()
                };
                Add(specialty_);

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
            finally
            { }
        }

        public void Deactive_Activate(Guid id)
        {
            //Edit
            var old_ = _specialtyRepository.Get(id);
            if (old_.IsActive)
                old_.IsActive = false;
            else
                old_.IsActive = true;

            _specialtyRepository.Update(old_);
            _specialtyRepository.UnitOfWork.SaveChanges();
        }

        public SpecialtyViewModels Get(Guid id)
        {
            var md = _specialtyRepository.Query().FirstOrDefault(c => c.Id == id);
            return new SpecialtyViewModels
            {
                TitleEn = md.TitleEn,
                TitleAr = md.TitleAr,
                DescriptionAr = md.DescriptionAr,
                DescriptionEn = md.DescriptionEn,
                AttachmentId = md.AttachmentId,
                Id = md.Id,
                CreatedBy = md.CreatedBy,
                CreationDate = md.CreationDate,
                LastUpdatedAt = md.LastUpdatedAt,
                LastUpdatedBy = md.LastUpdatedBy
            };
        }

        public List<SpecialtyViewModels> GetList(string query)
        {
            var images = _specialtyRepository.Query()
                  .Where(c => c.IsActive)
                  .Where(x => string.IsNullOrEmpty(query))
                  .Select(c => new SpecialtyViewModels
                  {
                      TitleEn = c.TitleEn,
                      TitleAr = c.TitleAr,
                      DescriptionAr = c.DescriptionAr,
                      DescriptionEn = c.DescriptionEn,
                      AttachmentId = c.AttachmentId,
                      Id = c.Id,
                      CreatedBy = c.CreatedBy,
                      CreationDate = c.CreationDate,
                      IsActive = c.IsActive,
                      LastUpdatedAt = c.LastUpdatedAt,
                      LastUpdatedBy = c.LastUpdatedBy
                  }).ToList();
            foreach (var item in images)
            {
                item.ImageUrl = _attachmentComponent.GetFilesById(item.AttachmentId).FirstOrDefault().GeneratedFileName;
            }
            return images;
        }

     

        public ListSpecialtyViewModel GetPageData(string searchValue, int requestStart, int requestLenght, string orderColumsName, int orderColumsDirect)
        {
            var images = _specialtyRepository.Query();

            var filteredData = String.IsNullOrWhiteSpace(searchValue) ?
                                   images :
                                    images.Where(x =>
                                       x.TitleAr.ToLower().Contains(searchValue)
                                    || x.TitleEn.ToLower().Contains(searchValue)
                                    || x.DescriptionAr.ToLower().Contains(searchValue)
                                    || x.DescriptionEn.ToLower().Contains(searchValue));

            switch (orderColumsName.ToLower())
            {
                case "titleAr":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.TitleAr) : filteredData.OrderByDescending(c => c.TitleAr);
                    break;
                case "titleEn":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.TitleEn) : filteredData.OrderByDescending(c => c.TitleEn);
                    break;
                case "descriptionAr":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.DescriptionAr) : filteredData.OrderByDescending(c => c.DescriptionAr);
                    break;
                case "descriptionEn":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.DescriptionEn) : filteredData.OrderByDescending(c => c.DescriptionEn);
                    break;
                default:
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderByDescending(c => c.CreationDate) : filteredData.OrderBy(c => c.CreationDate);
                    break;
            }

            var dataPage = filteredData.Skip(requestStart).Take(requestLenght);

            var viewModel = new ListSpecialtyViewModel();
            viewModel.TotalDatalenght = images.Count();
            viewModel.FilterDatalenght = filteredData.Count();
            viewModel.Items = dataPage.Select(c => new SpecialtyViewModels
            {
                Id = c.Id,
                CreatedBy = c.CreatedBy,
                CreationDate = c.CreationDate,
                IsActive = c.IsActive,
                LastUpdatedAt = c.LastUpdatedAt,
                LastUpdatedBy = c.LastUpdatedBy,
                TitleEn = c.TitleEn,
                TitleAr = c.TitleAr,
                DescriptionAr = c.DescriptionAr,
                DescriptionEn = c.DescriptionEn,
                AttachmentId = c.AttachmentId,
            }).ToList();

            foreach (var item in viewModel.Items)
            {
                item.ImageUrl = _attachmentComponent.GetFilesById(item.AttachmentId).FirstOrDefault().GeneratedFileName;
            }

            return viewModel;
        }

        public void Update(SpecialtyViewModels image)
        {
            //Edit
            var old_ = _specialtyRepository.Get(image.Id);
            old_.TitleEn = image.TitleEn;
            old_.TitleAr = image.TitleAr;
            old_.DescriptionAr = image.DescriptionAr;
            old_.DescriptionEn = image.DescriptionEn;

            if (image.AttachmentId != Guid.Empty)
                old_.AttachmentId = image.AttachmentId;

            old_.LastUpdatedAt = DateTime.Now;

            _specialtyRepository.Update(old_);
            _specialtyRepository.UnitOfWork.SaveChanges();
        }
    }
}
