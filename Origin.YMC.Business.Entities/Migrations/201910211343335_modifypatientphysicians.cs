namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifypatientphysicians : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PatientPhysician", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.PatientPhysician", "MailingAddress", c => c.String(nullable: false));
            AlterColumn("dbo.PatientPhysician", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.PatientPhysician", "Fax", c => c.String(nullable: false));
            AlterColumn("dbo.PatientPhysician", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PatientPhysician", "Email", c => c.String());
            AlterColumn("dbo.PatientPhysician", "Fax", c => c.String());
            AlterColumn("dbo.PatientPhysician", "Phone", c => c.String());
            AlterColumn("dbo.PatientPhysician", "MailingAddress", c => c.String());
            AlterColumn("dbo.PatientPhysician", "Name", c => c.String());
        }
    }
}
