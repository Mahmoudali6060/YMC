namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Patient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationUserId = c.Guid(nullable: false),
                        PaymentId = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        LastUpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Payment", t => t.PaymentId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.PaymentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patient", "PaymentId", "dbo.Payment");
            DropForeignKey("dbo.Patient", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Patient", new[] { "PaymentId" });
            DropIndex("dbo.Patient", new[] { "ApplicationUserId" });
            DropTable("dbo.Patient");
        }
    }
}
