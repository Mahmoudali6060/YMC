namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResponeTimeDoctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctor", "ResponsTime", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctor", "ResponsTime");
        }
    }
}
