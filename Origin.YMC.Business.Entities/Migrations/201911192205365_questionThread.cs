namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class questionThread : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionThread",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CaseId = c.Guid(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        OwnerType = c.Int(nullable: false),
                        ChatText = c.String(),
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
            DropForeignKey("dbo.QuestionThread", "CaseId", "dbo.Case");
            DropIndex("dbo.QuestionThread", new[] { "CaseId" });
            DropTable("dbo.QuestionThread");
        }
    }
}
