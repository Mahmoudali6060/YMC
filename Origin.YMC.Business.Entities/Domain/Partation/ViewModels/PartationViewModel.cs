
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Origin.YMC.Business.Entities.Domain.Partation.ViewModels
{
    public class PartationViewModel
    {
        [Display(Name = "Arabic Content")]
        [Required(ErrorMessage = "Please enter Arabic Content")]
        [RegularExpression(@"^[ا-ي إأآةؤئء 0-9\s\r\n_@./#!؟()&+-]*$", ErrorMessage = "Please enter valid arabic Content")]
        public string ContentAr { get; set; }


        [Display(Name = "English Content")]
        [Required(ErrorMessage = "Please enter English Content")]
        [RegularExpression(@"^[a-z A-Z 0-9\s\r\n_@./#?!()&+-]*$", ErrorMessage = "Please enter valid english Content")]
        public string ContentEn { get; set; }

        [Display(Name = "Partition type")]
        public Guid PartationTypeID { get; set; }

        public Guid? Id { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public string PartationTypeName { get; set; }

    }

    public class ListPartationViewModel
    {
        public List<PartationViewModel> Items { get; set; }
        public int TotalDatalenght { get; set; }
        public int FilterDatalenght { get; set; }
        public ListPartationViewModel()
        {
            Items = new List<PartationViewModel>();
        }
    }
}
