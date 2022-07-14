using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.Patients;
using Origin.YMC.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using static System.String;

namespace Origin.YMC.Business.Components.Implementation
{
    public class PatientCallRequestsComponent : IPatientCallRequestsComponent
    {
        private readonly IRepository<PatientCallRequests> _patientCallRequestsRepository;
        private readonly ILogComponent _logComponent;

        public PatientCallRequestsComponent(IRepository<PatientCallRequests> patientCallRequestsRepository, ILogComponent logComponent)
        {
            _patientCallRequestsRepository = patientCallRequestsRepository;
            _logComponent = logComponent;
        }

        public int Add(PatientCallRequestsViewModel patientCallRequestsViewModel)
        {
            var status = -1;
            try
            {
                _patientCallRequestsRepository.Add(new PatientCallRequests
                {
                    CaseId = patientCallRequestsViewModel.CaseId,
                    CreatedBy = patientCallRequestsViewModel.PatientId,
                    CreationDate = DateTime.Now,
                    Fees = 5,
                    IsConfirmed = false,

                });
                status = _patientCallRequestsRepository.UnitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return status;
        }

        public PatientCallRequestsViewModel GetListGetPageData(string searchValue, int requestStart, int requestLength, string orderColumnsName,
            int orderColumnsDirect)
        {
            var callRequests = _patientCallRequestsRepository.Query();

            var filteredData = IsNullOrWhiteSpace(searchValue) ?
                                   callRequests :
                                    callRequests.Where(x =>
                                       x.Case.Patient.ApplicationUser.FirstName.ToLower().Contains(searchValue)
                                    || x.Case.Patient.ApplicationUser.LastName.ToLower().Contains(searchValue)
                                       || x.ZoomUrl.ToLower().Contains(searchValue)
                                       || x.Case.OpinionID.ToString() == searchValue

                                       );

            switch (orderColumnsName.ToLower())
            {
                case "PatientName":
                    filteredData = orderColumnsDirect == 0 ? filteredData.OrderBy(c => c.Case.Patient.ApplicationUser.FirstName) : filteredData.OrderByDescending(c => c.Case.Patient.ApplicationUser.FirstName);
                    break;
                case "CaseId":
                    filteredData = orderColumnsDirect == 0 ? filteredData.OrderBy(c => c.Case.OpinionID) : filteredData.OrderByDescending(c => c.Case.OpinionID);
                    break;
                default:
                    filteredData = orderColumnsDirect == 0 ? filteredData.OrderByDescending(c => c.CreationDate) : filteredData.OrderBy(c => c.CreationDate);
                    break;
            }

            var dataPage = filteredData.Skip(requestStart).Take(requestLength);

            var viewModel = new PatientCallRequestsViewModel
            {
                TotalDatalenght = callRequests.Count(),
                FilterDatalenght = filteredData.Count(),
                Items = dataPage.Select(c => new PatientCallRequestsViewModel
                {
                    Id = c.Id,
                    PatientId = c.Case.PatientId,
                    PatientName = c.Case.Patient.ApplicationUser.FirstName + " " + c.Case.Patient.ApplicationUser.LastName,
                    CreatedBy = c.CreatedBy,
                    CreationDate = c.CreationDate,
                    IsActive = c.IsActive,
                    LastUpdatedAt = c.LastUpdatedAt,
                    LastUpdatedBy = c.LastUpdatedBy,
                    CaseId = c.CaseId,
                    Fees = c.Fees,
                    IsConfirmed = c.IsConfirmed,
                    CaseCode = c.Case.DoctorReportDiagnosis,
                    ZoomUrl = c.ZoomUrl,
                    MeetingTime = c.MeetingTime,
                    MeetingDate = c.MeetingDate
                }).ToList()
            };
            return viewModel;
        }
        public PatientCallRequestsViewModel Get(Guid id)
        {
            var item = _patientCallRequestsRepository.Query().Include(a => a.Case.Patient.ApplicationUser).FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                return new PatientCallRequestsViewModel
                {
                    Id = item.Id,
                    PatientId = item.Case.PatientId,
                    PatientName = item.Case.Patient.ApplicationUser.FirstName + " " +
                    item.Case.Patient.ApplicationUser.LastName,
                    CreatedBy = item.CreatedBy,
                    CreationDate = item.CreationDate,
                    IsActive = item.IsActive,
                    LastUpdatedAt = item.LastUpdatedAt,
                    LastUpdatedBy = item.LastUpdatedBy,
                    CaseId = item.CaseId,
                    Fees = item.Fees,
                    IsConfirmed = item.IsConfirmed,
                    CaseCode = item.Case.DoctorReportDiagnosis,
                    ZoomUrl = item.ZoomUrl,
                    MeetingTime = item.MeetingTime,
                    MeetingDate = item.MeetingDate,
                  MeetingDateStr = item.MeetingDate.HasValue? item.MeetingDate.Value.ToString("yyyy-MM-dd") :""
                };
            }

            return new PatientCallRequestsViewModel();
        }

        public Guid Update(PatientCallRequestsViewModel model)
        {
            var old = _patientCallRequestsRepository.Get(model.Id);
            old.ZoomUrl = model.ZoomUrl;
            old.MeetingTime = model.MeetingTime;
            old.MeetingDate = model.MeetingDate;
            old.IsConfirmed = true;
            old.LastUpdatedAt = DateTime.Now;
            _patientCallRequestsRepository.Update(old);
            _patientCallRequestsRepository.UnitOfWork.SaveChanges();
            return old.Case.PatientId;
        }

        public List<PatientCallRequestsViewModel> GetList(Guid patientId)
        {

            var callRequests = _patientCallRequestsRepository.Query()
                .Where(c => c.IsActive &&c.IsConfirmed&& c.Case.Patient.ApplicationUserId == patientId)
                .OrderByDescending(a => a.CreationDate)
                .Include(a => a.Case)
                .Include(a => a.Case.Doctor.ApplicationUser)
                .Include(a => a.Case.Patient)
                .ToList();
            var lstTestimonialViewModel = new List<PatientCallRequestsViewModel>();
            foreach (var item in callRequests)
            {
                lstTestimonialViewModel.Add(new PatientCallRequestsViewModel
                {
                    Id = item.Id,
                    PatientId = item.Case.PatientId,
                    PatientName = item.Case.Patient.ApplicationUser.FirstName + " " + item.Case.Patient.ApplicationUser.LastName,
                    CreatedBy = item.CreatedBy,
                    CreationDate = item.CreationDate,
                    IsActive = item.IsActive,
                    LastUpdatedAt = item.LastUpdatedAt,
                    LastUpdatedBy = item.LastUpdatedBy,
                    CaseId = item.CaseId,
                    Fees = item.Fees,
                    IsConfirmed = item.IsConfirmed,
                    CaseCode = item.Case.OpinionID.ToString(),
                    ZoomUrl = item.ZoomUrl,
                    DoctorName = item.Case.Doctor.ApplicationUser.FirstName+" "+ item.Case.Doctor.ApplicationUser.LastName,
                    MeetingTime = item.MeetingTime,
                    MeetingDate = item.MeetingDate
                    
                });
            }
            return lstTestimonialViewModel;
        }

        public int GetNotificationCount(Guid userId)
        {
           return _patientCallRequestsRepository.Query()
               .Count(a => a.Case.Patient.ApplicationUserId == userId&&a.IsActive && a.IsConfirmed);
        }
    }
}
