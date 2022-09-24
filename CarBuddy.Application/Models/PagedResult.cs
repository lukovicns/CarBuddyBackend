using System.Collections.Generic;
using System.Linq;

namespace CarBuddy.Application.Models
{
    public class PagedResult<T> where T : class
    {
        public IEnumerable<T> Content { get; private set; }
        public Pagination Pagination { get; private set; }
        
        public PagedResult(IEnumerable<T> items, Pagination pagination)
        {
            Content = GetPagedContent(items, pagination.PageNumber, pagination.PageSize);
            Pagination = new Pagination(pagination.PageNumber, pagination.PageSize, Content.Count());
        }

        public PagedResult(IEnumerable<T> items, int page, int size)
        {
            Content = GetPagedContent(items, page, size);
            Pagination = new Pagination(page, size, Content.Count());
        }

        private IEnumerable<T> GetPagedContent(IEnumerable<T> items, int page, int size) =>
            items.Skip((page - 1) * size)
                .Take(size)
                .ToList();
    }
}
