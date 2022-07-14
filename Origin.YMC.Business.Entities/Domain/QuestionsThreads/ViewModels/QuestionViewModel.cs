using Origin.YMC.Business.Entities.Domain.Doctors.ViewModel;
using Origin.YMC.Business.Entities.Domain.Patients.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.QuestionsThreads.ViewModels
{
    public class QuestionViewModel
    {
        public DoctorViewModels Doctor { get; set; }
        public PatientViewModels Patient { get; set; }
        public List<QuestionThreadViewModel> Questions { get; set; }
        public string DoctorReportDiagnosis { get; set; }
        public Guid CaseId { get; set; }
        public DateTime? ClosureDate { get; set; }
        public List<string> CasseAttchment { get; set; }
        public List<string> CasseAttchmentInterpreted { get; set; }

    }
}
