using Origin.YMC.Business.Entities.Domain.Cities;
using Origin.YMC.Business.Entities.Domain.Countries;
using Origin.YMC.Business.Entities.Domain.Doctors;
using Origin.YMC.Business.Entities.Domain.Patients.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origin.YMC.Business.Entities.Domain.Interpreter.Enums;

namespace Origin.YMC.Business.Entities.Domain.Patients
{
    public class Case : EntityBase
    {
        [ForeignKey("Doctor")]

        public Guid DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public string DoctorReportDiagnosis { get; set; }
        public string DoctorReportDiagnosisInterpreted { get; set; }

        public StatusEnum Status { get; set; }
        public Nullable<SaluationEnum> Saluation { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public Country Country { get; set; }

        [ForeignKey("Country")]
        public Nullable<Guid> CountryId { get; set; }

        public string Region { get; set; }

        public City City { get; set; }
        [ForeignKey("City")]

        public Nullable<Guid> CityId { get; set; }
        [ForeignKey("Interpreter")]
        public Nullable<Guid> AssignedToInterpreterId { get; set; }

        public int? ZipCode { get; set; }

        public PhoneTypeEnum? PrimaryPhoneType { get; set; }

        public PhoneTypeEnum? SecondaryPhoneType { get; set; }

        public string PrimaryPhone { get; set; }

        public string SecondaryPhone { get; set; }

        public string Email { get; set; }

        public DateTime? BirthDate { get; set; }
        public DateTime? ClosureDate { get; set; }

        public GenderEnum? Gender { get; set; }

        [ForeignKey("Patient")]

        public Guid PatientId { get; set; }

        public Patient Patient { get; set; }

        public int OpinionID { get; set; }


        public InterpretationTypes InterpretationTypeId { get; set; }
        public Interpreter.Interpreter Interpreter { get; set; }
    }
}
