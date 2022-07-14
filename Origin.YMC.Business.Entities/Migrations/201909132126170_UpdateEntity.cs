namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Specialties",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        LastUpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Payment", "Country", c => c.String());
            DropColumn("dbo.Payment", "CountryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payment", "CountryId", c => c.Int(nullable: false));
            DropColumn("dbo.Payment", "Country");
            DropTable("dbo.Specialties");
        }
    }
}
