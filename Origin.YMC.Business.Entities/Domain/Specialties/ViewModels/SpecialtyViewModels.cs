using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;


namespace Origin.YMC.Business.Entities.Domain.Specialties.ViewModels
{
    public class SpecialtyViewModels 
    {
        [Display(Name = "Arabic Title")]
        [Required(ErrorMessage = "Please enter Arabic Content")]
        [RegularExpression(@"^[ا-ي إأآةؤئء 0-9\s\r\n_@./#!؟()&+-]*$", ErrorMessage = "Please enter valid arabic Content")]
        public string TitleAr { get; set; }

        [Display(Name = "English Title")]
        [Required(ErrorMessage = "Please enter English Content")]
        [RegularExpression(@"^[a-z A-Z 0-9\s\r\n_@./#?!()&+-]*$", ErrorMessage = "Please enter valid english Content")]
        public string TitleEn { get; set; }

        [Display(Name = " Arabic Description")]
        [Required(ErrorMessage = "Please enter Arabic Content")]
        [RegularExpression(@"^[ا-ي إأآةؤئء 0-9\s\r\n_@./#!؟()&+-]*$", ErrorMessage = "Please enter valid arabic Content")]
        public string DescriptionAr { get; set; }

        [Display(Name = "English Description")]
        [Required(ErrorMessage = "Please enter English Content")]
        [RegularExpression(@"^[a-z A-Z 0-9\s\r\n_@./#?!()&+-]*$", ErrorMessage = "Please enter valid english Content")]
        public string DescriptionEn { get; set; }

        public string ImageUrl { get; set; }

        public Guid AttachmentId { get; set; }

        [Display(Name = "Upload Images")]
        [Required(ErrorMessage = "Kindly upload your Images")]
        public HttpPostedFileBase[] AttachmentImage { get; set; }

        public Guid? Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public Guid? LastUpdatedBy { get; set; }
    }

    public class ListSpecialtyViewModel
    {
        public List<SpecialtyViewModels> Items { get; set; }
        public int TotalDatalenght { get; set; }
        public int FilterDatalenght { get; set; }
        public ListSpecialtyViewModel()
        {
            Items = new List<SpecialtyViewModels>();
        }
    }
}
