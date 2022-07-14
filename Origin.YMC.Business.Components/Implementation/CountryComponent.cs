using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.Cities;
using Origin.YMC.Business.Entities.Domain.Cities.ViewModels;
using Origin.YMC.Business.Entities.Domain.Countries;
using Origin.YMC.Business.Entities.Lockup.ViewModels;
using Origin.YMC.Core.Common;
using Origin.YMC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Origin.YMC.Business.Components.Implementation
{
    public class CountryComponent : ICountryComponent
    {

        private readonly IRepository<Country> _countryRepository;
        private readonly IUserComponent _userComponent;
        private readonly ILogComponent _logComponent;
        public CountryComponent(
            IRepository<Country> countryRepository,
            IUserComponent userComponent,
            ILogComponent logComponent)
        {
            _userComponent = userComponent;
            _countryRepository = countryRepository;
            _logComponent = logComponent;
        }
        public List<LockupViewModel> GetAll()
        {
            try
            {

                return _countryRepository.Query()
                    .Where(c => c.IsActive == true)
                    .OrderBy(c => c.CreationDate)
                    .Select(c => new LockupViewModel
                    {
                        Id = c.Id,
                        Name = ApplicationContext.IsArabic ? c.NameAr : c.Name,
                        Code = c.Code
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
