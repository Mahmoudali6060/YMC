namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnlineProfileDoctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctor", "OnlineProfileLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctor", "OnlineProfileLink");
        }
    }
}
