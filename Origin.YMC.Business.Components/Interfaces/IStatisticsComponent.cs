using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origin.YMC.Business.Entities.Domain.Statistics.ViewModels;

namespace Origin.YMC.Business.Components.Interfaces
{
   public interface IStatisticsComponent
   {
       FactsViewModels GetAllFacts();
   }
}
