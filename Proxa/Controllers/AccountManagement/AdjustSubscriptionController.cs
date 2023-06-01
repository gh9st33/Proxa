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
    [Route("api/accountmanagement/adjustsubscription")]
    public class AdjustSubscriptionController : ControllerBase
    {
        private readonly AccountManagementService _accountManagementService;

        public AdjustSubscriptionController(AccountManagementService accountManagementService)
        {
            _accountManagementService = accountManagementService;
        }

        [HttpPost]
        public async Task<IActionResult> AdjustSubscription([FromBody] AdjustSubscriptionRequest request)
        {
            var userId = Guid.Parse(User.Identity.Name);
            var result = await _accountManagementService.AdjustSubscriptionAsync(userId, request.SubscriptionType);

            if (result)
            {
                return Ok(new { message = "Subscription updated successfully." });
            }
            else
            {
                return BadRequest(new { message = "Failed to update subscription." });
            }
        }
    }

    public class AdjustSubscriptionRequest
    {
        public string SubscriptionType { get; set; }
    }
}
