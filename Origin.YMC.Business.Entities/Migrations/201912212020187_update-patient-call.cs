namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepatientcall : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientCallRequests", "MeetingDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.PatientCallRequests", "MeetingTime", c => c.Time(precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientCallRequests", "MeetingTime");
            DropColumn("dbo.PatientCallRequests", "MeetingDate");
        }
    }
}
