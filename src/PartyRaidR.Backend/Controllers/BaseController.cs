using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;
using PartyRaidR.Shared.Models.Responses;

namespace PartyRaidR.Backend.Controllers
{
    public abstract class BaseController<TModel, TDto> : ControllerBase
        where TModel : class, IDbEntity<TModel>, new()
        where TDto : class, IHasId
    {
        protected readonly IBaseService<TModel, TDto> _service;

        protected BaseController(IBaseService<TModel, TDto> service)
        {
            _service = service;
        }

        protected IActionResult HandleResponse<T> (ServiceResponse<T> response)
        {
            if (response.Success)
                return response.Data is null ? NoContent() : StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            HandleResponse(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) =>
            HandleResponse(await _service.GetByIdAsync(id));

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TDto dto) =>
            HandleResponse(await _service.AddAsync(dto));

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TDto dto) =>
            HandleResponse(await _service.UpdateAsync(dto));

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) =>
            HandleResponse(await _service.DeleteAsync(id));
    }
}
