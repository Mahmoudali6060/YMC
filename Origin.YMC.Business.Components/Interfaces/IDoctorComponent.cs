using Origin.YMC.Business.Entities.Domain.Doctors.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Components.Interfaces
{
    public interface IDoctorComponent
    {
        void Add(DoctorViewModels doctor);
        Guid AddThenGet(DoctorViewModels doctor);
        bool ActivateDeactivateDoctor(Guid id);
        ListDoctorViewModel GetPageData(string searchValue, int requestStart, int requestLenght, string orderColumsName, int orderColumsDirect);
        IQueryable<DoctorViewModels> List(string filter);
        DoctorViewModels GetDoctorProfileId(Guid doctorId);
        IQueryable<DoctorViewModels> GetDoctorsByspecialtyId(Guid specialtyId);
        Guid GetDoctorId(Guid userId);
        void Update(DoctorViewModels doctor);
        decimal GetDoctorFees(Guid id);

        bool CheckUserCompleteProfile(Guid userId);
        IQueryable<DoctorViewModels> Search(string specialtyName, string price);
        string GetDoctorProfileImage(Guid Id);
    }
}
