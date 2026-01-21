using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TDto dto)
        {
            var result = await _service.AddAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TDto dto)
        {
            var result = await _service.UpdateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
