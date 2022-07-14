using Origin.YMC.Business.Entities.Domain.Patients.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Components.Interfaces
{
    public interface IPatientComponent
    {
        void Add(PatientViewModels patient);
        bool ActivateDeactivatePatient(Guid id);
        IQueryable<PatientViewModels> List(string filter);
        PatientViewModels GetPatientProfileId(Guid patientId);
        Guid GetPatientId(Guid userId);
        ListPatientViewModel GetPageData(string searchValue, int requestStart, int requestLenght, string orderColumsName, int orderColumsDirect);
        bool CheckUserCompleteProfile(Guid userId);
        Guid GetUserId(Guid patientId);
    }
}
