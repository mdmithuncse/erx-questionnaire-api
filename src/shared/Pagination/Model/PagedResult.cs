using System.Collections.Generic;

namespace Pagination.Model
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Items { get; set; }

        public PagedResult()
        {
            Items = new List<T>();
        }
    }
}
