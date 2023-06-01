using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Proxa.Services;
using Proxa.Models;


namespace Proxa.Endpoints.AccountManagement
    {
        [Authorize]
        [ApiController]
        [Route("api/accountmanagement/adjustcredits")]
        public class AdjustCreditsController : ControllerBase
        {
            private readonly AccountManagementService _accountManagementService;

            public AdjustCreditsController(AccountManagementService accountManagementService)
            {
                _accountManagementService = accountManagementService;
            }

            [HttpPost]
            public async Task<IActionResult> AdjustCredits([FromBody] AdjustCreditsRequest request)
            {
                var userId = Guid.Parse(User.Identity.Name);
                var result = await _accountManagementService.AdjustCreditsAsync(userId, request.Credits);

                if (result)
                {
                    return Ok(new { message = "Credits adjusted successfully." });
                }
                else
                {
                    return BadRequest(new { message = "Failed to adjust credits." });
                }
            }
        }

        public class AdjustCreditsRequest
        {
            public int Credits { get; set; }
        }
    }

