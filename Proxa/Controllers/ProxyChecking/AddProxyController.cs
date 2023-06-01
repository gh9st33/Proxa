using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proxa.Models;
using Proxa.Services;

namespace Proxa.Controllers.ProxyChecking
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddProxyController : ControllerBase
    {
        private readonly ProxyCheckingService _proxyCheckingService;

        public AddProxyController(ProxyCheckingService proxyCheckingService)
        {
            _proxyCheckingService = proxyCheckingService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProxy([FromBody] Proxy proxy)
        {
            if (proxy == null)
            {
                return BadRequest("Proxy cannot be null.");
            }

            try
            {
                var addedProxy = await _proxyCheckingService.AddProxyAsync(proxy);
                return CreatedAtAction(nameof(AddProxy), new { id = addedProxy.Id }, addedProxy);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
