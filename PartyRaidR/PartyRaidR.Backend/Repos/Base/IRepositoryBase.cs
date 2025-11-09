using PartyRaidR.Shared.Models;

namespace PartyRaidR.Backend.Repos.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class, IDbEntity<TEntity>, new()
    {
        Task<TEntity> GetByIdAsync(string id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<int> SaveChangesAsync();
    }
}
