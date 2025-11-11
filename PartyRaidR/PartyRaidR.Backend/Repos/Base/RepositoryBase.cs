using PartyRaidR.Shared.Models;
using Microsoft.EntityFrameworkCore;
using PartyRaidR.Backend.Context;

namespace PartyRaidR.Backend.Repos.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class, IDbEntity<TEntity>, new()
    {
        private readonly AppDbContext _context;
        protected readonly DbSet<TEntity>? _dbSet;

        public RepositoryBase(AppDbContext? context)
        {
            ArgumentNullException.ThrowIfNull(context, $"Table '{nameof(context)}' cannot be accessed.");
            _context = context;
            _dbSet = _context.Set<TEntity>() ?? throw new ArgumentException($"Table '{nameof(TEntity)}' cannot be accessed.");
        }

        // CRUD Operations
        // An empty entity is returned if no entity is found with the given ID.
        public async Task<TEntity> GetByIdAsync(string id) =>
            await _dbSet!.FindAsync(id) ?? new TEntity();

        public async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await _dbSet!.ToListAsync();

        public async Task InsertAsync(TEntity entity) =>
            await _dbSet!.AddAsync(entity);

        public void Update(TEntity entity) =>
            _dbSet!.Update(entity);

        public void Delete(TEntity entity) =>
            _dbSet!.Remove(entity);

        public async Task<int> SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
