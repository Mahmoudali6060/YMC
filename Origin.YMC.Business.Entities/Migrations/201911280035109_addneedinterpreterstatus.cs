namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addneedinterpreterstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Case", "NeedInterpreterStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Case", "NeedInterpreterStatus");
        }
    }
}
