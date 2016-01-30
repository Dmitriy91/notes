using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        #region Fields
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        #endregion

        protected RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            //_dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public virtual void Add(params TEntity[] entities)
        {
            foreach (TEntity entity in entities)
                _dbSet.Add(entity);
        }

        public virtual void Update(params TEntity[] entities)
        {
            foreach (TEntity entity in entities)
            {
                _dbSet.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual void Delete(params TEntity[] entities)
        {
            foreach (TEntity entity in entities)
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            _dbSet.RemoveRange(_dbSet.Where<TEntity>(where));
        }

        public virtual TEntity GetById(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable<TEntity>();
        }

        public virtual IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            if (where == null)
                return null;

            return _dbSet.Where(where);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            if (where == null)
                return null;

            return _dbSet.FirstOrDefault<TEntity>(where);
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> where)
        {
            if (where == null)
                return false;

            return _dbSet.Any<TEntity>(where);
        }

        public virtual async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public virtual void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
