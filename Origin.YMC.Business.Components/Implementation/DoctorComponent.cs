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
using Origin.YMC.Business.Entities.Domain.Doctors;
using Origin.YMC.Business.Entities.Domain.Doctors.ViewModel;
using Origin.YMC.Business.Components.ViewModels;
using Origin.YMC.Core.Common;
using Origin.YMC.Business.Entities.Domain;

namespace Origin.YMC.Business.Components.Implementation
{
    public class DoctorComponent : IDoctorComponent
    {

        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Attachment> _attachmentRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IUserComponent _userComponent;
        private readonly ILogComponent _logComponent;
        private readonly IAttachmentComponent _attachmentComponent;

        public DoctorComponent(
            IRepository<Country> countryRepository,
            IRepository<City> cityRepository,
            IRepository<Doctor> DoctorRepository,
             IAttachmentComponent attachmentComponent,
             IRepository<Attachment>  attachmentRepository,
            IUserComponent userComponent,
            ILogComponent logComponent)
        {
            _userComponent = userComponent;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _doctorRepository = DoctorRepository;
            _countryRepository = countryRepository;
            _logComponent = logComponent;
            _attachmentComponent = attachmentComponent;
            _attachmentRepository = attachmentRepository;
        }
        public string GetDoctorProfileImage(Guid UserId)
        {
            try
            { 
                    return _attachmentRepository.Query()
                        .Where(x=>x.OwnerId == UserId && x.AttachmentTypeId ==
                        (int)AttachmentTypeEnum.DoctorProfileImage)
                        .Select(x=>x.GeneratedFileName).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public void Add(DoctorViewModels doctor)
        {
            try
            {

                _doctorRepository.Add(new Doctor
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = doctor.ApplicationUserId,
                    PaymentId = doctor.PaymentId,
                    CreatedBy = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = false,
                    SpecialtieId = doctor.SpecialtyId,
                    Refrances = doctor.Refrances,
                    Experinces = doctor.Experinces,
                    Bio = doctor.Bio,

                });
                _cityRepository.UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public Guid AddThenGet(DoctorViewModels doctor)
        {
            try
            {

                var result = _doctorRepository.AddThenGet(new Doctor
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = doctor.ApplicationUserId,
                    PaymentId = doctor.PaymentId,
                    CreatedBy = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = false,
                    SpecialtieId = doctor.SpecialtyId,
                    Refrances = doctor.Refrances,
                    Experinces = doctor.Experinces,
                    Bio = doctor.Bio,
                    OnlineProfileLink = doctor.OnlineProfileLink
                });
                _cityRepository.UnitOfWork.SaveChanges();



                return result.Id;
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public bool ActivateDeactivateDoctor(Guid id)
        {
            try
            {
                //Edit
                var oldDoctor = _doctorRepository.Get(id);
                if (oldDoctor.IsActive)
                    oldDoctor.IsActive = false;
                else
                    oldDoctor.IsActive = true;

                _doctorRepository.Update(oldDoctor);
                _doctorRepository.UnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public decimal GetDoctorFees(Guid id)
        {
            var doctor = _doctorRepository.Query().FirstOrDefault(x => x.Id == id);
            if (doctor != null)
            {
                return doctor.Fees;
            }
            else
            {
                throw new Exception("Doctor is not exists");
            }
        }


        public ListDoctorViewModel GetPageData(string searchValue, int requestStart, int requestLenght, string orderColumsName, int orderColumsDirect)
        {
            var patients = _doctorRepository.Query();

            var filteredData = String.IsNullOrWhiteSpace(searchValue) ?
                                   patients :
                                     patients.Where(x =>
                                                      x.ApplicationUser.FirstName.Contains(searchValue)
                                                   || x.ApplicationUser.LastName.Contains(searchValue)
                                                   || x.ApplicationUser.NonUsStateOrProvince.Contains(searchValue)
                                                   || x.ApplicationUser.Mobile.Contains(searchValue)
                                                   || x.Specialty.TitleEn.Contains(searchValue)
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

            var viewModel = new ListDoctorViewModel();
            viewModel.TotalDatalenght = patients.Count();
            viewModel.FilterDatalenght = filteredData.Count();
            viewModel.Items = dataPage
                .ToList()
                .Select(x => new DoctorViewModels
                {
                    Id = x.Id,
                    ApplicationUserId = x.ApplicationUserId,
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
                    SpecialtyId = x.SpecialtieId,
                    SpecialtyName = ApplicationContext.IsArabic ? x.Specialty.TitleAr : x.Specialty.TitleEn,
                    Bio = x.Bio,
                    Certificates = GetDoctrosCertificates(x.Id),
                    ProfileImage = GetDoctroProfileImage(x.Id),
                    fees = x.Fees,
                    ResponsTime = x.ResponsTime,
                    FocusOfScientific = x.FocusOfScientific,
                    CreationDate = x.CreationDate
                }).ToList();
            return viewModel;
        }

        public IQueryable<DoctorViewModels> List(string filter)
        {
            try
            {
                return _doctorRepository.Query().Where(c => c.IsActive)
                                                .Where(x =>
                                                x.IsActive && (
                                                        string.IsNullOrEmpty(filter)
                                                     || x.ApplicationUser.FirstName.Contains(filter)
                                                     || x.ApplicationUser.LastName.Contains(filter)
                                                     || x.ApplicationUser.Email.Contains(filter)))
                                                   .OrderByDescending(x => x.CreationDate)
                                                   .ToList()
                                                   .Select(x => new DoctorViewModels
                                                   {
                                                       Id = x.Id,
                                                       ApplicationUserId = x.ApplicationUserId,
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
                                                       SpecialtyId = x.SpecialtieId,
                                                       SpecialtyName = ApplicationContext.IsArabic ? x.Specialty.TitleAr : x.Specialty.TitleEn,
                                                       Bio = x.Bio,
                                                       Certificates = GetDoctrosCertificates(x.Id),
                                                       ProfileImage = GetDoctroProfileImage(x.Id),
                                                       fees = x.Fees,
                                                       ResponsTime = x.ResponsTime,
                                                       FocusOfScientific = x.FocusOfScientific,
                                                       OnlineProfileLink = x.OnlineProfileLink
                                                   }).AsQueryable();

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public IQueryable<DoctorViewModels> Search(string specialtyName, string price)
        {
            try
            {
                var priceRange = price.Split('_');
                int fromPrice = int.Parse(priceRange[0]);
                int toPrice = int.Parse(priceRange[1]);



                return _doctorRepository.Query().Where(c => c.IsActive)
                                                .Where(x => x.IsActive && (
                                                        string.IsNullOrEmpty(specialtyName)
                                                     || x.Specialty.TitleAr.Contains(specialtyName)
                                                     || x.Specialty.TitleEn.Contains(specialtyName)))
                                                   .Where(x => x.Fees >= fromPrice && x.Fees <= toPrice)
                                                   .OrderByDescending(x => x.CreationDate)
                                                   .ToList()
                                                   .Select(x => new DoctorViewModels
                                                   {
                                                       Id = x.Id,
                                                       ApplicationUserId = x.ApplicationUserId,
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
                                                       SpecialtyId = x.SpecialtieId,
                                                       SpecialtyName = ApplicationContext.IsArabic ? x.Specialty.TitleAr : x.Specialty.TitleEn,
                                                       Bio = x.Bio,
                                                       Certificates = GetDoctrosCertificates(x.Id),
                                                       ProfileImage = GetDoctroProfileImage(x.Id),
                                                       fees = x.Fees,
                                                       ResponsTime = x.ResponsTime,
                                                       FocusOfScientific = x.FocusOfScientific,
                                                       OnlineProfileLink = x.OnlineProfileLink
                                                   }).AsQueryable();

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        private List<string> GetDoctrosCertificates(Guid doctorId)
        {

            var userAttachements = _attachmentComponent.GetFilesByOwnerId(doctorId);
            return userAttachements.Where(x => x.AttachmentTypeId == (int)AttachmentTypeEnum.Certifications).Select(x => x.GeneratedFileName).ToList();
        }

        private string GetDoctroProfileImage(Guid doctorId)
        {

            var userAttachements = _attachmentComponent.GetFilesByOwnerId(doctorId);
            return userAttachements.Where(x => x.AttachmentTypeId == (int)AttachmentTypeEnum.DoctorProfileImage).OrderByDescending(c => c.CreationDate).Select(x => x.GeneratedFileName).FirstOrDefault();
        }

        public Guid GetDoctorId(Guid userId)
        {
            try
            {

                var doctor = _doctorRepository.Query()
                      .Include(x => x.ApplicationUser)
                      .Include(x => x.Payment)
                      .FirstOrDefault(x => x.ApplicationUser.Id == userId);

                return doctor != null ? doctor.Id : Guid.Empty;
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public DoctorViewModels GetDoctorProfileId(Guid doctorId)
        {
            DoctorViewModels doctorProfile;

            var doctor = _doctorRepository.Query()
                      .Include(x => x.ApplicationUser)
                      .Include(x => x.Payment)
                      .FirstOrDefault(x => x.Id == doctorId);


            doctorProfile = new DoctorViewModels
            {
                Id = doctor.Id,
                ApplicationUserId = doctor.ApplicationUserId,
                CityId = doctor.ApplicationUser.CityId ?? Guid.Empty,
                FirstName = doctor.ApplicationUser.FirstName,
                LastName = doctor.ApplicationUser.LastName,
                GenderId = (int)doctor.ApplicationUser.GenderEnum,
                HeardAboutUsId = doctor.ApplicationUser.HeardAboutUsId ?? 0,
                SecurityQuestionId = doctor.ApplicationUser.SecurityQuestionId ?? 0,
                SecurityQuestionAnswer = doctor.ApplicationUser.SecurityQuestionAnswer,
                NonUsStateOrProvince = doctor.ApplicationUser.NonUsStateOrProvince,
                Year = doctor.ApplicationUser.BirthDate.Value.Year.ToString(),
                Month = doctor.ApplicationUser.BirthDate.Value.Month.ToString(),
                Day = doctor.ApplicationUser.BirthDate.Value.Day.ToString(),
                Address1 = doctor.ApplicationUser.Address1,
                Phone = doctor.ApplicationUser.Phone,
                Address2 = doctor.ApplicationUser.Address2,
                ZipPostalCode = doctor.ApplicationUser.ZipPostalCode,
                MobilePhone = doctor.ApplicationUser.Mobile,
                Email = doctor.ApplicationUser.Email,
                CityName = doctor.ApplicationUser.City.Name,
                IsActive = doctor.IsActive,
                SpecialtyId = doctor.SpecialtieId,
                Refrances = doctor.Refrances,
                Experinces = doctor.Experinces,
                SpecialtyName = ApplicationContext.IsArabic ? doctor.Specialty.TitleAr : doctor.Specialty.TitleEn,
                Bio = doctor.Bio,
                fees = doctor.Fees,
                ResponsTime = doctor.ResponsTime,
                FocusOfScientific = doctor.FocusOfScientific,
                CreationDate = doctor.CreationDate,
                OnlineProfileLink = doctor.OnlineProfileLink
            };

            doctorProfile.Certificates = GetDoctrosCertificates(doctor.Id);
            doctorProfile.ProfileImage = GetDoctroProfileImage(doctor.Id);
            return doctorProfile;
        }

        public void Update(DoctorViewModels doctor)
        {
            //Edit
            var old_ = _doctorRepository.Get(doctor.Id);

            old_.Bio = doctor.Bio;
            old_.Experinces = doctor.Experinces;
            old_.SpecialtieId = doctor.SpecialtyId;
            old_.LastUpdatedAt = DateTime.Now;
            old_.Fees = doctor.fees;
            old_.ResponsTime = doctor.ResponsTime;
            old_.FocusOfScientific = doctor.FocusOfScientific;
            old_.OnlineProfileLink = doctor.OnlineProfileLink;

            _doctorRepository.Update(old_);
            _doctorRepository.UnitOfWork.SaveChanges();
        }

        public IQueryable<DoctorViewModels> GetDoctorsByspecialtyId(Guid specialtyId)
        {
            try
            {
                return _doctorRepository.Query().Where(c => c.IsActive)
                                                .Where(x => x.SpecialtieId == specialtyId)
                                                   .OrderByDescending(x => x.CreationDate)
                                                   .ToList()
                                                   .Select(x => new DoctorViewModels
                                                   {
                                                       Id = x.Id,
                                                       ApplicationUserId = x.ApplicationUserId,
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
                                                       SpecialtyId = x.SpecialtieId,
                                                       SpecialtyName = ApplicationContext.IsArabic ? x.Specialty.TitleAr : x.Specialty.TitleEn,
                                                       Bio = x.Bio,
                                                       Certificates = GetDoctrosCertificates(x.Id),
                                                       ProfileImage = GetDoctroProfileImage(x.Id),
                                                       fees = x.Fees,
                                                       ResponsTime = x.ResponsTime,
                                                       FocusOfScientific = x.FocusOfScientific,
                                                       OnlineProfileLink = x.OnlineProfileLink
                                                   }).AsQueryable();

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public bool CheckUserCompleteProfile(Guid userId)
        {
            try
            {
                return _doctorRepository.GetAll().ToList().Any(x => x.ApplicationUser.Id == userId);

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}
