using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Proxa.Models;
using Proxa.Services;

namespace Proxa.Controllers.ListServing
{
    [Authorize]
    [ApiController]
    [Route("api/listserving")]
    public class GetListController : ControllerBase
    {
        private readonly ListServingService _listServingService;

        public GetListController(ListServingService listServingService)
        {
            _listServingService = listServingService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetList(Guid id)
        {
            try
            {
                List list = await _listServingService.GetListAsync(id);

                if (list == null)
                {
                    return NotFound("List not found.");
                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
