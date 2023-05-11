using Microsoft.EntityFrameworkCore;

namespace DATN.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; } = 1;
        public int TotalPage { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling((double)count / pageSize);
            AddRange(items);
        }

        public static async Task<PaginatedList<T>> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((int)(pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, (int)count, pageIndex, pageSize);
        }
    }
}
