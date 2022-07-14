using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.Cities;
using Origin.YMC.Business.Entities.Domain.Cities.ViewModels;
using Origin.YMC.Core.Common;
using Origin.YMC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Origin.YMC.Business.Components.Implementation
{
    public class CityComponent : ICityComponent
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IUserComponent _userComponent;
        private readonly ILogComponent _logComponent;
        public CityComponent(
            IRepository<City> cityRepository,
            IUserComponent userComponent,
            ILogComponent logComponent)
        {
            _userComponent = userComponent;
            _cityRepository = cityRepository;
            _logComponent = logComponent;
        }


        public void Add(CityViewModel cityVM)
        {
            try
            {

                _cityRepository.Add(new City
                {
                    Id = Guid.NewGuid(),
                    Name = cityVM.Name,
                    CreatedBy = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true
                });
                _cityRepository.UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public List<CityViewModel> GetAll()
        {
            try
            {

                return _cityRepository.Query()
                    .Where(c => c.IsActive == true)
                    .OrderBy(c => c.CreationDate)
                    .Select(c => new CityViewModel
                    {
                        Id = c.Id,
                        Name = ApplicationContext.IsArabic ? c.NameAr : c.Name
                    }).ToList();

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public List<CityViewModel> GetByCode(string countryCode)
        {
            try
            {

                return _cityRepository.Query()
                    .Where(c => c.IsActive == true)
                    .Where(c => c.CountryCode.Trim().ToLower() == countryCode.Trim().ToLower())
                    .OrderBy(c => c.CreationDate)
                    .Select(c => new CityViewModel
                    {
                        Id = c.Id,
                        Name = ApplicationContext.IsArabic ? c.NameAr : c.Name
                    }).ToList();

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}
