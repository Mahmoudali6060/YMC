namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addclosuredate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Case", "ClosureDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Case", "ClosureDate");
        }
    }
}
