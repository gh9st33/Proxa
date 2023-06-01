using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proxa.Models;
using Proxa.Services;
using Proxa.Helpers;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Graph;

namespace Proxa.Endpoints.AccountManagement
{
    [ApiController]
    [Route("api/accountmanagement/updateaccount")]
    public class UpdateAccountController : ControllerBase
    {
        private readonly AccountManagementService _accountManagementService;
        private readonly AuthHelper _authHelper;

        public UpdateAccountController(AccountManagementService accountManagementService, AuthHelper authHelper)
        {
            _accountManagementService = accountManagementService;
            _authHelper = authHelper;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid userId;
            try
            {
                userId = _authHelper.GetUserIdFromToken(Request.Headers["Authorization"]);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

            User updatedUser;
            try
            {
                updatedUser = await _accountManagementService.UpdateAccountAsync(userId, request.Email, request.Password);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { message = "Account updated successfully", user = updatedUser });
        }
    }

    public class UpdateAccountRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
