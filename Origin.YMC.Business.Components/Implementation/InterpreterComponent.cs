using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.Interpreter;
using Origin.YMC.Business.Entities.Domain.Interpreter.ViewModels;
using Origin.YMC.Business.Entities.Domain.Patients;
using Origin.YMC.Core.Common;
using Origin.YMC.Repositories;
using System;
using System.Data.Entity;
using System.Linq;
using Origin.YMC.Business.Entities.Domain.Patients.Enums;

namespace Origin.YMC.Business.Components.Implementation
{
    public class InterpreterComponent : IInterpreterComponent
    {
        private readonly ILogComponent _logComponent;
        private readonly IRepository<Interpreter> _interpreterRepository;
        private readonly IRepository<InterpretationFees> _interpretationFees;
        private readonly IRepository<Case> _caseRepository;

        public InterpreterComponent(ILogComponent logComponent, IRepository<Interpreter> interpreterRepository, IRepository<InterpretationFees> interpretationFees, IRepository<Case> caseRepository)
        {
            _logComponent = logComponent;
            _interpreterRepository = interpreterRepository;
            _interpretationFees = interpretationFees;
            _caseRepository = caseRepository;
        }
        public void Add(InterpreterViewModels Interpreter)
        {
            try
            {

                _interpreterRepository.Add(new Interpreter()
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = Interpreter.ApplicationUserId,
                    CreatedBy = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    //SpecialtieId = Interpreter.SpecialtyId,

                });
                _interpreterRepository.UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public ListInterpreterViewModel GetPageData(string searchValue, int requestStart, int requestLenght, string orderColumsName,
            int orderColumsDirect)

        {
            IQueryable<Interpreter> patients = _interpreterRepository.Query();

            IQueryable<Interpreter> filteredData = string.IsNullOrWhiteSpace(searchValue) ?
                                   patients :
                                     patients.Where(x =>
                                                      x.ApplicationUser.FirstName.Contains(searchValue)
                                                   || x.ApplicationUser.LastName.Contains(searchValue)
                                                   || x.ApplicationUser.NonUsStateOrProvince.Contains(searchValue)
                                                   || x.ApplicationUser.Mobile.Contains(searchValue)
                                                   //|| x.Specialty.TitleEn.Contains(searchValue)
                                                   || x.ApplicationUser.Email.Contains(searchValue));

            switch (orderColumsName.ToLower())
            {
                case "firstName":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.ApplicationUser.FirstName) : filteredData.OrderByDescending(c => c.ApplicationUser.FirstName);
                    break;
                case "lastName":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.ApplicationUser.LastName) : filteredData.OrderByDescending(c => c.ApplicationUser.LastName);
                    break;
                case "mobile":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.ApplicationUser.Mobile) : filteredData.OrderByDescending(c => c.ApplicationUser.Mobile);
                    break;
                case "email":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.ApplicationUser.Email) : filteredData.OrderByDescending(c => c.ApplicationUser.Email);
                    break;
                default:
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderByDescending(c => c.CreationDate) : filteredData.OrderBy(c => c.CreationDate);
                    break;
            }

            IQueryable<Interpreter> dataPage = filteredData.Skip(requestStart).Take(requestLenght);

            ListInterpreterViewModel viewModel = new ListInterpreterViewModel
            {
                TotalDatalenght = patients.Count(),
                FilterDatalenght = filteredData.Count(),
                Items = dataPage
                .ToList()
                .Select(x => new InterpreterViewModels()
                {
                    Id = x.Id,
                    ApplicationUserId = x.ApplicationUserId,
                    FirstName = x.ApplicationUser.UserName,
                    LastName = "",
                    GenderId = 1,
                    Year = x.ApplicationUser.BirthDate?.Year.ToString(),
                    Month = x.ApplicationUser.BirthDate?.Month.ToString(),
                    Day = x.ApplicationUser.BirthDate?.Day.ToString(),
                    Email = x.ApplicationUser.Email,
                    IsActive = x.IsActive,
                    //SpecialtyId = x.SpecialtieId,
                    //SpecialtyName = ApplicationContext.IsArabic ? x.Specialty.TitleAr : x.Specialty.TitleEn,
                    CreationDate = x.CreationDate
                }).ToList()
            };
            return viewModel;
        }

        public bool ActivateDeactivateInterpreter(Guid id)
        {

            try
            {
                Interpreter oldInterpreter = _interpreterRepository.Get(id);
                oldInterpreter.IsActive = !oldInterpreter.IsActive;

                _interpreterRepository.Update(oldInterpreter);
                return _interpreterRepository.UnitOfWork.SaveChanges() >= 0;
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message,  System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }

        }

        public decimal GetInterpretationFees(int interpretationTypeId)
        {
            decimal fees = 0;
            try
            {
                InterpretationFees interpretationFees = _interpretationFees.Query().FirstOrDefault(a => a.InterpretationTypeId == interpretationTypeId);
                if (interpretationFees != null)
                {
                    fees = interpretationFees.Fees;
                }
            }
            catch (Exception e)
            {
                _logComponent.LogError(e.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }

            return fees;
        }

        public int ChangeInterpreter(Guid caseId, Guid interpreterId)
        {
            var status = -1;
            try
            {
                var currentCase = _caseRepository.Get(caseId);
                var interpreter = _interpreterRepository
                                      .Query()
                                      .FirstOrDefault(a => a.Id != interpreterId &&
                                                           a.IsActive && Enumerable.Count(a.Cases,
                                                               c => c.AssignedToInterpreterId.HasValue) < 10) ??
                                  _interpreterRepository.Query().FirstOrDefault(a => a.IsActive&& a.Id != interpreterId);


                if (interpreter != null)
                {
                    currentCase.AssignedToInterpreterId = interpreter.Id;
                    _caseRepository.Update(currentCase);
                    status= _caseRepository.UnitOfWork.SaveChanges();

                }
                else
                {
                    status = -2;
                }
            }
            catch (Exception e)
            {
                _logComponent.LogError(e.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }

            return status;
        }

        public Guid AssignInterpreterToCase(Guid caseId)
        {
            try
            {
               
                var interpreter = _interpreterRepository
                                      .Query()
                                      .FirstOrDefault(a =>
                                          a.IsActive && Enumerable.Count(a.Cases, c => c.AssignedToInterpreterId.HasValue) < 10) ??
                                  _interpreterRepository.Query().FirstOrDefault(a => a.IsActive );
                 

                var oldCase = _caseRepository.Get(caseId);
                if (interpreter != null)
                {
                    oldCase.AssignedToInterpreterId = interpreter.Id;
                    oldCase.Status = StatusEnum.CaseUnderInterpretation;
                    _caseRepository.Update(oldCase);
                    _caseRepository.UnitOfWork.SaveChanges();

                    return interpreter.Id;
                }
            }
            catch (Exception e)
            {
                _logComponent.LogError  (e.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }

            return new Guid();
        }
        public Guid GetInterpreterId(Guid userId)
        {
            var interpreter = _interpreterRepository.Query()
                .Include(x => x.ApplicationUser)
                .FirstOrDefault(x => x.ApplicationUser.Id == userId);

            return interpreter?.Id ?? Guid.Empty;
        }

        public Guid GetUserId(Guid interpreterId)
        {
            var interpreter = _interpreterRepository.Query()
                .Include(x => x.ApplicationUser)
                .Where(x => x.Id == interpreterId)
                .Select(a=>a.ApplicationUserId).FirstOrDefault();

            return interpreter;
        }
    }
}
