using Microsoft.EntityFrameworkCore;

namespace Flozacode.Repository
{
    public interface IFlozaRepo<T, TData> where TData : DbContext where T : class
    {
        Task<T> GetByIdAsync<TIdType>(TIdType id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<List<T>> GetListAsync();
        Task<IQueryable<T>> GetQueryAsync();
        Task<IQueryable<TDto>> GetQueryAsync<TDto>();
        Task<T> FindAsync(long id);
        Task<TDto> FindAsync<TDto>(long id);
        Task<IReadOnlyList<TDto>> GetAllAsync<TDto>();
        Task<List<T>> GetListAsync(Func<T, bool>? condition = null);
        Task<List<TDto>> GetListAsync<TDto>(Func<T, bool>? condition = null);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> RecordCountAsync();
        Task<int> RecordCountAsync(Func<T, bool>? condition = null);
        int RecordCount(Func<T, bool>? condition = null);
        IQueryable<T> AsQueryable { get; }
        IEnumerable<T> AsEnumerable { get; }
        Task<int> AddAsync(T entity, bool saveChanges = true, string errorMessage = "");
        Task<int> UpdateAsync(T entity, bool saveChanges = true, string errorMessage = "");
        Task<int> DeleteAsync(T Entity, bool saveChanges = true, string errorMessage = "");
        Task<int> AddMultipleAsync(List<T> entities, bool saveChanges = true, string errorMessage = "");
        Task<int> UpdateMultipleAsync(List<T> entities, bool saveChanges = true, string errorMessage = "");
        Task<int> DeleteMultipleAsync(List<T> entities, bool saveChanges = true, string errorMessage = "");
    }
}
