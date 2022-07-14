using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Origin.YMC.Web.Client.Models
{
    public class DoctorViewModels
    {
        [Required(ErrorMessageResourceName = "CountryRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        // [Display(Name = "Country", ResourceType = typeof(Resources.GlobalStrings))]
        public string CountryId { get; set; }

        [Required(ErrorMessageResourceName = "FirstNameRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        // [Display(Name = "FirstName", ResourceType = typeof(Resources.GlobalStrings))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "LastNameRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "LastName", ResourceType = typeof(Resources.GlobalStrings))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "GenderRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "Gender", ResourceType = typeof(Resources.GlobalStrings))]
        public int GenderId { get; set; }

        [Required(ErrorMessageResourceName = "HeardAboutUsRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "HeardAboutUs", ResourceType = typeof(Resources.GlobalStrings))]
        public int HeardAboutUsId { get; set; }

        [Required(ErrorMessageResourceName = "SecurityQuestionRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        // [Display(Name = "SecurityQuestion", ResourceType = typeof(Resources.GlobalStrings))]
        public int SecurityQuestionId { get; set; }

        [Required(ErrorMessageResourceName = "SecurityQuestionAnswerRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "SecurityQuestionAnswer", ResourceType = typeof(Resources.GlobalStrings))]
        public string SecurityQuestionAnswer { get; set; }

        //[Required(ErrorMessageResourceName = "NonUsStateOrProvinceRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        // [Display(Name = "NonUsStateOrProvince", ResourceType = typeof(Resources.GlobalStrings))]
        public string NonUsStateOrProvince { get; set; }

        [Required(ErrorMessageResourceName = "YearRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "Year", ResourceType = typeof(Resources.GlobalStrings))]
        [Range(1920, 2019, ErrorMessageResourceName = "YearRangMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        [DataAnnotationsExtensions.Integer(ErrorMessageResourceName = "NumberTypeMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public string Year { get; set; }

        [Required(ErrorMessageResourceName = "MonthRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "Month", ResourceType = typeof(Resources.GlobalStrings))]
        [Range(1, 12, ErrorMessageResourceName = "MonthRangMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        [DataAnnotationsExtensions.Integer(ErrorMessageResourceName = "NumberTypeMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public string Month { get; set; }

        [Required(ErrorMessageResourceName = "DayRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "Day", ResourceType = typeof(Resources.GlobalStrings))]
        [Range(1, 31, ErrorMessageResourceName = "DayRangMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        [DataAnnotationsExtensions.Integer(ErrorMessageResourceName = "NumberTypeMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public string Day { get; set; }

        [Required(ErrorMessageResourceName = "BillingAddressRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "Address1", ResourceType = typeof(Resources.GlobalStrings))]
        public string Address1 { get; set; }

        [Required(ErrorMessageResourceName = "CityRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "City", ResourceType = typeof(Resources.GlobalStrings))]
        public Guid CityId { get; set; }

        [Required(ErrorMessageResourceName = "PhoneRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "Phone", ResourceType = typeof(Resources.GlobalStrings))]
        [DataAnnotationsExtensions.Integer(ErrorMessageResourceName = "NumberTypeMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceName = "WorkAddressRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public string Address2 { get; set; }
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessageResourceName = "ZipPostalCodeRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "ZipPostalCode", ResourceType = typeof(Resources.GlobalStrings))]
        [DataAnnotationsExtensions.Integer(ErrorMessageResourceName = "NumberTypeMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public string ZipPostalCode { get; set; }

        [Required(ErrorMessageResourceName = "MobilePhoneRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "MobilePhone", ResourceType = typeof(Resources.GlobalStrings))]
        //[DataAnnotationsExtensions.Integer(ErrorMessageResourceName = "NumberTypeMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public string MobilePhone { get; set; }

        [Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        #region PaymentInfo
        public string PaymentInfoCardholderFullName { get; set; }
        public string PaymentInfoCreditCardNumber { get; set; }
        public string PaymentInfoCVV { get; set; }
        public string PaymentInfoAddress1 { get; set; }
        public string PaymentInfoCity { get; set; }
        public int PaymentInfoCreditCardType { get; set; }
        public DateTime PaymentInfoExpirationDate { get; set; }
        public string PaymentInfoExpirationMonth { get; set; }
        public string PaymentInfoExpirationYear { get; set; }
        public string PaymentInfoCountry { get; set; }
        public string PaymentInfoAddress2 { get; set; }
        public string PaymentInfoState { get; set; }
        #endregion


        //[Required(ErrorMessage = "kindly upload certificates")]
        //public HttpPostedFileBase[] AttachmentCertificate { get; set; }
        [Required(ErrorMessage = "kindly upload Profile Image")]

        public HttpPostedFileBase  ProfileImage { get; set; }

        [Url(ErrorMessageResourceName = "UrlNotValid", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        [Required(ErrorMessageResourceName = "OnlineProfileLinkRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public string OnlineProfileLink { get; set; }


    }
}