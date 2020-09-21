using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Data
{
    public class PagedListRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
