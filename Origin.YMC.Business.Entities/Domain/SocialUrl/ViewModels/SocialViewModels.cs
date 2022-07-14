using Origin.YMC.Business.Entities.Domain.SocialUrl.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Origin.YMC.Business.Entities.Domain.SocialUrl.ViewModels
{
    public class SocialViewModels
    {
        [Display(Name = "Arabic Title")]
        [Required(ErrorMessage = "Please enter Arabic Content")]
        [RegularExpression(@"^[ا-ي إأآةؤئء 0-9\s\r\n_@./#!؟()&+-]*$", ErrorMessage = "Please enter valid arabic Content")]
        public string TitleAr { get; set; }

        [Display(Name = "English Title")]
        [Required(ErrorMessage = "Please enter English Content")]
        [RegularExpression(@"^[a-z A-Z 0-9\s\r\n_@./#?!()&+-]*$", ErrorMessage = "Please enter valid english Content")]
        public string TitleEn { get; set; }

        [Display(Name = "Social Info(Url - Phone - Mail)")]
        [Required(ErrorMessage = "Please enter Url")]
        //[RegularExpression(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$", ErrorMessage = "Please enter valid Url")]
        public string Url { set; get; }

        [Display(Name = "Social Type")]
        [Required(ErrorMessage = "Please choose Social Type")]
        public SocialType Type { set; get; }

        public Guid? Id { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }

    public class ListSocialViewModels
    {
        public List<SocialViewModels> Items { get; set; }
        public int TotalDatalenght { get; set; }
        public int FilterDatalenght { get; set; }
        public ListSocialViewModels()
        {
            Items = new List<SocialViewModels>();
        }
    }
}
