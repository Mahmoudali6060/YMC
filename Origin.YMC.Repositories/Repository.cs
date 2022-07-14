using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Origin.YMC.Business.Entities;

namespace Origin.YMC.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
           where TEntity : class, IEntity
    {
        #region Members

        protected readonly IQueryableUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        public virtual void Delete(TEntity item)
        {
            if (item != null)
            {
                //attach item if not exist
                _unitOfWork.Attach(item);

                //set as "removed"
                GetSet().Remove(item);
            }
            else
            {

            }
        }

        public virtual void Remove(TEntity item)
        {
            if (item != null)
            {
                //attach item if not exist
                _unitOfWork.Attach(item);

                //set as "removed"
                item.IsActive = true;
                _unitOfWork.SetModified(item);

            }
            else
            {
                //LoggerFactory.CreateLogger()
                //          .Info(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            }
        }

        public virtual void Merge(TEntity persisted, TEntity current)
        {
            GetSet().Attach(persisted);
            _unitOfWork.ApplyCurrentValues(persisted, current);
        }
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return ExecuteQuery<TEntity>(sqlQuery, parameters);
        }
        public virtual TEntity Get(params object[] keyValues)
        {
            if (keyValues != null)
                return GetSet().Find(keyValues);
            return null;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return GetSet();
        }


        #region Query Operations
        /// <summary>
        /// A query that returns records where "IsActive" = "True"
        /// </summary>
        /// <param name="keepTracking">If set to true Entity Framework will keep tracking. Enable this only when needed as it sometimes cause problems such as: unintentionally updating graphs. </param>
        /// <returns></returns>
        public IQueryable<TEntity> GetRecords(bool keepTracking = false)
        {
            var query = GetSet();//*.Where(e => e.IsActive)*/.AsNoTracking();

            //if (keepTracking)
            return query;
            //else
            //    return query.AsNoTracking();
        }

        public IQueryable<TEntity> GetActives()
        {
            return GetRecords().Where(e => e.IsActive);
        }

        public int GetCount()
        {
            return GetRecords().Count();
        }

        public List<TEntity> GetMultiple()
        {
            return GetRecords().ToList();
        }
        #endregion

        #region None-Query Operations
        public void Add(TEntity entity)
        {
            //if (entity is INeedPrimaryKey<Guid> && (entity as INeedPrimaryKey<Guid>).Id == Guid.Empty)
            //    (entity as INeedPrimaryKey<Guid>).Id = Guid.NewGuid();

            if (entity != null)
            {
                GetSet().Add(entity);
            }
            else
            {
                //LoggerFactory.CreateLogger()
                //    .Info(Messages.info_CannotAddNullEntity, typeof(TEntity).ToString());
            }
        }

        public TEntity AddThenGet(TEntity entity)
        {
            //if (entity is INeedPrimaryKey<Guid> && (entity as INeedPrimaryKey<Guid>).Id == Guid.Empty)
            //    (entity as INeedPrimaryKey<Guid>).Id = Guid.NewGuid();

            if (entity != null)
            {
               return GetSet().Add(entity);
            }
            else
            {
                //LoggerFactory.CreateLogger()
                //    .Info(Messages.info_CannotAddNullEntity, typeof(TEntity).ToString());
                return null;
            }
        }

        public void Update(TEntity entity)
        {
            if (entity != null)
            {
                //this operation also attach item in object state manager               
                _unitOfWork.SetModified(entity);
            }

        }

        public void Update(TEntity entity, object[] primaryKeyValues)
        {

            var dbObject = _unitOfWork.CreateSet<TEntity>().Find(primaryKeyValues);
            if (dbObject == null /*|| !dbObject.IsActive*/)
                throw new Exception("This record doesn't exist.");

            _unitOfWork.SetModified(entity);


        }

        public void Deactivate(TEntity entity)
        {
            entity.IsActive = false;
            _unitOfWork.SetModified(entity);
        }

        public void Activate(TEntity entity)
        {
            entity.IsActive = true;
            _unitOfWork.SetModified(entity);
        }

        #endregion

        #region Query Operations With Expressions

        public IQueryable<TEntity> Query()
        {
            return GetRecords();

        }

        #endregion

        #region Scalar Operations

        public int GetCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetRecords().Count(predicate);
        }
        #endregion

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_unitOfWork != null)
              _unitOfWork.Dispose();
        }

        #endregion

        #region Private Methods

        DbSet<TEntity> GetSet()
        {
            return _unitOfWork.CreateSet<TEntity>();
        }

        #endregion

    }
}
