using PartyRaidR.Backend.Assemblers;
using PartyRaidR.Backend.Exceptions;
using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Services.Base
{
    public class BaseService<TModel, TDto> : IBaseService<TModel, TDto>
        where TModel : class, IDbEntity<TModel>, new()
        where TDto: class, IHasId, new()
    {
        protected readonly Assembler<TModel, TDto> _assembler;
        protected readonly IRepositoryBase<TModel> _repo;
        protected readonly IUserContext _userContext;

        public BaseService(Assembler<TModel, TDto>? assembler, IRepositoryBase<TModel>? repo, IUserContext? userContext)
        {
            _assembler = assembler ?? throw new ArgumentNullException($"{nameof(assembler)} was null.");
            _repo = repo ?? throw new ArgumentNullException($"{nameof(repo)} was null.");
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        public virtual async Task<ServiceResponse<TDto>> GetByIdAsync(string id)
        {
            try
            {
                TModel? model = await _repo.GetByIdAsync(id) ?? throw new EntityNotFoundException($"Could not found a(n) {nameof(TModel)} with the given ID.");

                if (model is null)
                    return CreateResponse<TDto>(false, 404, message: $"Entity with the given ID was not found.");

                return CreateResponse(true, 200, _assembler.ConvertToDto(model));
            }
            catch(Exception)
            {
                return CreateResponse<TDto>(false, 500, message: "An error occured while retrieving entity data.");
            }
        }

        public virtual async Task<ServiceResponse<IEnumerable<TDto>>> GetAllAsync()
        {
            try
            {
                IEnumerable<TModel> models = await _repo.GetAllAsync();
                return CreateResponse(true, 200, models.Select(_assembler.ConvertToDto));
            }
            catch(Exception)
            {
                return CreateResponse<IEnumerable<TDto>>(false, 500, message: "Could not retrieve entities.");
            }
        }

        public virtual async Task<ServiceResponse<TDto>> AddAsync(TDto dto)
        {
            TModel entity = _assembler.ConvertToModel(dto);

            try
            {
                // Generate a new ID for new entities
                entity.Id = Guid.CreateVersion7().ToString();

                await _repo.InsertAsync(entity);
                await _repo.SaveChangesAsync();

                return CreateResponse(true, 201, _assembler.ConvertToDto(entity));
            }
            catch(Exception)
            {
                return CreateResponse<TDto>(false, 500, message: "Failed to add new entity.");
            }
        }

        public virtual async Task<ServiceResponse<TDto>> UpdateAsync(TDto dto)
        {
            try
            {
                TModel? entity = await _repo.GetByIdAsync(dto.Id);

                if(entity is null)
                    return CreateResponse<TDto>(false, 404, message: "Could not found entity with the given ID.");

                entity = _assembler.ConvertToModel(dto);

                _repo.Update(entity);

                await _repo.SaveChangesAsync();

                return CreateResponse<TDto>(true, 200, message: "Update successful.");
            }
            catch (Exception e)
            {
                return CreateResponse<TDto>(false, 500, message: $"Failed to update entity: {e.Message}");
            }
        }

        public virtual async Task<ServiceResponse<TDto>> DeleteAsync(string id)
        {
            try
            {
                TModel? entity = await _repo.GetByIdAsync(id);

                if (entity is null)
                    return CreateResponse<TDto>(false, 404, message: "Could not found an entity with the given ID.");
                else
                    _repo.Delete(entity);

                await _repo.SaveChangesAsync();

                return CreateResponse<TDto>(true, 200);
            }
            catch (Exception)
            {
                return CreateResponse<TDto>(false, 500, message: "Failed to delete entity.");
            }
        }

        protected ServiceResponse<T> CreateResponse<T>(bool isSuccess, int statusCode, T? data = default, string? message = null)
        {
            return new ServiceResponse<T>
            {
                Success = isSuccess,
                StatusCode = statusCode,
                Data = data,
                Message = message ?? string.Empty
            };
        }
    }
}
