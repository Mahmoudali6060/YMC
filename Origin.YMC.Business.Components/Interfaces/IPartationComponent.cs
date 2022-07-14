
using Origin.YMC.Business.Entities.Domain.Partation.ViewModels;
using Origin.YMC.Business.Entities.Domain.Partation;
using System;
using System.Collections.Generic;

namespace Origin.YMC.Business.Components.Interfaces
{
    public interface IPartationComponent
    {
        void Add(Partation partation);
        void Update(PartationViewModel partation);
        void Create(PartationViewModel partation);
        PartationViewModel Get(Guid id);
        void Deactive_Activate(Guid id);
        List<PartationViewModel> GetList(string query);
        ListPartationViewModel GetPageData(string searchValue, int requestStart, int requestLenght, string orderColumsName, int orderColumsDirect);
        PartationViewModel GetByPartationTypeName(string partionName);
    }
}
