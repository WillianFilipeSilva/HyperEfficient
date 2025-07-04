namespace HyperEfficient.Dtos.Base
{
    public class GetAllResponseBase<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
    }
}