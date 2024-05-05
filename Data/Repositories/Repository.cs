// MIT License

using DnnReactModule.Utilities;
using System.Collections.Generic;
using System.Linq;
using DnnReactModule.Data.IRepositories;
using System.Data.Entity;

namespace DnnReactModule.Data.Repositories
{
    /// <summary>
    /// Generic repository class that implements IRepository interface for CRUD operations on entities of type TEntity.
    /// </summary>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private ModuleDbContext _context;
        private DbSet<TEntity> _dbSet;
        public Repository(ModuleDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        #region Sync Methods

        public void Add(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            _dbSet.Add(entity);
        }

        public void AddRange(List<TEntity> entities)
        {
            Assert.NotNull(entities, nameof(entities));
            _dbSet.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            _dbSet.Remove(entity);
        }

        public void Delete(object id)
        {
            var entity = GetById(id);
            Delete(entity);
        }

        public TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return _dbSet.SqlQuery(query, parameters).ToList();
        }

        //public virtual IQueryable<TEntity> Get()
        //{
        //    IQueryable<TEntity> query = _dbSet;
        //        return query;
        //}
        #endregion

        #region Attach & Detach

        public void Attach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            var entry = _context.Entry(entity);
            if (entry != null)
                entry.State = EntityState.Detached;
        }

        public void Detach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
        }
        #endregion

    }
}
