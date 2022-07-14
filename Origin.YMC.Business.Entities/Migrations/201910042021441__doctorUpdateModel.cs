namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _doctorUpdateModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctor", "SpecialtieId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Doctor", "SpecialtieId");
            AddForeignKey("dbo.Doctor", "SpecialtieId", "dbo.Specialty", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctor", "SpecialtieId", "dbo.Specialty");
            DropIndex("dbo.Doctor", new[] { "SpecialtieId" });
            AlterColumn("dbo.Doctor", "SpecialtieId", c => c.Int(nullable: false));
        }
    }
}
