namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entityMissing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        Name = c.String(),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        LastUpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CardholderFullName = c.String(),
                        CreditCardNumber = c.String(),
                        CVV = c.String(),
                        CreditCardType = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        State = c.String(),
                        City = c.String(),
                        CountryId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        LastUpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.City", "CountryCode", c => c.String());
            AddColumn("dbo.City", "PostalCode", c => c.String());
            AddColumn("dbo.AspNetUsers", "HeardAboutUsId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "SecurityQuestionId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "SecurityQuestionAnswer", c => c.String());
            AddColumn("dbo.AspNetUsers", "NonUsStateOrProvince", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address1", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address2", c => c.String());
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String());
            AddColumn("dbo.AspNetUsers", "ZipPostalCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ZipPostalCode");
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "Address2");
            DropColumn("dbo.AspNetUsers", "Address1");
            DropColumn("dbo.AspNetUsers", "NonUsStateOrProvince");
            DropColumn("dbo.AspNetUsers", "SecurityQuestionAnswer");
            DropColumn("dbo.AspNetUsers", "SecurityQuestionId");
            DropColumn("dbo.AspNetUsers", "HeardAboutUsId");
            DropColumn("dbo.City", "PostalCode");
            DropColumn("dbo.City", "CountryCode");
            DropTable("dbo.Payment");
            DropTable("dbo.Country");
        }
    }
}
