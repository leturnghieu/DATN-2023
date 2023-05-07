namespace DATN.DTOs
{
    public class PaginatedResponse<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}
