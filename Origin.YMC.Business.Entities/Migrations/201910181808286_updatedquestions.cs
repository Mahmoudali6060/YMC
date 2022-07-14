namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedquestions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CasePatientQuestionsAnswers", "OtherEthinicOrigin", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CasePatientQuestionsAnswers", "OtherEthinicOrigin");
        }
    }
}
