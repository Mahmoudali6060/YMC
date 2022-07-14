namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDoctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctor", "Refrances", c => c.String());
            AddColumn("dbo.Doctor", "Experinces", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctor", "Experinces");
            DropColumn("dbo.Doctor", "Refrances");
        }
    }
}
