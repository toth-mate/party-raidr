using PartyRaidR.Shared.Dtos;
using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Backend.Models;

namespace PartyRaidR.Backend.Services.Base
{
    public interface IBaseService<TModel, TDto>
        where TModel : class, IDbEntity<TModel>, new()
        where TDto: class, IHasId
    {
        /// <summary>
        /// Retrieves an entity by its unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>A service response object containing the entity DTO.</returns>
        Task<ServiceResponse<TDto>> GetByIdAsync(string id);
        
        /// <summary>
        /// Retieves all entities of the specified type.
        /// </summary>
        /// <returns>A service response object containing a collection of entity DTOs.</returns>
        Task<ServiceResponse<IEnumerable<TDto>>> GetAllAsync();

        /// <summary>
        /// Inserts a new entity into the database based on the provided DTO.
        /// The entity gets a unique identifier.
        /// </summary>
        /// <param name="dto">The provided object in the form it is inserted in.</param>
        /// <returns>A service response containing the inserted entity object as a DTO.</returns>
        Task<ServiceResponse<TDto>> AddAsync(TDto dto);

        /// <summary>
        /// Updates an existing entity in the database based on the provided DTO object.
        /// The object must contain the unique ID, as that is used to find the entity in the database.
        /// Every property of the entity will be updated to match the provided DTO, so all properties need to be provided.
        /// </summary>
        /// <param name="dto">The DTO object containing the updated data.</param>
        /// <returns>A service response containing the updated entity as a DTO object.</returns>
        Task<ServiceResponse<TDto>> UpdateAsync(TDto dto);

        /// <summary>
        /// Deletes an entity from the databse based on the provided ID.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be deleted.</param>
        /// <returns>A service response containing a DTO object representing the deleted entity.</returns>
        Task<ServiceResponse<TDto>> DeleteAsync(string id);
    }
}
