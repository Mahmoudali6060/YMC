using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.Partation;
using Origin.YMC.Business.Entities.Domain.Partation.ViewModels;
using Origin.YMC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Origin.YMC.Business.Components.Implementation
{
    public class PartationTypeComponent : IPartationTypeComponent
    {
        private readonly IRepository<PartationType> _partationTypeRepository;
        private readonly ILogComponent _logComponent;

        public PartationTypeComponent(
            IRepository<PartationType> partationTypeRepository,
            ILogComponent logComponent)
        {
            _partationTypeRepository = partationTypeRepository;
            _logComponent = logComponent;
        }


        public void Add(PartationType partationType)
        {
            _partationTypeRepository.Add(partationType);
            _partationTypeRepository.UnitOfWork.SaveChanges();
        }

        public void Create(PartationTypeViewModel partationType)
        {
            try
            {

                var partationType_ = new PartationType
                {
                    TitleAr = partationType.TitleAr,
                    TitleEn = partationType.TitleEn,
                    OrderPostionAppear = partationType.OrderPostionAppear,
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    LastUpdatedAt = DateTime.Now,
                    CreatedBy = Guid.NewGuid(),
                    LastUpdatedBy = Guid.NewGuid()
                };
                Add(partationType_);

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
            var old_ = _partationTypeRepository.Get(id);
            if (old_.IsActive)
                old_.IsActive = false;
            else
                old_.IsActive = true;

            _partationTypeRepository.Update(old_);
            _partationTypeRepository.UnitOfWork.SaveChanges();
        }

        public PartationTypeViewModel Get(Guid id)
        {
            var md = _partationTypeRepository.Query().FirstOrDefault(c => c.Id == id);
            return new PartationTypeViewModel
            {
                TitleAr = md.TitleAr,
                TitleEn = md.TitleEn,
                OrderPostionAppear = md.OrderPostionAppear,
                Id = md.Id,
                CreatedBy = md.CreatedBy,
                CreationDate = md.CreationDate,
                LastUpdatedAt = md.LastUpdatedAt,
                LastUpdatedBy = md.LastUpdatedBy
            };
        }

        public List<PartationTypeViewModel> GetList(string query)
        {
            return _partationTypeRepository.Query()
                  .Where(x => string.IsNullOrEmpty(query))
                  .Select(c => new PartationTypeViewModel
                  {
                      Id = c.Id,
                      TitleAr = c.TitleAr,
                      TitleEn = c.TitleEn,
                      OrderPostionAppear = c.OrderPostionAppear,
                      CreatedBy = c.CreatedBy,
                      CreationDate = c.CreationDate,
                      IsActive = c.IsActive,
                      LastUpdatedAt = c.LastUpdatedAt,
                      LastUpdatedBy = c.LastUpdatedBy
                  }).ToList();
        }

        public ListPartationTypeViewModel GetPageData(string searchValue, int requestStart, int requestLenght, string orderColumsName, int orderColumsDirect)
        {
            var partationTypes = _partationTypeRepository.Query();

            var filteredData = String.IsNullOrWhiteSpace(searchValue) ?
                                   partationTypes :
                                    partationTypes.Where(x =>
                                       x.TitleAr.ToLower().Contains(searchValue)
                                    || x.TitleEn.ToLower().Contains(searchValue));

            switch (orderColumsName.ToLower())
            {
                case "titleAr":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.TitleAr) : filteredData.OrderByDescending(c => c.TitleAr);
                    break;
                case "titleEn":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.TitleEn) : filteredData.OrderByDescending(c => c.TitleEn);
                    break;
                default:
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderByDescending(c => c.CreationDate) : filteredData.OrderBy(c => c.CreationDate);
                    break;
            }

            var dataPage = filteredData.Skip(requestStart).Take(requestLenght);

            var viewModel = new ListPartationTypeViewModel();
            viewModel.TotalDatalenght = partationTypes.Count();
            viewModel.FilterDatalenght = filteredData.Count();
            viewModel.Items = dataPage.Select(c => new PartationTypeViewModel
            {
                Id = c.Id,
                TitleAr = c.TitleAr,
                TitleEn = c.TitleEn,
                OrderPostionAppear = c.OrderPostionAppear,
                CreatedBy = c.CreatedBy,
                CreationDate = c.CreationDate,
                IsActive = c.IsActive,
                LastUpdatedAt = c.LastUpdatedAt,
                LastUpdatedBy = c.LastUpdatedBy
            }).ToList();
            return viewModel;
        }

        public void Update(PartationTypeViewModel partationType)
        {
            //Edit
            var old_ = _partationTypeRepository.Get(partationType.Id);
            old_.TitleAr = partationType.TitleAr;
            old_.TitleEn = partationType.TitleEn;
            old_.OrderPostionAppear = partationType.OrderPostionAppear;
            old_.LastUpdatedAt = DateTime.Now;
            _partationTypeRepository.Update(old_);
            _partationTypeRepository.UnitOfWork.SaveChanges();
        }

        public PartationTypeViewModel CheckOrderPostionAppearExist(int orderPostionAppear, Guid id)
        {
            var md = _partationTypeRepository.Query().FirstOrDefault(c => c.OrderPostionAppear == orderPostionAppear);
            if (md != null && md.Id == id)
                return null;

            return md != null ? new PartationTypeViewModel
            {
                TitleAr = md.TitleAr,
                TitleEn = md.TitleEn,
                OrderPostionAppear = md.OrderPostionAppear,
                Id = md.Id,
                CreatedBy = md.CreatedBy,
                CreationDate = md.CreationDate,
                LastUpdatedAt = md.LastUpdatedAt,
                LastUpdatedBy = md.LastUpdatedBy
            } : null;
        }
    }
}
