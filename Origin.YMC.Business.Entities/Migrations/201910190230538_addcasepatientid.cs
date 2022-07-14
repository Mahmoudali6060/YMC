namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcasepatientid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Case", "PatientId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Case", "PatientId");
        }
    }
}
