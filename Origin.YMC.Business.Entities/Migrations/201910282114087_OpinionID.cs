namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OpinionID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Case", "OpinionID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Case", "OpinionID");
        }
    }
}
