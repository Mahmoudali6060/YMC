using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Origin.YMC.Web.Client.Models
{
    public class PatientViewModels
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

        [Required(ErrorMessageResourceName = "RejectPatientAgreementRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public string RejectPatientAgreement { get; set; }

        [Required(ErrorMessageResourceName = "SecurityQuestionRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
       // [Display(Name = "SecurityQuestion", ResourceType = typeof(Resources.GlobalStrings))]
        public int SecurityQuestionId { get; set; }

        [Required(ErrorMessageResourceName = "SecurityQuestionAnswerRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "SecurityQuestionAnswer", ResourceType = typeof(Resources.GlobalStrings))]
        public string SecurityQuestionAnswer { get; set; }

       // [Required(ErrorMessageResourceName = "NonUsStateOrProvinceRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
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

        [Required(ErrorMessageResourceName = "Address1RequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "Address1", ResourceType = typeof(Resources.GlobalStrings))]
        public string Address1 { get; set; }

        [Required(ErrorMessageResourceName = "CityRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "City", ResourceType = typeof(Resources.GlobalStrings))]
        public Guid CityId { get; set; }


        [Required(ErrorMessageResourceName = "PhoneRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "Phone", ResourceType = typeof(Resources.GlobalStrings))]
        [RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$", ErrorMessageResourceName = "NumberTypeMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public string Phone { get; set; }
        [Required(ErrorMessageResourceName = "address2RequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]

        public string Address2 { get; set; }

        [Required(ErrorMessageResourceName = "ZipPostalCodeRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "ZipPostalCode", ResourceType = typeof(Resources.GlobalStrings))]
        [DataAnnotationsExtensions.Integer(ErrorMessageResourceName = "NumberTypeMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public string ZipPostalCode { get; set; }

        [Required(ErrorMessageResourceName = "MobilePhoneRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        //[Display(Name = "MobilePhone", ResourceType = typeof(Resources.GlobalStrings))]
        [RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$", ErrorMessageResourceName = "NumberTypeMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public string MobilePhone { get; set; }


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
        public DateTime BirthDate { get; set; }

        public string PaymentInfoCountry { get; set; }
        public string PaymentInfoAddress2 { get; set; }
        public string PaymentInfoState { get; set; }
        #endregion
    }

    public class CaseViewModel
    {
        public Guid CaseId { get; set; }
        public Guid DoctorId { get; set; }
        public decimal Fees { get; set; }
       
        [Required(ErrorMessageResourceName = "PaymentInfoCardholderFullNameRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public string PaymentInfoCardholderFullName { get; set; }

        [DataAnnotationsExtensions.Integer(ErrorMessageResourceName = "NumberTypeMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        [Required(ErrorMessageResourceName = "PaymentInfoCreditCardNumberRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public string PaymentInfoCreditCardNumber { get; set; }
       
        [Required(ErrorMessageResourceName = "PaymentInfoCVVRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public string PaymentInfoCVV { get; set; }
        public string PaymentInfoAddress1 { get; set; }
        public string PaymentInfoCity { get; set; }
        public int PaymentInfoCreditCardType { get; set; }
       
        [Required(ErrorMessageResourceName = "PaymentInfoExpirationDateRequiredMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public DateTime PaymentInfoExpirationDate { get; set; }

        public string PaymentInfoExpirationMonth { get; set; }
        public string PaymentInfoExpirationYear { get; set; }
        public string PaymentInfoCountry { get; set; }
        public string PaymentInfoAddress2 { get; set; }
        public string PaymentInfoState { get; set; }
        [DataAnnotationsExtensions.Integer(ErrorMessageResourceName = "NumberTypeMsg", ErrorMessageResourceType = typeof(Resources.GlobalStrings))]
        public string ZipPostalCode { get; set; }
        public Guid PatientId { get; set; }
        public int InterpreterStatus { get; set; }
        public Guid? interpreterId { get; set; }

    }
}