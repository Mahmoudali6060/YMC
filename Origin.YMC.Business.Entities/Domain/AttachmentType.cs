using System.ComponentModel.DataAnnotations.Schema;


namespace Origin.YMC.Business.Entities.Domain
{
    public class AttachmentType
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
