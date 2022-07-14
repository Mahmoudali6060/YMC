using Origin.YMC.Business.Entities.Domain.Cities.ViewModels;
using System.Collections.Generic;

namespace Origin.YMC.Business.Components.Interfaces
{
    public interface ICityComponent
    {
        List<CityViewModel> GetAll();
        void Add(CityViewModel cityVM);
        List<CityViewModel> GetByCode(string countryCode);
    }
}
