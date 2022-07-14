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


namespace Origin.YMC.Business.Components.Implementation
{
    public class PatientComponent : IPatientComponent
    {

        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Patient> _patientRepository;
        private readonly IUserComponent _userComponent;
        private readonly ILogComponent _logComponent;
        private readonly ICaseComponent _caseComponent;

        public PatientComponent(
            IRepository<Country> countryRepository,
            IRepository<Patient> patientRepository,
            IRepository<City> cityRepository,
            IUserComponent userComponent,
            ILogComponent logComponent,
            ICaseComponent caseComponent)
        {
            _userComponent = userComponent;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _patientRepository = patientRepository;
            _countryRepository = countryRepository;
            _logComponent = logComponent;
            _caseComponent = caseComponent;
        }

        public void Add(PatientViewModels patient)
        {
            try
            {

                _patientRepository.Add(new Patient
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = patient.ApplicationUserId,
                    PaymentId = patient.PaymentId,
                    CreatedBy = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true
                });
                _cityRepository.UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }


        public bool ActivateDeactivatePatient(Guid id)
        {
            try
            {
                //Edit
                var oldPatient = _patientRepository.Get(id);
                if (oldPatient.IsActive)
                    oldPatient.IsActive = false;
                else
                    oldPatient.IsActive = true;

                _patientRepository.Update(oldPatient);
                _patientRepository.UnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IQueryable<PatientViewModels> List(string filter)
        {
            try
            {
                return _patientRepository.Query().Where(x =>
                                                      string.IsNullOrEmpty(filter)
                                                   || x.ApplicationUser.FirstName.Contains(filter)
                                                   || x.ApplicationUser.LastName.Contains(filter)
                                                   || x.ApplicationUser.Email.Contains(filter))
                                                   .OrderByDescending(x => x.CreationDate).Select(x => new PatientViewModels
                                                   {
                                                       Id = x.Id,
                                                       CityId = x.ApplicationUser.CityId ?? Guid.Empty,
                                                       FirstName = x.ApplicationUser.FirstName,
                                                       LastName = x.ApplicationUser.LastName,
                                                       GenderId = (int)x.ApplicationUser.GenderEnum,
                                                       HeardAboutUsId = x.ApplicationUser.HeardAboutUsId ?? 0,
                                                       SecurityQuestionId = x.ApplicationUser.SecurityQuestionId ?? 0,
                                                       SecurityQuestionAnswer = x.ApplicationUser.SecurityQuestionAnswer,
                                                       NonUsStateOrProvince = x.ApplicationUser.NonUsStateOrProvince,
                                                       Year = x.ApplicationUser.BirthDate.Value.Year.ToString(),
                                                       Month = x.ApplicationUser.BirthDate.Value.Month.ToString(),
                                                       Day = x.ApplicationUser.BirthDate.Value.Day.ToString(),
                                                       Address1 = x.ApplicationUser.Address1,
                                                       Phone = x.ApplicationUser.Phone,
                                                       Address2 = x.ApplicationUser.Address2,
                                                       ZipPostalCode = x.ApplicationUser.ZipPostalCode,
                                                       MobilePhone = x.ApplicationUser.Mobile,
                                                       Email = x.ApplicationUser.Email,
                                                       CityName = x.ApplicationUser.City.Name,
                                                       IsActive = x.IsActive
                                                   });

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
        public ListPatientViewModel GetPageData(string searchValue, int requestStart, int requestLenght, string orderColumsName, int orderColumsDirect)
        {
            var patients = _patientRepository.Query();

            var filteredData = String.IsNullOrWhiteSpace(searchValue) ?
                                   patients :
                                     patients.Where(x =>
                                                      x.ApplicationUser.FirstName.Contains(searchValue)
                                                   || x.ApplicationUser.LastName.Contains(searchValue)
                                                   || x.ApplicationUser.NonUsStateOrProvince.Contains(searchValue)
                                                   || x.ApplicationUser.Mobile.Contains(searchValue)
                                                   || x.ApplicationUser.City.Name.Contains(searchValue)
                                                   || x.ApplicationUser.Email.Contains(searchValue));

            switch (orderColumsName.ToLower())
            {
                case "firstName":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.ApplicationUser.FirstName) : filteredData.OrderByDescending(c => c.ApplicationUser.FirstName);
                    break;
                case "lastName":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.ApplicationUser.LastName) : filteredData.OrderByDescending(c => c.ApplicationUser.LastName);
                    break;
                case "cityName":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.ApplicationUser.City.Name) : filteredData.OrderByDescending(c => c.ApplicationUser.City.Name);
                    break;
                default:
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderByDescending(c => c.CreationDate) : filteredData.OrderBy(c => c.CreationDate);
                    break;
            }

            var dataPage = filteredData.Skip(requestStart).Take(requestLenght);

            var viewModel = new ListPatientViewModel();
            viewModel.TotalDatalenght = patients.Count();
            viewModel.FilterDatalenght = filteredData.Count();
            viewModel.Items = dataPage.Select(x => new PatientViewModels
            {
                Id = x.Id,
                CityId = x.ApplicationUser.CityId ?? Guid.Empty,
                FirstName = x.ApplicationUser.FirstName,
                LastName = x.ApplicationUser.LastName,
                GenderId = (int)x.ApplicationUser.GenderEnum,
                HeardAboutUsId = x.ApplicationUser.HeardAboutUsId ?? 0,
                SecurityQuestionId = x.ApplicationUser.SecurityQuestionId ?? 0,
                SecurityQuestionAnswer = x.ApplicationUser.SecurityQuestionAnswer,
                NonUsStateOrProvince = x.ApplicationUser.NonUsStateOrProvince,
                Year = x.ApplicationUser.BirthDate.Value.Year.ToString(),
                Month = x.ApplicationUser.BirthDate.Value.Month.ToString(),
                Day = x.ApplicationUser.BirthDate.Value.Day.ToString(),
                Address1 = x.ApplicationUser.Address1,
                Phone = x.ApplicationUser.Phone,
                Address2 = x.ApplicationUser.Address2,
                ZipPostalCode = x.ApplicationUser.ZipPostalCode,
                MobilePhone = x.ApplicationUser.Mobile,
                Email = x.ApplicationUser.Email,
                CityName = x.ApplicationUser.City.Name,
                IsActive = x.IsActive,
                CreationDate = x.CreationDate
            }).ToList();
            return viewModel;
        }
        public PatientViewModels GetPatientProfileId(Guid patientId)
        {
            PatientViewModels patientProfile;

            var patient = _patientRepository.Query()
                      .Include(x => x.ApplicationUser)
                      .Include(x=>x.ApplicationUser.City)
                      .Include(x => x.Payment)
                      .FirstOrDefault(x => x.Id == patientId);

            patientProfile = new PatientViewModels
            {
                Id = patient.Id,
                CityId = patient.ApplicationUser.CityId ?? Guid.Empty,
                CountryId = _countryRepository.Query().FirstOrDefault(x=>x.Code == patient.ApplicationUser.City.CountryCode )?.Id ?? Guid.Empty,
                FirstName = patient.ApplicationUser.FirstName,
                LastName = patient.ApplicationUser.LastName,
                GenderId = (int)patient.ApplicationUser.GenderEnum,
                HeardAboutUsId = patient.ApplicationUser.HeardAboutUsId ?? 0,
                SecurityQuestionId = patient.ApplicationUser.SecurityQuestionId ?? 0,
                SecurityQuestionAnswer = patient.ApplicationUser.SecurityQuestionAnswer,
                NonUsStateOrProvince = patient.ApplicationUser.NonUsStateOrProvince,
                Year = patient.ApplicationUser.BirthDate.Value.Year.ToString(),
                Month = patient.ApplicationUser.BirthDate.Value.Month.ToString(),
                Day = patient.ApplicationUser.BirthDate.Value.Day.ToString(),
                Address1 = patient.ApplicationUser.Address1,
                Phone = patient.ApplicationUser.Phone,
                Address2 = patient.ApplicationUser.Address2,
                ZipPostalCode = patient.ApplicationUser.ZipPostalCode,
                MobilePhone = patient.ApplicationUser.Mobile,
                Email = patient.ApplicationUser.Email,
                CityName = patient.ApplicationUser.City.Name,
                IsActive = patient.IsActive,
                Cases = _caseComponent.GetCasesByPatientId(patient.Id),
                CreationDate = patient.CreationDate,
                PaymentInfoCardholderFullName = patient.Payment.CardholderFullName,
                PaymentInfoCreditCardNumber = patient.Payment.CreditCardNumber,
                PaymentInfoExpirationDate = patient.Payment.ExpirationDate,
                PaymentInfoExpirationMonth = patient.Payment.ExpirationDate.ToString("MM"),
                PaymentInfoExpirationYear = patient.Payment.ExpirationDate.ToString("yy"),
                PaymentInfoCVV = patient.Payment.CVV,
                BirthDate = patient.ApplicationUser.BirthDate.Value,
                CountryCode = patient.ApplicationUser.City.CountryCode
            };
            return patientProfile;
        }
        public Guid GetUserId(Guid patientId)
        {
            var interpreter = _patientRepository.Query()
                .Include(x => x.ApplicationUser)
                .Where(x => x.Id == patientId)
                .Select(a => a.ApplicationUserId).FirstOrDefault();

            return interpreter;
        }
        public Guid GetPatientId(Guid userId)
        {
            var patient = _patientRepository.Query()
                     .Include(x => x.ApplicationUser)
                     .Include(x => x.Payment)
                     .FirstOrDefault(x => x.ApplicationUser.Id == userId);

            return patient != null ? patient.Id : Guid.Empty;
        }

        public bool CheckUserCompleteProfile(Guid userId)
        {
            try
            {

                return _patientRepository.Query()
                      .Include(x => x.ApplicationUser)
                      .Any(x => x.ApplicationUser.Id == userId);

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}
