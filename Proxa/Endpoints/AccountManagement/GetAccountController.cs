using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Proxa.Services;
using Proxa.Models;
using Microsoft.Graph;

namespace Proxa.Endpoints.AccountManagement
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GetAccountController : ControllerBase
    {
        private readonly AccountManagementService _accountManagementService;

        public GetAccountController(AccountManagementService accountManagementService)
        {
            _accountManagementService = accountManagementService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAccount(Guid userId)
        {
            User user = await _accountManagementService.GetAccountAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }
    }
}
