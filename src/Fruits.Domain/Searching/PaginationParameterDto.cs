namespace Fruits.Domain.Searching
{
    public class PaginationParameterDto
    {
        public int Page { get; set; } = 1;
        public int ItemsByPage { get; set; } = 10;
    }
}
