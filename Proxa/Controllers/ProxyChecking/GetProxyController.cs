using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proxa.Services;
using Proxa.Models;

namespace Proxa.Controllers.ProxyChecking
{
    [ApiController]
    [Route("api/proxychecking/[controller]")]
    public class GetProxyController : ControllerBase
    {
        private readonly ProxyCheckingService _proxyCheckingService;

        public GetProxyController(ProxyCheckingService proxyCheckingService)
        {
            _proxyCheckingService = proxyCheckingService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProxy(Guid id)
        {
            Proxy proxy = await _proxyCheckingService.GetProxyAsync(id);

            if (proxy == null)
            {
                return NotFound();
            }

            return Ok(proxy);
        }
    }
}
