using Core.Domain;
using Domain;
using Microsoft.AspNetCore.Http;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace Core.Web.Controllers.Api
{
    [AppExceptionFilter]
    public class RootApiController : Controller
    {
        
    }

    public static class FilteredPage
    {
        public static FilteredPageRequest FilteredPageRequest(this HttpRequest request, string defaultOrderBy, bool defaultOrderByAscending)
        {
            var page = request.Query.TryGetValue("page", out var pageString) ? uint.Parse(pageString) : 1;

            var pageSize = request.Query.TryGetValue("pageSize", out var pageSizeString) ? uint.Parse(pageSizeString) : 10;

            var orderByAsc = request.Query.TryGetValue("orderByAsc", out var orderByAscString) ? bool.Parse(orderByAscString) : defaultOrderByAscending;
            
            request.Query.TryGetValue("orderBy", out var orderByStringValues);
            var orderBy = string.IsNullOrWhiteSpace(orderByStringValues.ToString()) ? defaultOrderBy : orderByStringValues.ToString();
            
            request.Query.TryGetValue("searchTerm", out var searchTermString);
            var searchTerm = string.IsNullOrWhiteSpace(searchTermString.ToString()) ? "" : searchTermString.ToString();
            
            return new FilteredPageRequest(page, pageSize, orderBy, orderByAsc, searchTerm);
        }
    }
}