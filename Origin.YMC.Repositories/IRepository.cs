using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IUnitOfWork UnitOfWork { get; }

        TEntity Get(params object[] keyValues);
        IQueryable<TEntity> Query();

        void Remove(TEntity item);
        void Delete(TEntity item);

        int GetCount();

        void Add(TEntity entity);
        TEntity AddThenGet(TEntity entity);
        void Update(TEntity entity, object[] primaryKeyValues);
        void Update(TEntity entity);
        void Deactivate(TEntity entity);
        void Activate(TEntity entity);
        IQueryable<TEntity> GetAll();

    }
}
