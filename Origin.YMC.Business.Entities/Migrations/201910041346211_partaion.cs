namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class partaion : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Specialties", newName: "Specialty");
            CreateTable(
                "dbo.Partation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContentAr = c.String(),
                        ContentEn = c.String(),
                        PartationTypeID = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        LastUpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartationType", t => t.PartationTypeID, cascadeDelete: true)
                .Index(t => t.PartationTypeID);
            
            CreateTable(
                "dbo.PartationType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TitleAr = c.String(),
                        TitleEn = c.String(),
                        OrderPostionAppear = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        LastUpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Social",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TitleAr = c.String(),
                        TitleEn = c.String(),
                        Url = c.String(),
                        Type = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        LastUpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Specialty", "ArabicName", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Partation", "PartationTypeID", "dbo.PartationType");
            DropIndex("dbo.Partation", new[] { "PartationTypeID" });
            DropColumn("dbo.Specialty", "ArabicName");
            DropTable("dbo.Social");
            DropTable("dbo.PartationType");
            DropTable("dbo.Partation");
            RenameTable(name: "dbo.Specialty", newName: "Specialties");
        }
    }
}
