using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proxa.Services;
using Proxa.Models;

namespace Proxa.Endpoints.APIKeyManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignAPIKeyController : ControllerBase
    {
        private readonly APIKeyManagementService _apiKeyManagementService;

        public AssignAPIKeyController(APIKeyManagementService apiKeyManagementService)
        {
            _apiKeyManagementService = apiKeyManagementService;
        }

        [HttpPost]
        public async Task<IActionResult> AssignAPIKey(Guid apiKeyId, Guid userId)
        {
            try
            {
                var apiKey = _apiKeyManagementService.GetAPIKey(apiKeyId);
                if (apiKey == null)
                {
                    return NotFound("API Key not found.");
                }

                apiKey.UserId = userId;
                await _apiKeyManagementService.UpdateAPIKey(apiKey);

                return Ok("API Key assigned successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
