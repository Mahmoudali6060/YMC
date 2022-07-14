namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestimonialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Testimonial",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PatientId = c.Guid(nullable: false),
                        TextReview = c.String(),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        LastUpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patient", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            AddColumn("dbo.Specialty", "AttachmentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Specialty", "AttachmentId");
            AddForeignKey("dbo.Specialty", "AttachmentId", "dbo.Attachment", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Testimonial", "PatientId", "dbo.Patient");
            DropForeignKey("dbo.Specialty", "AttachmentId", "dbo.Attachment");
            DropIndex("dbo.Testimonial", new[] { "PatientId" });
            DropIndex("dbo.Specialty", new[] { "AttachmentId" });
            DropColumn("dbo.Specialty", "AttachmentId");
            DropTable("dbo.Testimonial");
        }
    }
}
