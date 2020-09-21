using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity
{
    public static class DbsetExtension
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> query, int page, int take) where T : class
        {
            return query.Skip((page - 1) * take).Take(take);
        }

        public async static Task<PagedList<T>> ToPagedList<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var totalEntry = query.Count();
            var item = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var pagedList = new PagedList<T>(item, totalEntry, page, pageSize);

            return pagedList;
        }
    }
}
