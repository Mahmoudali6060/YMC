using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.Partation;
using Origin.YMC.Business.Entities.Domain.Partation.ViewModels;
using Origin.YMC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Origin.YMC.Business.Components.Implementation
{
    public class PartationComponent : IPartationComponent
    {
        private readonly IRepository<Partation> _partationRepository;
        private readonly ILogComponent _logComponent;

        public PartationComponent(
            IRepository<Partation> partationRepository,
            ILogComponent logComponent)
        {
            _partationRepository = partationRepository;
            _logComponent = logComponent;
        }


        public void Add(Partation partation)
        {
            _partationRepository.Add(partation);
            _partationRepository.UnitOfWork.SaveChanges();
        }

        public void Create(PartationViewModel partation)
        {
            try
            {

                var partation_ = new Partation
                {
                    ContentAr = partation.ContentAr,
                    ContentEn = partation.ContentEn,
                    PartationTypeID = partation.PartationTypeID,
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    LastUpdatedAt = DateTime.Now,
                    CreatedBy = Guid.NewGuid(),
                    LastUpdatedBy = Guid.NewGuid()
                };
                Add(partation_);

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
            var old_ = _partationRepository.Get(id);
            if (old_.IsActive)
                old_.IsActive = false;
            else
                old_.IsActive = true;

            _partationRepository.Update(old_);
            _partationRepository.UnitOfWork.SaveChanges();
        }

        public PartationViewModel Get(Guid id)
        {
            var md = _partationRepository.Query().FirstOrDefault(c => c.Id == id);
            return new PartationViewModel
            {
                ContentAr = md.ContentAr,
                ContentEn = md.ContentEn,
                PartationTypeID = md.PartationTypeID,
                Id = md.Id,
                CreatedBy = md.CreatedBy,
                CreationDate = md.CreationDate,
                LastUpdatedAt = md.LastUpdatedAt,
                LastUpdatedBy = md.LastUpdatedBy,
                
            };
        }

        public List<PartationViewModel> GetList(string query)
        {
            return _partationRepository.Query()
                  .Where(x => string.IsNullOrEmpty(query))
                  .Select(c => new PartationViewModel
                  {
                      Id = c.Id,
                      ContentAr = c.ContentAr,
                      ContentEn = c.ContentEn,
                      PartationTypeID = c.PartationTypeID,
                      CreatedBy = c.CreatedBy,
                      CreationDate = c.CreationDate,
                      IsActive = c.IsActive,
                      LastUpdatedAt = c.LastUpdatedAt,
                      LastUpdatedBy = c.LastUpdatedBy
                  }).ToList();
        }

        public PartationViewModel GetByPartationTypeName(string partionName)
        {
            var _p = _partationRepository.Query().Where(c => c.IsActive).FirstOrDefault(p => p.PartationType.TitleEn.ToLower().Trim().Contains(partionName.ToLower().Trim()));
            if (_p != null)
                return new PartationViewModel
                {
                    Id = _p.Id,
                    ContentAr = _p.ContentAr,
                    ContentEn = _p.ContentEn,
                    PartationTypeID = _p.PartationTypeID,
                    CreatedBy = _p.CreatedBy,
                    CreationDate = _p.CreationDate,
                    IsActive = _p.IsActive,
                    LastUpdatedAt = _p.LastUpdatedAt,
                    LastUpdatedBy = _p.LastUpdatedBy
                };
            else
                return new PartationViewModel
                {
                    Id = Guid.Empty,
                    ContentAr = string.Empty,
                    ContentEn = string.Empty,
                    PartationTypeID = Guid.Empty,
                    CreatedBy = Guid.Empty,
                    CreationDate = DateTime.MinValue,
                    IsActive = false,
                    LastUpdatedAt = DateTime.MinValue,
                    LastUpdatedBy = Guid.Empty
                };

        }

        public ListPartationViewModel GetPageData(string searchValue, int requestStart, int requestLenght, string orderColumsName, int orderColumsDirect)
        {
            var partations = _partationRepository.Query();

            var filteredData = String.IsNullOrWhiteSpace(searchValue) ?
                                   partations :
                                    partations.Where(x =>
                                       x.ContentAr.ToLower().Contains(searchValue)
                                    || x.ContentEn.ToLower().Contains(searchValue));

            switch (orderColumsName.ToLower())
            {
                case "contentAr":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.ContentAr) : filteredData.OrderByDescending(c => c.ContentAr);
                    break;
                case "contentEn":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.ContentEn) : filteredData.OrderByDescending(c => c.ContentEn);
                    break;
                default:
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderByDescending(c => c.CreationDate) : filteredData.OrderBy(c => c.CreationDate);
                    break;
            }

            var dataPage = filteredData.Skip(requestStart).Take(requestLenght);

            var viewModel = new ListPartationViewModel();
            viewModel.TotalDatalenght = partations.Count();
            viewModel.FilterDatalenght = filteredData.Count();
            viewModel.Items = dataPage.Select(c => new PartationViewModel
            {
                Id = c.Id,
                ContentAr = c.ContentAr,
                ContentEn = c.ContentEn,
                PartationTypeID = c.PartationTypeID,
                CreatedBy = c.CreatedBy,
                CreationDate = c.CreationDate,
                IsActive = c.IsActive,
                LastUpdatedAt = c.LastUpdatedAt,
                LastUpdatedBy = c.LastUpdatedBy,
                PartationTypeName = c.PartationType.TitleEn,
            }).ToList();
            return viewModel;
        }

        public void Update(PartationViewModel partation)
        {
            //Edit
            var old_ = _partationRepository.Get(partation.Id);
            old_.ContentAr = partation.ContentAr;
            old_.ContentEn = partation.ContentEn;
            old_.PartationTypeID = partation.PartationTypeID;
            old_.LastUpdatedAt = DateTime.Now;
            _partationRepository.Update(old_);
            _partationRepository.UnitOfWork.SaveChanges();
        }
    }
}
