using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proxa.Models;
using Proxa.Services;

namespace Proxa.Endpoints.APIKeyManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAPIKeyController : ControllerBase
    {
        private readonly APIKeyManagementService _apiKeyManagementService;

        public GetAPIKeyController(APIKeyManagementService apiKeyManagementService)
        {
            _apiKeyManagementService = apiKeyManagementService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAPIKey(Guid id)
        {
            APIKey apiKey = await _apiKeyManagementService.GetAPIKeyByIdAsync(id);

            if (apiKey == null)
            {
                return NotFound("API Key not found.");
            }

            return Ok(apiKey);
        }
    }
}
