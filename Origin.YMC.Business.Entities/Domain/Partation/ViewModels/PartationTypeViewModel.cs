
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Origin.YMC.Business.Entities.Domain.Partation.ViewModels
{
    public class PartationTypeViewModel
    {
        [Display(Name = "Arabic Title")]
        [Required(ErrorMessage = "Please enter Arabic Content")]
        [RegularExpression(@"^[ا-ي إأآةؤئء 0-9\s\r\n_@./#!؟()&+-]*$", ErrorMessage = "Please enter valid arabic Content")]
        public string TitleAr { get; set; }

        [Display(Name = "English Title")]
        [Required(ErrorMessage = "Please enter English Content")]
        [RegularExpression(@"^[a-z A-Z 0-9\s\r\n_@./#?!()&+-]*$", ErrorMessage = "Please enter valid english Content")]
        public string TitleEn { get; set; }

        [Display(Name = "Order ")]
        [Required(ErrorMessage = "Please enter english order postion number")]
        public int OrderPostionAppear { get; set; }

        public Guid? Id { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }

    public class ListPartationTypeViewModel
    {
        public List<PartationTypeViewModel> Items { get; set; }
        public int TotalDatalenght { get; set; }
        public int FilterDatalenght { get; set; }
        public ListPartationTypeViewModel()
        {
            Items = new List<PartationTypeViewModel>();
        }
    }
}
