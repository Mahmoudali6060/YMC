namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removespecialityinterpreter : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Interpreter", "SpecialtieId", "dbo.Specialty");
            DropIndex("dbo.Interpreter", new[] { "SpecialtieId" });
            DropColumn("dbo.Interpreter", "SpecialtieId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Interpreter", "SpecialtieId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Interpreter", "SpecialtieId");
            AddForeignKey("dbo.Interpreter", "SpecialtieId", "dbo.Specialty", "Id", cascadeDelete: true);
        }
    }
}
