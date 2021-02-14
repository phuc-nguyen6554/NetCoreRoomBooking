using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Data
{
    public class PagedListResponse<T> where T : class
    {
        public int TotalCount { get; set; }
        public int Count { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public List<T> Result { get; set; }

        public PagedListResponse() { }

        public PagedListResponse(List<T> items, int total, int page, int pageSize)
        {
            Result = items;
            TotalCount = total;
            Count = items.Count;
            CurrentPage = page;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
        }

        public PagedListResponse<T> Map<T>(List<T> result) where T : class
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
