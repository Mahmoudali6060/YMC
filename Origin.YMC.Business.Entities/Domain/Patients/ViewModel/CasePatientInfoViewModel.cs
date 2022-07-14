using Origin.YMC.Business.Entities.Domain.Patients.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.Patients.ViewModel
{
   public class CasePatientInfoViewModel
    {
        public Guid CaseId { get; set; }
        [Required]

        public Nullable<SaluationEnum> Saluation { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }



        [Required]
        public string CountryId { get; set; }
        [Required]
        public string Region { get; set; }




        public Nullable<Guid> CityId { get; set; }
        [Required]
        [DataAnnotationsExtensions.Integer(ErrorMessage =  "ZipCode is not valid" )]
        public int? ZipCode { get; set; }
        [Required]
        public PhoneTypeEnum? PrimaryPhoneType { get; set; }
         public PhoneTypeEnum? SecondaryPhoneType { get; set; }
        [Required]

        public string PrimaryPhone { get; set; }
         public string SecondaryPhone { get; set; }
        [Required]
        public string Email { get; set; }

        public DateTime? BirthDate { get; set; }

        public GenderEnum? Gender { get; set; }

    }
}
