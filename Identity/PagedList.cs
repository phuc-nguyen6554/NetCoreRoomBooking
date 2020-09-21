using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity
{
    public class PagedListResponse<T> where T : class
    {
        public int TotalCount { get; set; }
        public int Count { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public List<T> Result { get; set; }

        public PagedListResponse(){}

        public PagedListResponse(List<T> items, int total, int page, int pageSize)
        {
            Result = items;
            TotalCount = total;
            CurrentPage = page;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(TotalCount /(double) pageSize);
        }

        public PagedListResponse(PagedListResponse<T> list, List<T> result)
        {
            Result = result;
            TotalCount = list.TotalCount;
            CurrentPage = list.CurrentPage;
            PageSize = list.PageSize;
            TotalPages = (int)Math.Ceiling(list.TotalCount / (double)list.PageSize);
        }

        public PagedListResponse<T> Map<T>(List<T> result) where T:class
        {
            return new PagedListResponse<T>
            {
                Result = result,
                TotalCount = TotalCount,
                CurrentPage = CurrentPage,
                PageSize = PageSize,
                TotalPages = TotalPages
            };
        }
    }
}
