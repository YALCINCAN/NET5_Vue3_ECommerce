using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int page,int pagesize)
        {
            if (page <= 0)
                page = 1;
            if (pagesize <= 0)
                pagesize = 10;
            return query.Skip((page - 1) * pagesize).Take(pagesize);
        }
    }
}
