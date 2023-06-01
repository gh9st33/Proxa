using System;
using System.Collections.Generic;
using System.Linq;

namespace Proxa.Helpers
{
    public static class PaginationHelper
    {
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than or equal to 1.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than or equal to 1.");
            }

            return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
