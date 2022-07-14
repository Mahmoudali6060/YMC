namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class interpreterentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Interpreter",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SpecialtieId = c.Guid(nullable: false),
                        ApplicationUserId = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        LastUpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Specialty", t => t.SpecialtieId, cascadeDelete: true)
                .Index(t => t.SpecialtieId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Interpreter", "SpecialtieId", "dbo.Specialty");
            DropForeignKey("dbo.Interpreter", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Interpreter", new[] { "ApplicationUserId" });
            DropIndex("dbo.Interpreter", new[] { "SpecialtieId" });
            DropTable("dbo.Interpreter");
        }
    }
}
