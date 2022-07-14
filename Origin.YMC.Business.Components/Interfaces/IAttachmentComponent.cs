
using Origin.YMC.Business.Entities.Domain;
using System;
using System.Collections.Generic;

namespace Origin.YMC.Business.Components.Interfaces
{
    public interface IAttachmentComponent
    {
        void Add(Attachment attachment);
        void Add(Attachment attachment, out Guid id);
        List<Attachment> GetFilesByOwnerId(Guid ownerId);
        Attachment GetFilesByOwnerId(Guid ownerId,int attachmentTypeId);

        void Remove(Guid id);
        List<Attachment> GetFilesById(Guid id);
    }
}
