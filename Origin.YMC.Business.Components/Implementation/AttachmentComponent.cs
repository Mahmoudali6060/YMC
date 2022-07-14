using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain;
using Origin.YMC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Origin.YMC.Business.Components.Implementation
{
    public class AttachmentComponent : IAttachmentComponent
    {
        private readonly IRepository<Attachment> _attachmentRepository;
        private readonly IUserComponent _userComponent;
        private readonly ILogComponent _logComponent;
        public AttachmentComponent(
            IRepository<Attachment> attachmentRepository,
            IUserComponent userComponent,
            ILogComponent logComponent)
        {
            _userComponent = userComponent;
            _attachmentRepository = attachmentRepository;
            _logComponent = logComponent;
        }


        public void Add(Attachment attachment)
        {
            try
            {

                _attachmentRepository.Add(new Attachment
                {
                    Id = Guid.NewGuid(),
                    AttachmentTypeId = attachment.AttachmentTypeId,
                    CreatedBy = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    FileExtenstion = attachment.FileExtenstion,
                    FileSize = attachment.FileSize,
                    GeneratedFileName = attachment.GeneratedFileName,
                    IsActive = true,
                    OriginalFileName = attachment.OriginalFileName,
                    OwnerId = attachment.OwnerId,
                });
                _attachmentRepository.UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public void Add(Attachment attachment, out Guid id)
        {
            try
            {
                var att = new Attachment
                {
                    Id = Guid.NewGuid(),
                    AttachmentTypeId = attachment.AttachmentTypeId,
                    CreatedBy = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    FileExtenstion = attachment.FileExtenstion,
                    FileSize = attachment.FileSize,
                    GeneratedFileName = attachment.GeneratedFileName,
                    IsActive = true,
                    OriginalFileName = attachment.OriginalFileName,

                };
                _attachmentRepository.Add(att);
                _attachmentRepository.UnitOfWork.SaveChanges();
                id = att.Id;
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public List<Attachment> GetFilesByOwnerId(Guid ownerId)
        {
            try
            {

                return _attachmentRepository.Query()
                    .Where(c => c.OwnerId == ownerId)
                    .OrderBy(c => c.CreationDate)
                    .ToList();

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public Attachment GetFilesByOwnerId(Guid ownerId, int attachmentTypeId)
        {
            try
            {

                return _attachmentRepository.Query()
                    .Where(c => c.OwnerId == ownerId&&c.AttachmentTypeId==attachmentTypeId)
                    .OrderBy(c => c.CreationDate)
                    .FirstOrDefault();

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public List<Attachment> GetFilesById(Guid id)
        {
            try
            {

                return _attachmentRepository.Query()
                    .Where(c => c.Id == id)
                    .OrderBy(c => c.CreationDate)
                    .ToList();

            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }


        public void Remove(Guid id)
        {
            try
            {
                _attachmentRepository.Delete(_attachmentRepository.Get(id));
                _attachmentRepository.UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;

            }
        }
    }
}
