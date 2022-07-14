using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Entities
{
    public class EntityBase : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }

    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime CreationDate { get; set; }
        Guid CreatedBy { get; set; }
        DateTime? LastUpdatedAt { get; set; }
        Guid? LastUpdatedBy { get; set; }
        bool IsActive { get; set; }
    }
}
