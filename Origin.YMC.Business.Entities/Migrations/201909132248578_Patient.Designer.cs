// <auto-generated />
namespace Origin.YMC.Business.Entities.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.2.0-61023")]
    public sealed partial class Patient : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(Patient));
        
        string IMigrationMetadata.Id
        {
            get { return "201909132248578_Patient"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}