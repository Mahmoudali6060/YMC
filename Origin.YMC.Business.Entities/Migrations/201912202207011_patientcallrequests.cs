namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientcallrequests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientCallRequests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CaseId = c.Guid(nullable: false),
                        Fees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ZoomUrl = c.String(),
                        IsConfirmed = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        LastUpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Case", t => t.CaseId, cascadeDelete: true)
                .Index(t => t.CaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientCallRequests", "CaseId", "dbo.Case");
            DropIndex("dbo.PatientCallRequests", new[] { "CaseId" });
            DropTable("dbo.PatientCallRequests");
        }
    }
}
