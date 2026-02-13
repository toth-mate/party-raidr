using PartyRaidR.Shared.Dtos;
using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Backend.Models;

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
