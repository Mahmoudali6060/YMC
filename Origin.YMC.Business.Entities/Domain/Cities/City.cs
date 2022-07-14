using Origin.YMC.Business.Entities.Domain.Countries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.Cities
{
    public class City : EntityBase
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
    }
}
