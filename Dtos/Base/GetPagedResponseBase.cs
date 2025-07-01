namespace HyperEfficient.Dtos.Base;

public class GetPagedResponseBase<T> where T : class
{
    public IEnumerable<T> Data { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
} 