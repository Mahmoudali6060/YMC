
using Origin.YMC.Business.Entities.Domain.Partation.ViewModels;
using Origin.YMC.Business.Entities.Domain.Partation;
using System;
using System.Collections.Generic;

namespace Origin.YMC.Business.Components.Interfaces
{
    public interface IPartationTypeComponent
    {
        void Add(PartationType partationType);
        void Update(PartationTypeViewModel partationType);
        void Create(PartationTypeViewModel partationType);
        PartationTypeViewModel Get(Guid id);
        void Deactive_Activate(Guid id);
        List<PartationTypeViewModel> GetList(string query);
        ListPartationTypeViewModel GetPageData(string searchValue, int requestStart, int requestLenght, string orderColumsName, int orderColumsDirect);
        PartationTypeViewModel CheckOrderPostionAppearExist(int orderPostionAppear, Guid id);
    }
}
