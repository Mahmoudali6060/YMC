namespace Origin.YMC.Business.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class interpreter1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Case", "AssignedToInterpreterId", c => c.Guid());
            CreateIndex("dbo.Case", "AssignedToInterpreterId");
            AddForeignKey("dbo.Case", "AssignedToInterpreterId", "dbo.Interpreter", "Id");
            //DropColumn("dbo.Case", "InterpreterId");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Case", "InterpreterId", c => c.Guid());
            DropForeignKey("dbo.Case", "AssignedToInterpreterId", "dbo.Interpreter");
            DropIndex("dbo.Case", new[] { "AssignedToInterpreterId" });
            DropColumn("dbo.Case", "AssignedToInterpreterId");
        }
    }
}
