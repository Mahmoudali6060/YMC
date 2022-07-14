namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editDoctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctor", "Bio", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctor", "Bio");
        }
    }
}
