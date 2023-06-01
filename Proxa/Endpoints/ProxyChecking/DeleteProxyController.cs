using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proxa.Services;

namespace Proxa.Endpoints.ProxyChecking
{
    [ApiController]
    [Route("api/proxychecking/[controller]")]
    public class DeleteProxyController : ControllerBase
    {
        private readonly ProxyCheckingService _proxyCheckingService;

        public DeleteProxyController(ProxyCheckingService proxyCheckingService)
        {
            _proxyCheckingService = proxyCheckingService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProxy(Guid id)
        {
            bool isDeleted = await _proxyCheckingService.DeleteProxyAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
