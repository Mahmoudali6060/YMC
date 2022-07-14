using System;

namespace Origin.YMC.Business.Entities.Domain
{
    public class Attachment : EntityBase
    {
        public int AttachmentTypeId { get; set; }
        public string FileExtenstion { get; set; }
        public string OriginalFileName { get; set; }
        public long FileSize { get; set; }
        public string GeneratedFileName { get; set; }
        public Guid OwnerId { get; set; }
        public virtual AttachmentType AttachmentType { get; set; }
    }
}
