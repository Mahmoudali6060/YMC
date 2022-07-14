namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDoctorResportCase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Case", "DoctorReportDiagnosis", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Case", "DoctorReportDiagnosis");
        }
    }
}
