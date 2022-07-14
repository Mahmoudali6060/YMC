namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FocusOfScientificDoctor1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctor", "FocusOfScientific", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctor", "FocusOfScientific");
        }
    }
}
