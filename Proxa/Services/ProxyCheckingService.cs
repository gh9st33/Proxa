using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proxa.Data;
using Proxa.Models;

namespace Proxa.Services
{
    public class ProxyCheckingService
    {
        private readonly ApplicationDbContext _context;

        public ProxyCheckingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Proxy> AddProxyAsync(Proxy proxy)
        {
            if (proxy == null)
            {
                throw new ArgumentNullException(nameof(proxy));
            }

            await _context.Proxies.AddAsync(proxy);
            await _context.SaveChangesAsync();

            return proxy;
        }

        public async Task<Proxy> GetProxyAsync(Guid id)
        {
            return await _context.Proxies.FindAsync(id);
        }

        public async Task<IEnumerable<Proxy>> GetUserProxiesAsync(Guid userId)
        {
            return _context.Proxies.Where(p => p.UserId == userId).ToList();
        }

        public async Task<bool> DeleteProxyAsync(Guid id)
        {
            var proxy = await _context.Proxies.FindAsync(id);
            if (proxy == null)
            {
                return false;
            }

            _context.Proxies.Remove(proxy);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
