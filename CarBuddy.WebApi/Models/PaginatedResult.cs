using CarBuddy.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarBuddy.WebApi.Models
{
    public class PaginatedResult<T, U>
        where T : class
        where U : new()
    {
        public IEnumerable<U> Content { get; }
        public Pagination Pagination { get; }

        public PaginatedResult(IEnumerable<T> items, Pagination pagination, int totalElements)
        {
            Content = items.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .Select(i => (U)Activator.CreateInstance(typeof(U), i));

            Pagination = new Pagination(
                pagination.PageNumber,
                pagination.PageSize,
                totalElements);
        }
    }
}
