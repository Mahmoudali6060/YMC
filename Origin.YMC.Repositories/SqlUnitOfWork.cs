using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using Origin.YMC.Business.Entities;
using Origin.YMC.Business.Entities.Domain.Doctors;
using Origin.YMC.CrossCutting.Framework;

namespace Origin.YMC.Repositories
{
    public class SqlUnitOfWork : IQueryableUnitOfWork, ISqlCommand
    {
        #region Members

        private readonly DbContext _context;
        #endregion

        #region Constructor

        public SqlUnitOfWork()
        {            
            _context = new ApplicationDbContext();
            _context.Configuration.ValidateOnSaveEnabled = false;
            _context.Configuration.LazyLoadingEnabled = true;
        }

        #endregion

        #region IUnitOfWork Members

        public int SaveChanges()
        {
            SetIEntityFields();
            return _context.SaveChanges();
        }
        public int SaveChanges(bool DontGenerateId)
        {
            return _context.SaveChanges();
        }
        public void RollbackChanges()
        {
            // set all entities in change tracker  as 'unchanged state'
            _context.ChangeTracker.Entries()
                  .ToList()
                  .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        public DbSet<TEntity> CreateSet<TEntity>()
            where TEntity : class
        {
            var set = _context.Set<TEntity>();
            return set;
        }

        public void Attach<TEntity>(TEntity item)
            where TEntity : class
        {
            //attach and set as unchanged
            _context.Entry(item).State = EntityState.Unchanged;
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            _context.Entry(item).State = EntityState.Detached;
            _context.Entry(item).State = EntityState.Modified;

            var excluded = new[] { "CreationDate", "CreatedBy" };
            _context.Entry(item).CurrentValues.PropertyNames
                .Where(name => !excluded.Contains(name))
                .ToList()
                .ForEach(p => _context.Entry(item).Property(p).IsModified = true);

            //Set all properties as modified execpt "CreatedDate" and "CreatedBy"

        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {

            //_context.Entry(original).State = EntityState.Modified;
            //if it is not attached, attach original and set current values
            _context.Entry(original).CurrentValues.SetValues(current);

            //if (original is CampaignCity)
            //{
            //    CampaignCitySuppliers.CampaignCitySupplierFile
            //    // Delete subFoos from database that are not in the newFoo.SubFoo collection
            //    foreach (var dbSubFoo in original)
            //        if (!newFoo.SubFoo.Any(s => s.Id == dbSubFoo.Id))
            //            context.SubFoos.Remove(dbSubFoo);

            //    foreach (var newSubFoo in newFoo.SubFoo)
            //    {
            //        var dbSubFoo = dbFoo.SubFoo.SingleOrDefault(s => s.Id == newSubFoo.Id);
            //        if (dbSubFoo != null)
            //            // Update subFoos that are in the newFoo.SubFoo collection
            //            context.Entry(dbSubFoo).CurrentValues.SetValues(newSubFoo);
            //        else
            //            // Insert subFoos into the database that are not
            //            // in the dbFoo.subFoo collection
            //            dbFoo.SubFoo.Add(newSubFoo);
            //    }

            //    // and the same for AnotherSubFoo...
            //}

        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion

        #region Private Methods

        private void SetIEntityFields()
        {
            var now = DateTime.Now;

            var lst = _context.ChangeTracker.Entries<IEntity>()
                .Where(e => e.State == EntityState.Added)
                .ToList();
            lst
                .ForEach(e =>
                {
                    e.Entity.Id = Guid.NewGuid();
                    e.Entity.CreationDate = now;
                    e.Entity.CreatedBy = IdentityProviderFactory.CreateIdentity().GetUserId();
                    e.Entity.LastUpdatedAt = now;
                   
                   // e.Entity.IsActive = e.Ent true;

                });

            _context.ChangeTracker.Entries<IEntity>()
                .Where(e => e.State == EntityState.Modified)
                .ToList()
                .ForEach(e =>
                {
                    e.Entity.LastUpdatedAt = now;
                    e.Entity.LastUpdatedBy = IdentityProviderFactory.CreateIdentity().GetUserId();
                });
        }

        #endregion

        #region Implementation of ISqlCommand

        public int ExecuteCommand(string command, params object[] args)
        {
            return _context.Database.ExecuteSqlCommand(command, args);
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return _context.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        #endregion

        #region Bulk Operations
        public void InsertBulk<TEntity>(IEnumerable<TEntity> items) where TEntity : class
        {
            var now = DateTime.Now;
            var lst = items.ToList();
            lst
            .ForEach(item =>
            {
                ((IEntity)item).IsActive = true;
                ((IEntity)item).CreationDate = now;
                ((IEntity)item).CreatedBy = IdentityProviderFactory.CreateIdentity().GetUserId();
                ((IEntity)item).LastUpdatedAt = now;
            });

            CreateSet<TEntity>().AddRange(lst);

            //Z.EntityFramework.Extnstions trial version expired on 04-09-2016
            //_context.BulkInsert(lst);
        }

        public void UpdateBulk<TEntity>(IEnumerable<TEntity> items) where TEntity : class
        {
            var now = DateTime.Now;
            var lst = items.ToList();
            lst
            .ForEach(item =>
            {
                ((IEntity)item).IsActive = true;
                ((IEntity)item).CreationDate = now;
                ((IEntity)item).LastUpdatedBy = IdentityProviderFactory.CreateIdentity().GetUserId();
                ((IEntity)item).LastUpdatedAt = now;
            });

            CreateSet<TEntity>().AddRange(lst);

            //Z.EntityFramework.Extnstions trial version expired on 04-09-2016
            //_context.BulkUpdate(lst);
        }
        #endregion
    }
}
