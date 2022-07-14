using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.Countries;
using Origin.YMC.Business.Entities.Domain.Patients;
using Origin.YMC.Business.Entities.Domain.Doctors;

using Origin.YMC.Business.Entities.Domain.Statistics.ViewModels;
using Origin.YMC.Repositories;

namespace Origin.YMC.Business.Components.Implementation
{
    public class StatisticsComponent : IStatisticsComponent
    {
        private readonly ILogComponent _logComponent;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Case> _caseRepository;
        private readonly IRepository<Patient> _patientRepository;

        public StatisticsComponent(ILogComponent logComponent, IRepository<Country> countryRepository, IRepository<Doctor> doctorRepository, IRepository<Case> caseRepository, IRepository<Patient> patientRepository)
        {
            _logComponent = logComponent;
            _countryRepository = countryRepository;
            _doctorRepository = doctorRepository;
            _caseRepository = caseRepository;
            _patientRepository = patientRepository;
        }

        public FactsViewModels GetAllFacts()
        {
            try
            {
                return new FactsViewModels()
                {
                    TotalCases = _caseRepository.Query().Count(a=>a.IsActive),
                    TotalCountries = _countryRepository.Query().Count(a=>a.IsActive),
                    TotalDoctors = _doctorRepository.Query().Count(a=>a.IsActive),
                    TotalPatients = _patientRepository.Query().Count(a=>a.IsActive)
                };
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}
