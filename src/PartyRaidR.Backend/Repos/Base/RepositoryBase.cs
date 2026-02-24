using Microsoft.EntityFrameworkCore;
using PartyRaidR.Backend.Context;
using System.Linq.Expressions;
using PartyRaidR.Backend.Models;

namespace PartyRaidR.Backend.Repos.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class, IDbEntity<TEntity>, new()
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity>? _dbSet;

        public RepositoryBase(AppDbContext? context)
        {
            ArgumentNullException.ThrowIfNull(context, $"Table '{nameof(context)}' cannot be accessed.");
            _context = context;
            _dbSet = _context.Set<TEntity>() ?? throw new ArgumentException($"Table '{nameof(TEntity)}' cannot be accessed.");
        }

        // CRUD Operations
        public async Task<TEntity?> GetByIdAsync(string id) =>
            await _dbSet!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await _dbSet!.AsNoTracking().ToListAsync();

        public async Task InsertAsync(TEntity entity) =>
            await _dbSet!.AddAsync(entity);

        public void Update(TEntity entity) =>
            _dbSet!.Update(entity);

        public void Delete(TEntity entity) =>
            _dbSet!.Remove(entity);

        public async Task<int> SaveChangesAsync() =>
            await _context.SaveChangesAsync();

        public async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> condition) =>
            await _dbSet!
            .Where(condition)
            .ToListAsync();

        public async void ClearTracker() =>
            _context.ChangeTracker.Clear();

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _dbSet!.AnyAsync(predicate);

        public IQueryable<TEntity> GetAllAsQueryable() =>
            _dbSet!.AsQueryable();

        public async Task<int> CountAsync() =>
            await _dbSet!.CountAsync();

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _dbSet!.CountAsync(predicate);
    }
}
