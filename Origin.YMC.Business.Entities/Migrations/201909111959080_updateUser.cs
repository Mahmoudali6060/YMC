namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "CityId", "dbo.City");
            DropIndex("dbo.AspNetUsers", new[] { "CityId" });
            AlterColumn("dbo.AspNetUsers", "CityId", c => c.Guid());
            CreateIndex("dbo.AspNetUsers", "CityId");
            AddForeignKey("dbo.AspNetUsers", "CityId", "dbo.City", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CityId", "dbo.City");
            DropIndex("dbo.AspNetUsers", new[] { "CityId" });
            AlterColumn("dbo.AspNetUsers", "CityId", c => c.Guid(nullable: false));
            CreateIndex("dbo.AspNetUsers", "CityId");
            AddForeignKey("dbo.AspNetUsers", "CityId", "dbo.City", "Id", cascadeDelete: true);
        }
    }
}
