using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proxa.Services;
using Proxa.Models;

namespace Proxa.Endpoints.APIKeyManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateAPIKeyController : ControllerBase
    {
        private readonly APIKeyManagementService _apiKeyManagementService;

        public CreateAPIKeyController(APIKeyManagementService apiKeyManagementService)
        {
            _apiKeyManagementService = apiKeyManagementService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAPIKey([FromBody] Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest("Invalid user ID.");
            }

            try
            {
                APIKey apiKey = await _apiKeyManagementService.CreateAPIKey(userId);
                return Ok(apiKey);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
