using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Shared.Assemblers;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Backend.Services
{
    public class BaseService<TModel, TDto>
        where TModel : class, IDbEntity<TModel>, new()
        where TDto: class, new()
    {
        protected Assembler<TModel, TDto> _assembler;
        protected IRepositoryBase<TModel> _repo;

        public BaseService(Assembler<TModel, TDto>? assembler, IRepositoryBase<TModel>? repo)
        {
            _assembler = assembler ?? throw new ArgumentNullException($"{nameof(assembler)} was null.");
            _repo = repo ?? throw new ArgumentNullException($"{nameof(repo)} was null.");
        }

        public virtual async Task<TDto> GetByIdAsync(string id)
        {
            TModel model = await _repo.GetByIdAsync(id);
            return _assembler.ConvertToDto(model);
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            IEnumerable<TModel> models = await _repo.GetAllAsync();
            return models.Select(_assembler.ConvertToDto);
        }

        public virtual async Task<TDto> AddAsync(TDto dto)
        {
            TModel entity = _assembler.ConvertToModel(dto);

            if (entity.HasId)
                throw new Exception($"There is already a(n) {nameof(TModel)} with the given ID.");

            // Generate a new ID for new entities
            entity.Id = Guid.CreateVersion7().ToString();

            await _repo.InsertAsync(entity);

            await _repo.SaveChangesAsync();

            return _assembler.ConvertToDto(entity);
        }

        public virtual async Task<TDto> UpdateAsync(TDto dto)
        {
            TModel model = _assembler.ConvertToModel(dto);

            TModel entity = await _repo.GetByIdAsync(model.Id);

            if (entity == default)
                throw new KeyNotFoundException($"Could not found a(n) {nameof(TModel)} with the given ID.");

            _repo.UpdateAsync(entity);

            await _repo.SaveChangesAsync();

            return _assembler.ConvertToDto(entity);
        }

        public virtual async Task<TDto> DeleteAsync(TDto dto)
        {
            TModel model = _assembler.ConvertToModel(dto);

            TModel entity = await _repo.GetByIdAsync(model.Id);

            if (entity == default)
                throw new KeyNotFoundException($"Could not found a(n) {nameof(TModel)} with the given ID.");

            _repo.DeleteAsync(entity);

            await _repo.SaveChangesAsync();

            return _assembler.ConvertToDto(entity);
        }
    }
}
