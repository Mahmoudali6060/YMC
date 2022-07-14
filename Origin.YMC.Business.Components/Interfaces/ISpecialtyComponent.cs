using Origin.YMC.Business.Entities.Domain.Specialties;
using Origin.YMC.Business.Entities.Domain.Specialties.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Components.Interfaces
{
   public interface ISpecialtyComponent
    {
        void Add(Specialty model);
        void Update(SpecialtyViewModels model);
        void Create(SpecialtyViewModels model);
        SpecialtyViewModels Get(Guid id);
        void Deactive_Activate(Guid id);
        List<SpecialtyViewModels> GetList(string query);
        ListSpecialtyViewModel GetPageData(string searchValue, int requestStart, int requestLenght, string orderColumsName, int orderColumsDirect);

    }
}
