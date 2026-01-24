using PartyRaidR.Shared.Models;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models.Responses;

namespace PartyRaidR.Backend.Services.Base
{
    public interface IBaseService<TModel, TDto>
        where TModel : class, IDbEntity<TModel>, new()
        where TDto: class, IHasId
    {
        Task<ServiceResponse<TDto>> GetByIdAsync(string id);
        Task<ServiceResponse<IEnumerable<TDto>>> GetAllAsync();
        Task<ServiceResponse<TDto>> AddAsync(TDto dto);
        Task<ServiceResponse<TDto>> UpdateAsync(TDto dto);
        Task<ServiceResponse<TDto>> DeleteAsync(string id);
    }
}
