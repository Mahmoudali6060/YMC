namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addforeignkeyforpatient : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Case", "PatientId");
            AddForeignKey("dbo.Case", "PatientId", "dbo.Patient", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Case", "PatientId", "dbo.Patient");
            DropIndex("dbo.Case", new[] { "PatientId" });
        }
    }
}
