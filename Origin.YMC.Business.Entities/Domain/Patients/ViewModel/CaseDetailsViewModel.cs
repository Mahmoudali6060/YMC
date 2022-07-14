using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.Patients.ViewModel
{
    public class CaseDetailsViewModel
    {
        public CasePatientQuestionsViewModel CasePatientQuestions { get; set; }
        public CasePatientInfoViewModel CasePatientInfo { get; set; }
        public CaseListingViewModel CaseInfo { get; set; }
        public List<string> Attchments { get; set; }
        public List<string> CaseDocuments { get; set; }
        public List<string> CaseDocumentsInterpreted { get; set; }
 
    }
}
