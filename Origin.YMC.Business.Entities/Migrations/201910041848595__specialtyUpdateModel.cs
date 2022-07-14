namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _specialtyUpdateModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Specialty", "TitleAr", c => c.String());
            AddColumn("dbo.Specialty", "TitleEn", c => c.String());
            AddColumn("dbo.Specialty", "DescriptionAr", c => c.String());
            AddColumn("dbo.Specialty", "DescriptionEn", c => c.String());
            DropColumn("dbo.Specialty", "Name");
            DropColumn("dbo.Specialty", "ArabicName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Specialty", "ArabicName", c => c.String());
            AddColumn("dbo.Specialty", "Name", c => c.String());
            DropColumn("dbo.Specialty", "DescriptionEn");
            DropColumn("dbo.Specialty", "DescriptionAr");
            DropColumn("dbo.Specialty", "TitleEn");
            DropColumn("dbo.Specialty", "TitleAr");
        }
    }
}
