namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class medicalreportdiagnostic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Case", "DoctorReportDiagnosisInterpreted", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Case", "DoctorReportDiagnosisInterpreted");
        }
    }
}
