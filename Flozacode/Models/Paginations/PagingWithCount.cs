namespace Flozacode.Models.Paginations
{
    public class PagingWithCount<T>
    {
        public int Count { get; set; }
        public List<T>? List { get; set; }
    }
}
