namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class countryArabicName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Country", "NameAr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Country", "NameAr");
        }
    }
}
