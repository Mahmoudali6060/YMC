namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addforeignkeyforDoctor : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Case", "DoctorId");
            AddForeignKey("dbo.Case", "DoctorId", "dbo.Doctor", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Case", "DoctorId", "dbo.Doctor");
            DropIndex("dbo.Case", new[] { "DoctorId" });
        }
    }
}
