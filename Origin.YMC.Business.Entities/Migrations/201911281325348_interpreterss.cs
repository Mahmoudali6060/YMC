namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class interpreterss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InterpretationFees",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        InterpretationTypeId = c.Int(nullable: false),
                        Fees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        LastUpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Case", "InterpretationTypeId", c => c.Int(nullable: false));
            DropColumn("dbo.Case", "NeedInterpreterStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Case", "NeedInterpreterStatus", c => c.Int(nullable: false));
            DropColumn("dbo.Case", "InterpretationTypeId");
            DropTable("dbo.InterpretationFees");
        }
    }
}
