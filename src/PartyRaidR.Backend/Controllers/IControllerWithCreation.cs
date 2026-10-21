using Microsoft.AspNetCore.Mvc;

namespace PartyRaidR.Backend.Controllers;

public interface IControllerWithCreation<in TDto>
{
    Task<IActionResult> Create([FromBody] TDto dto);
}