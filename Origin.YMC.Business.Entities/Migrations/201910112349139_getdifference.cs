namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class getdifference : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Doctor", "Bio");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctor", "Bio", c => c.String());
        }
    }
}
