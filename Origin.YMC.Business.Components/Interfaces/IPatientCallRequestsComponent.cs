using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origin.YMC.Business.Entities.Domain.Patients;

namespace Origin.YMC.Business.Components.Interfaces
{
    public interface IPatientCallRequestsComponent
    {
        int Add(PatientCallRequestsViewModel patientCallRequestsViewModel);
        PatientCallRequestsViewModel GetListGetPageData(string searchValue, int requestStart, int requestLength, string orderColumnsName, int orderColumnsDirect);
        PatientCallRequestsViewModel Get(Guid id);
        Guid Update(PatientCallRequestsViewModel model);
        List<PatientCallRequestsViewModel> GetList(Guid patientId);
       int GetNotificationCount(Guid userId);

    }
}
