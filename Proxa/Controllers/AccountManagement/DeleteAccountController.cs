using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Proxa.Services;
using Proxa.Models;

namespace Proxa.Controllers.AccountManagement
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteAccountController : ControllerBase
    {
        private readonly AccountManagementService _accountManagementService;

        public DeleteAccountController(AccountManagementService accountManagementService)
        {
            _accountManagementService = accountManagementService;
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAccount(Guid userId)
        {
            bool isDeleted = await _accountManagementService.DeleteAccountAsync(userId);

            if (!isDeleted)
            {
                return NotFound("User not found.");
            }

            return Ok("User successfully deleted.");
        }
    }
}
