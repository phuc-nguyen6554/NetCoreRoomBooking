using Microsoft.EntityFrameworkCore;
using Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class DbSetExtension
    {
        public async static Task<PagedListResponse<T>> ToPagedList<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var totalEntry = query.Count();
            var item = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var pagedList = new PagedListResponse<T>(item, totalEntry, page, pageSize);

            return pagedList;
        }
    }
}
