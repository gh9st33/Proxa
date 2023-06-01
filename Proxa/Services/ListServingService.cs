using System;
using System.Collections.Generic;
using System.Linq;
using Proxa.Models;
using Proxa.Data;
using Proxa.Helpers;

namespace Proxa.Services
{
    public class ListServingService
    {
        private readonly ApplicationDbContext _context;

        public ListServingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List GetList(Guid listId)
        {
            return _context.Lists.FirstOrDefault(l => l.Id == listId);
        }

        public IEnumerable<List> GetListsByUser(Guid userId, int pageNumber, int pageSize)
        {
            return _context.Lists
                .Where(l => l.UserId == userId)
                .OrderBy(l => l.CreatedAt)
                .Paginate(pageNumber, pageSize);
        }

        public List CreateList(string name, Guid userId)
        {
            var newList = new List
            {
                Id = Guid.NewGuid(),
                Name = name,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Lists.Add(newList);
            _context.SaveChanges();

            return newList;
        }

        public List UpdateList(Guid listId, string name)
        {
            var list = _context.Lists.FirstOrDefault(l => l.Id == listId);

            if (list == null)
            {
                return null;
            }

            list.Name = name;
            list.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return list;
        }

        public bool DeleteList(Guid listId)
        {
            var list = _context.Lists.FirstOrDefault(l => l.Id == listId);

            if (list == null)
            {
                return false;
            }

            _context.Lists.Remove(list);
            _context.SaveChanges();

            return true;
        }
    }
}
