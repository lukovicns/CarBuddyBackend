namespace CarBuddy.Application.Models
{
    public class Pagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalElements { get; set; }

        public Pagination() { }

        public Pagination(int page, int size, int totalElements)
        {
            PageNumber = page;
            PageSize = size;
            TotalElements = totalElements;
        }
    }
}
