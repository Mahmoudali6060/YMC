using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.Patients.ViewModel
{
   public class CaseViewModel
    {
        public Guid  CaseId{ get; set; }
        public Guid DoctorId { get; set; }
        public decimal  Fees { get; set; }
        [Required(ErrorMessage ="Full Name is Required ")]
        public string PaymentInfoCardholderFullName { get; set; }
        [Required(ErrorMessage = "Card Number is Required ")]
        public string PaymentInfoCreditCardNumber  { get; set; }
        [Required(ErrorMessage = "CVV is Required ")]
        public string PaymentInfoCVV               { get; set; }
        public string PaymentInfoAddress1            { get; set; }
        public string PaymentInfoCity              { get; set; }
        public int    PaymentInfoCreditCardType     { get; set; }
        [Required(ErrorMessage = "ExpirationDate is Required ")]
        public DateTime PaymentInfoExpirationDate    { get; set; }
 
        public string PaymentInfoExpirationMonth { get; set; }
        public string PaymentInfoExpirationYear { get; set; }
        public string PaymentInfoCountry           { get; set; }
        public string PaymentInfoAddress2          { get; set; }
        public string PaymentInfoState             { get; set; }
        public string ZipPostalCode { get; set; }
        public Guid PatientId { get; set; }
        public int InterpreterStatus { get; set; }
        public Guid? interpreterId { get; set; }

    }
}
