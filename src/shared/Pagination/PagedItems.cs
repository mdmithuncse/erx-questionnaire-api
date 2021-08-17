using Microsoft.EntityFrameworkCore;
using Pagination.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pagination
{
    public static class PagedItems
    {
        public static async Task<PagedResult<T>> GetPagedItemsAsync<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }
    }
}
