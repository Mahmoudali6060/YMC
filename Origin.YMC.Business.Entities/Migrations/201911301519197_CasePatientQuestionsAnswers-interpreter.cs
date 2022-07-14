namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CasePatientQuestionsAnswersinterpreter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CasePatientQuestionsAnswers", "PrimaryCheifProblemInterpreted", c => c.String());
            AddColumn("dbo.CasePatientQuestionsAnswers", "MedicalDiagnosisInterpreted", c => c.String());
            AddColumn("dbo.CasePatientQuestionsAnswers", "FirstQuestionInterpreted", c => c.String());
            AddColumn("dbo.CasePatientQuestionsAnswers", "SecondQuestionInterpreted", c => c.String());
            AddColumn("dbo.CasePatientQuestionsAnswers", "ThirdQuestionInterpreted", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CasePatientQuestionsAnswers", "ThirdQuestionInterpreted");
            DropColumn("dbo.CasePatientQuestionsAnswers", "SecondQuestionInterpreted");
            DropColumn("dbo.CasePatientQuestionsAnswers", "FirstQuestionInterpreted");
            DropColumn("dbo.CasePatientQuestionsAnswers", "MedicalDiagnosisInterpreted");
            DropColumn("dbo.CasePatientQuestionsAnswers", "PrimaryCheifProblemInterpreted");
        }
    }
}
