// MIT License

using System.Collections.Generic;

namespace DnnReactDemo.Data.IRepositories
{
    /// <summary>
    /// Interface for a generic repository that defines common CRUD operations for entities.
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(object id);
        TEntity GetById(object id);
        IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);
        void Attach(TEntity entity);
        void Detach(TEntity entity);
    }
}
