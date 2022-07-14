namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfeestodoctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctor", "Fees", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctor", "Fees");
        }
    }
}
