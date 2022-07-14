using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities.Domain.Patients
{
  public  class PatientPhysician :EntityBase
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string MailingAddress { get; set; }
        [Required]

        public string Phone { get; set; }
        [Required]

        public string Fax { get; set; }
        [Required]

        public string Email { get; set; }
        [ForeignKey("CasePatientQuestionsAnswers")]

        public Guid CasePatientQuestionsAnswersId { get; set; }

        public CasePatientQuestionsAnswers CasePatientQuestionsAnswers { get; set; }

    }
}
