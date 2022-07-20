namespace Flozacode.Models.Paginations
{
    public interface IFlozaPagination<TView, TCreate, TUpdate, TFilter> where TFilter : TableFilter
    {
        public Task<Pagination<TView>> GetPagedAsync(TFilter filter);
        public Task<List<TView>> GetListAsync();
        public Task<TView> FindAsync(long id);
        public Task<int> CreateAsync(TCreate value);
        public Task<int> UpdateAsync(TUpdate value);
        public Task<int> DeleteAsync(long id);
    }
}
