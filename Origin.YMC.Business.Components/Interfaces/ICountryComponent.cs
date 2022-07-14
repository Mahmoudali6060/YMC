using Origin.YMC.Business.Entities.Lockup.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Components.Interfaces
{
    public interface ICountryComponent
    {
        List<LockupViewModel> GetAll();
    }
}
