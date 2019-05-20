using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Domain
{
    public class PaginatedList<T>
    {
        public List<T> List { get; }
        public ulong TotalCount { get; }
        public ulong PageSize { get; }
        public uint Page { get; }
        public uint NoOfPages { get; }

        [JsonConstructor]
        public PaginatedList(List<T> list, ulong totalCount, ulong pageSize, uint page)
        {
            List = list;
            TotalCount = totalCount;
            PageSize = pageSize;
            Page = page;
            NoOfPages = (uint)Math.Ceiling((float)TotalCount / PageSize);
        }
        
        public PaginatedList(List<T> list, long totalCount, FilteredPageRequest page)
        {
            List = list;
            TotalCount = (ulong) totalCount;
            PageSize = page.PageSize;
            Page = page.Page;
            NoOfPages = (uint)Math.Ceiling((float)TotalCount / PageSize);
        }
    }
}