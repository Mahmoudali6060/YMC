using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.Cities;
using Origin.YMC.Business.Entities.Domain.Cities.ViewModels;
using Origin.YMC.Business.Entities.Domain.Countries;
using Origin.YMC.Business.Entities.Domain.Patients;
using Origin.YMC.Business.Entities.Domain.Patients.ViewModel;
using Origin.YMC.Business.Entities.Lockup.ViewModels;
using Origin.YMC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Origin.YMC.Business.Entities.Domain.Payments;
using Origin.YMC.Business.Entities.Domain.Payments.ViewModel;

namespace Origin.YMC.Business.Components.Implementation
{
    public class PaymentComponent : IPaymentComponent
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IUserComponent _userComponent;
        private readonly ILogComponent _logComponent;
        public PaymentComponent(
            IRepository<Country> countryRepository,
            IRepository<Patient> patientRepository,
            IRepository<City> cityRepository,
            IRepository<Payment> paymentRepository,
            IUserComponent userComponent,
            ILogComponent logComponent)
        {
            _userComponent = userComponent;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _patientRepository = patientRepository;
            _paymentRepository = paymentRepository;
            _countryRepository = countryRepository;
            _logComponent = logComponent;
        }

        public Guid? Add(PaymentViewModel payment)
        {
            try
            {

                var result = _paymentRepository.AddThenGet(new Payment
                {
                    Id = Guid.NewGuid(),
                    CreatedBy = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    CardholderFullName = payment.CardholderFullName,
                    CreditCardNumber = payment.CreditCardNumber,
                    CVV = payment.CVV,
                    Address1 = payment.Address1,
                    City = payment.City,
                    CreditCardType = payment.CreditCardType,
                    ExpirationDate = payment.ExpirationDate,
                    Country = payment.Country,
                    Address2 = payment.Address2,
                    State = payment.State
                });
                _paymentRepository.UnitOfWork.SaveChanges();

                return result.Id;
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }
        public Guid? UpdatePatientPayment(PaymentViewModel payment, Guid Id)
        {
            try
            {

                var result = _patientRepository.GetAll().Include(x=>x.Payment).FirstOrDefault(x=>x.Id == Id);
                result.Payment.CreditCardNumber = payment.CreditCardNumber;
                result.Payment.ExpirationDate = payment.ExpirationDate;
                result.Payment.CardholderFullName = payment.CardholderFullName;
                
                _paymentRepository.UnitOfWork.SaveChanges();

                return result.Id;
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }
    }
}
