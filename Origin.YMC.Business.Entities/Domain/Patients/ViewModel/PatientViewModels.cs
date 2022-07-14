using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Origin.YMC.Business.Entities.Domain.Patients.ViewModel
{
    public class PatientViewModels
    {
        #region Patient  
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public int HeardAboutUsId { get; set; }
        public int SecurityQuestionId { get; set; }
        public string SecurityQuestionAnswer { get; set; }
        public string NonUsStateOrProvince { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Address1 { get; set; }
        public Guid CountryId { get; set; }
        public string CountryCode { get; set; }
        public Guid CityId { get; set; }
        public string Phone { get; set; }
        public string Address2 { get; set; }
        public string ZipPostalCode { get; set; }
        public string MobilePhone { get; set; }
        public DateTime CreationDate { get; set; }
        #endregion

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
        public Guid ApplicationUserId { get; set; }
        public Guid PaymentId { get; set; }
        public string CityName { get; set; }
        public bool IsActive { get; set; }

        public List<CaseListingViewModel> Cases { get; set; }
    }
    public class ListPatientViewModel
    {
        public List<PatientViewModels> Items { get; set; }
        public int TotalDatalenght { get; set; }
        public int FilterDatalenght { get; set; }
        public ListPatientViewModel()
        {
            Items = new List<PatientViewModels>();
        }
    }
}