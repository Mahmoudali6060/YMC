namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cityArabicName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.City", "NameAr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.City", "NameAr");
        }
    }
}
