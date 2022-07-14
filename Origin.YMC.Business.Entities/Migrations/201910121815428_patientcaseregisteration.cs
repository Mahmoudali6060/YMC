namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientcaseregisteration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Case",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DoctorId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        Saluation = c.Int(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        ShippingAddress = c.String(),
                        CountryId = c.Guid(),
                        Region = c.String(),
                        CityId = c.Guid(),
                        ZipCode = c.Int(),
                        PrimaryPhoneType = c.Int(),
                        SecondaryPhoneType = c.Int(),
                        PrimaryPhone = c.String(),
                        SecondaryPhone = c.String(),
                        Email = c.String(),
                        BirthDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Gender = c.Int(),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        LastUpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.CityId)
                .ForeignKey("dbo.Country", t => t.CountryId)
                .Index(t => t.CountryId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.CasePatientQuestionsAnswers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CaseId = c.Guid(nullable: false),
                        PrimaryCheifProblem = c.String(),
                        MedicalDiagnosis = c.String(),
                        DiagnosisDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DoYouHadWorkerCompensation = c.Boolean(nullable: false),
                        DoYouHadLitigation = c.Boolean(nullable: false),
                        AreYouCurrentlyHospitalized = c.Boolean(nullable: false),
                        DoYouHaveSurgery = c.Boolean(nullable: false),
                        SurgeryDates = c.String(),
                        NoneOfTheQuestions = c.Boolean(nullable: false),
                        TreatmentRecommendations = c.String(),
                        FirstQuestion = c.String(),
                        SecondQuestion = c.String(),
                        ThirdQuestion = c.String(),
                        PatientAge = c.String(),
                        PatientHeight = c.String(),
                        PatientWeight = c.String(),
                        EthnicOrigin = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        LastUpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PatientPhysician",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        MailingAddress = c.String(),
                        Phone = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        CasePatientQuestionsAnswersId = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        LastUpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CasePatientQuestionsAnswers", t => t.CasePatientQuestionsAnswersId, cascadeDelete: true)
                .Index(t => t.CasePatientQuestionsAnswersId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientPhysician", "CasePatientQuestionsAnswersId", "dbo.CasePatientQuestionsAnswers");
            DropForeignKey("dbo.Case", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Case", "CityId", "dbo.City");
            DropIndex("dbo.PatientPhysician", new[] { "CasePatientQuestionsAnswersId" });
            DropIndex("dbo.Case", new[] { "CityId" });
            DropIndex("dbo.Case", new[] { "CountryId" });
            DropTable("dbo.PatientPhysician");
            DropTable("dbo.CasePatientQuestionsAnswers");
            DropTable("dbo.Case");
        }
    }
}
