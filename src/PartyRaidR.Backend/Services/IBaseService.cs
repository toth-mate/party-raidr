using PartyRaidR.Shared.Models;

namespace PartyRaidR.Backend.Services
{
    public interface IBaseService<TModel, TDto>
        where TModel : class, IDbEntity<TModel>, new()
        where TDto: class
    {
        Task<TDto> GetByIdAsync(string id);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> AddAsync(TDto dto);
        Task<TDto> UpdateAsync(TDto dto);
        Task<TDto> DeleteAsync(string id);
    }
}
