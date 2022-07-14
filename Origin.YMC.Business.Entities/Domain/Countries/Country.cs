using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.Countries
{
    public class Country : EntityBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
    }
}
