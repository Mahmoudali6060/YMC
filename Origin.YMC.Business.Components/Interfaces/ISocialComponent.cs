using Origin.YMC.Business.Entities.Domain.SocialUrl;
using Origin.YMC.Business.Entities.Domain.SocialUrl.ViewModels;
using System;
using System.Collections.Generic;

namespace Origin.YMC.Business.Components.Interfaces
{
    public interface ISocialComponent
    {
        void Add(Social page);
        void Update(SocialViewModels model);

        void Create(SocialViewModels model);
        SocialViewModels Get(Guid id);
        void Deactive_Activate(Guid id);
        List<SocialViewModels> GetList(string query);
        ListSocialViewModels GetPageData(string searchValue, int requestStart, int requestLenght, string orderColumsName, int orderColumsDirect);
    }
}
