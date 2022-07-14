using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using Origin.YMC.Business.Entities.Domain;
using Origin.YMC.Business.Entities.Domain.Cities;
using Origin.YMC.Business.Entities.Domain.Log;
using Origin.YMC.Business.Entities.Domain.Countries;
using Origin.YMC.Business.Entities.Domain.Payments;
using Origin.YMC.Business.Entities.Lockup;
using Origin.YMC.Business.Entities.Domain.Patients;
using Origin.YMC.Business.Entities.Domain.Doctors;
using Origin.YMC.Business.Entities.Domain.Interpreter;
using Origin.YMC.Business.Entities.Domain.Specialties;
using Origin.YMC.Business.Entities.Domain.SocialUrl;
using Origin.YMC.Business.Entities.Domain.Partation;
using Origin.YMC.Business.Entities.Domain.Testimonials;
using Origin.YMC.Business.Entities.Domain.QuestionsThreads;

namespace Origin.YMC.Business.Entities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, UserLogin, UserRole, UserClaim>
    {

        public virtual DbSet<Attachment> Attachment { get; set; }
        public virtual DbSet<AttachmentType> AttachmentType { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Specialty> Specialty { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Social> Social { get; set; }
        public virtual DbSet<PartationType> PartationType { get; set; }
        public virtual DbSet<Partation> Partation { get; set; }
        public virtual DbSet<Testimonial> Testimonial { get; set; }
        public virtual DbSet<PatientPhysician> PatientPhysician { get; set; }
        public virtual DbSet<Case> Case { get; set; }
        public virtual DbSet<CasePatientQuestionsAnswers> CasePatientQuestionsAnswers { get; set; }
        public virtual DbSet<QuestionThread> QuestionThread { get; set; }
        public virtual DbSet<Interpreter> Interpreter { get; set; }
        public virtual DbSet<InterpretationFees> InterpretationFees { get; set; }
        public virtual DbSet<PatientCallRequests> PatientCallRequests { get; set; }

        

        public ApplicationDbContext()
            : base("ApplicationDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<DateTime>()
           .Configure(c => c.HasColumnType("datetime2"));
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
