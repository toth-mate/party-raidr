using PartyRaidR.Backend.Models;
using System.Linq.Expressions;

namespace PartyRaidR.Backend.Repos.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class, IDbEntity<TEntity>, new()
    {
        Task<TEntity?> GetByIdAsync(string id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> condition);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChangesAsync();
        IQueryable<TEntity> GetAllAsQueryable();
    }
}
