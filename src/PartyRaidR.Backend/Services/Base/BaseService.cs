using PartyRaidR.Backend.Exceptions;
using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Shared.Assemblers;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;
using PartyRaidR.Shared.Models.Responses;

namespace PartyRaidR.Backend.Services.Base
{
    public class BaseService<TModel, TDto> : IBaseService<TModel, TDto>
        where TModel : class, IDbEntity<TModel>, new()
        where TDto: class, IHasId, new()
    {
        protected Assembler<TModel, TDto> _assembler;
        protected IRepositoryBase<TModel> _repo;

        public BaseService(Assembler<TModel, TDto>? assembler, IRepositoryBase<TModel>? repo)
        {
            _assembler = assembler ?? throw new ArgumentNullException($"{nameof(assembler)} was null.");
            _repo = repo ?? throw new ArgumentNullException($"{nameof(repo)} was null.");
        }

        public virtual async Task<ServiceResponse<TDto>> GetByIdAsync(string id)
        {
            try
            {
                TModel? model = await _repo.GetByIdAsync(id) ?? throw new EntityNotFoundException($"Could not found a(n) {nameof(TModel)} with the given ID.");
                return new ServiceResponse<TDto>
                {
                    Success = true,
                    Data = _assembler.ConvertToDto(model),
                    StatusCode = 200
                };
            }
            catch(Exception)
            {
                return new ServiceResponse<TDto>
                {
                    Success = false,
                    Message = $"{nameof(TModel)} with the given ID was not found.",
                    StatusCode = 404
                };
            }
        }

        public virtual async Task<ServiceResponse<IEnumerable<TDto>>> GetAllAsync()
        {
            try
            {
                IEnumerable<TModel> models = await _repo.GetAllAsync();
                return new ServiceResponse<IEnumerable<TDto>>
                {
                    Success = true,
                    Data = models.Select(_assembler.ConvertToDto),
                    StatusCode = 200
                };
            }
            catch(Exception)
            {
                return new ServiceResponse<IEnumerable<TDto>>
                {
                    Success = false,
                    Message = $"Could not retrieve {nameof(TModel)} entities.",
                    StatusCode = 500
                };
            }
        }

        public virtual async Task<ServiceResponse<TDto>> AddAsync(TDto dto)
        {
            TModel entity = _assembler.ConvertToModel(dto);

            try
            {
                if (entity.HasId)
                    throw new EntityAlreadyExistsException($"There is already a(n) {nameof(TModel)} with the given ID.");

                // Generate a new ID for new entities
                entity.Id = Guid.CreateVersion7().ToString();

                await _repo.InsertAsync(entity);

                await _repo.SaveChangesAsync();

                return new ServiceResponse<TDto>
                {
                    Success = true,
                    Data = _assembler.ConvertToDto(entity),
                    StatusCode = 201
                };
            }
            catch(EntityAlreadyExistsException e)
            {
                return new ServiceResponse<TDto>
                {
                    Success = false,
                    Message = e.Message,
                    StatusCode = 409
                };
            }
            catch(Exception)
            {
                return new ServiceResponse<TDto>
                {
                    Success = false,
                    Message = $"Could not add the new {nameof(TModel)} entity.",
                    StatusCode = 500
                };
            }
        }

        public virtual async Task<ServiceResponse<TDto>> UpdateAsync(TDto dto)
        {
            try
            {
                TModel entity = _assembler.ConvertToModel(dto);

                _repo.UpdateAsync(entity);

                await _repo.SaveChangesAsync();

                return new ServiceResponse<TDto>
                {
                    Success = true,
                    StatusCode = 204,
                    Message = $"{nameof(TModel)} entity updated successfully.",
                    Data = null
                };
            }
            catch (EntityNotFoundException e)
            {
                return new ServiceResponse<TDto>
                {
                    Success = false,
                    Message = e.Message,
                    StatusCode = 404
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<TDto>
                {
                    Success = false,
                    Message = $"Could not update the {nameof(TModel)} entity.\n{e.Message}",
                    StatusCode = 500
                };
            }
        }

        public virtual async Task<ServiceResponse<TDto>> DeleteAsync(string id)
        {
            try
            {
                TModel? entity = await _repo.GetByIdAsync(id);

                if (entity is null)
                    throw new EntityNotFoundException($"Could not found a(n) {nameof(TModel)} with the given ID.");
                else
                    _repo.DeleteAsync(entity);

                await _repo.SaveChangesAsync();

                return new ServiceResponse<TDto>
                {
                    Success = true,
                    StatusCode = 204
                };
            }
            catch(EntityNotFoundException e)
            {
                return new ServiceResponse<TDto>
                {
                    Success = false,
                    Message = e.Message,
                    StatusCode = 404
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<TDto>
                {
                    Success = false,
                    Message = $"Could not delete the {nameof(TModel)} entity.",
                    StatusCode = 500
                };
            }
        }
    }
}
