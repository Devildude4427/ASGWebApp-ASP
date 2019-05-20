namespace Core.Domain
{
    public class FilteredPageRequest
    {
        public uint Page { get; }
        public uint PageSize { get; }
        public string OrderBy { get; }
        public bool OrderByAsc { get; }
        public string SearchTerm { get; }
        public uint Offset => (Page - 1) * PageSize;
        
        public FilteredPageRequest(uint page, uint pageSize, string orderBy, bool orderByAsc, string searchTerm = "")
        {
            Page = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            OrderByAsc = orderByAsc;
            SearchTerm = string.IsNullOrWhiteSpace(searchTerm) ? "%" : searchTerm + "%";
        }
    }
}