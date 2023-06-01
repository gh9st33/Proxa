using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proxa.Models;
using Proxa.Services;

namespace Proxa.Endpoints.APIKeyManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteAPIKeyController : ControllerBase
    {
        private readonly APIKeyManagementService _apiKeyManagementService;

        public DeleteAPIKeyController(APIKeyManagementService apiKeyManagementService)
        {
            _apiKeyManagementService = apiKeyManagementService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAPIKey(Guid id)
        {
            var result = await _apiKeyManagementService.DeleteAPIKeyAsync(id);

            if (result)
            {
                return Ok(new { message = "API Key deleted successfully." });
            }

            return NotFound(new { message = "API Key not found." });
        }
    }
}
