using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Proxa.Models;
using Proxa.Services;

namespace Proxa.Endpoints.AccountManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateAccountController : ControllerBase
    {
        private readonly AccountManagementService _accountManagementService;

        public CreateAccountController(AccountManagementService accountManagementService)
        {
            _accountManagementService = accountManagementService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountAsync([FromBody] CreateUserRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Email and password are required.");
            }

            User newUser = await _accountManagementService.CreateAccountAsync(request.Email, request.Password);

            if (newUser == null)
            {
                return StatusCode(500, "An error occurred while creating the account.");
            }

            return CreatedAtAction(nameof(CreateAccountAsync), new { id = newUser.Id }, newUser);
        }
    }

    public class CreateUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
