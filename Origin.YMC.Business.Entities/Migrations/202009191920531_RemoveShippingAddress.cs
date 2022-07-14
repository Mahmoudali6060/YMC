namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveShippingAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Case", "Address1", c => c.String());
            AddColumn("dbo.Case", "Address2", c => c.String());
            DropColumn("dbo.Case", "ShippingAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Case", "ShippingAddress", c => c.String());
            DropColumn("dbo.Case", "Address2");
            DropColumn("dbo.Case", "Address1");
        }
    }
}
