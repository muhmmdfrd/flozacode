using AutoMapper;
using AutoMapper.QueryableExtensions;
using Flozacode.Exceptions;
using Flozacode.Extensions.ExpressionExtension;
using Flozacode.Extensions.StringExtension;
using Flozacode.Models.Constants;
using Microsoft.EntityFrameworkCore;

namespace Flozacode.Repository
{
    public class FlozaRepo<T, TData> : IFlozaRepo<T, TData> where TData : DbContext where T : class
    {
        private readonly TData _db;
        private readonly IMapper _mapper;

        public FlozaRepo(TData db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> AddAsync(T entity, bool saveChanges = true, string errorMessage = "")
        {
            await _db.Set<T>().AddAsync(entity);

            if (saveChanges)
            {
                int result = await _db.SaveChangesAsync();

                if (result == 0)
                {
                    throw new Exception(errorMessage.IsNullOrEmpty() ? Message.ERROR_CREATE : errorMessage);
                }

                return result;
            }

            return 0;
        }

        public async Task<int> UpdateAsync(T entity, bool saveChanges = true, string errorMessage = "")
        {
            _db.Entry(entity).State = EntityState.Modified;

            if (saveChanges)
            {
                int result = await _db.SaveChangesAsync();

                if (result == 0)
                {
                    throw new Exception(errorMessage.IsNullOrEmpty() ? Message.ERROR_UPDATE : errorMessage);
                }

                return result;
            }

            return 0;
        }

        public async Task<int> DeleteAsync(T entity, bool saveChanges = true, string errorMessage = "")
        {
            _db.Set<T>().Remove(entity);

            if (saveChanges)
            {
                int result = await _db.SaveChangesAsync();

                if (result == 0)
                {
                    throw new Exception(string.IsNullOrEmpty(errorMessage) ? Message.ERROR_DELETE : errorMessage);
                }

                return result;
            }

            return 0;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetListAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<IQueryable<T>> GetQueryAsync()
        {
            IQueryable<T> query = _db.Set<T>().AsQueryable();
            return await Task.FromResult(query);
        }

        public async Task<IQueryable<TDto>> GetQueryAsync<TDto>()
        {
            IQueryable<TDto> query = _db.Set<T>().AsQueryable().ProjectTo<TDto>(_mapper.ConfigurationProvider);
            return await Task.FromResult(query);
        }

        public async Task<T> FindAsync(long id)
        {
            return await _db.Set<T>().AsQueryable().FindOrThrowAsync(id);
        }

        public async Task<TDto> FindAsync<TDto>(long id)
        {
            IQueryable<TDto> query = await GetQueryAsync<TDto>();
            return await query.FindOrThrowAsync(id);
        }

        public async Task<IReadOnlyList<TDto>> GetAllAsync<TDto>()
        {
            List<TDto> results = _db.Set<T>().AsQueryable().ProjectTo<TDto>(_mapper.ConfigurationProvider).ToList();
            return await Task.FromResult(results);
        }

        public async Task<List<T>> GetListAsync(Func<T, bool>? condition = null)
        {
            List<T> results = condition == null ?
                _db.Set<T>().AsQueryable().ToList() : 
                _db.Set<T>().Where(condition).AsQueryable().ToList();

            return await Task.FromResult(results);
        }

        public async Task<List<TDto>> GetListAsync<TDto>(Func<T, bool>? condition = null)
        {
            List<TDto> results = condition == null ?
                _db.Set<T>().AsQueryable().ProjectTo<TDto>(_mapper.ConfigurationProvider).ToList() :
                _db.Set<T>().Where(condition).AsQueryable().ProjectTo<TDto>(_mapper.ConfigurationProvider).ToList();

            return await Task.FromResult(results);
        }

        public async Task<T> GetByIdAsync<TIdType>(TIdType id)
        {
            T? data = await _db.Set<T>().FindAsync(id);

            if (data == null)
            {
                throw new RecordNotFoundException(string.Format(Message.ERROR_DATA_ID_NOT_FOUND, id));
            }

            return data;
        }

        public async Task<int> RecordCountAsync()
        {
            return await _db.Set<T>().CountAsync();
        }

        public IQueryable<T> AsQueryable => _db.Set<T>();

        public IEnumerable<T> AsEnumerable => _db.Set<T>().AsEnumerable();

        public async Task<int> AddMultipleAsync(List<T> entities, bool saveChanges = true, string errorMessage = "")
        {
            _db.Set<T>().AddRange(entities);

            if (saveChanges)
            {
                int result = await _db.SaveChangesAsync();

                if (result == 0)
                {
                    throw new CrudFailedException(string.IsNullOrEmpty(errorMessage) ? Message.ERROR_CREATE : errorMessage);
                }

                return result;
            }

            return 0;
        }

        public async Task<int> DeleteMultipleAsync(List<T> entities, bool saveChanges = true, string errorMessage = "")
        {
            _db.Set<T>().RemoveRange(entities);

            if (saveChanges)
            {
                int result = await _db.SaveChangesAsync();

                if (result == 0)
                {
                    throw new CrudFailedException(string.IsNullOrEmpty(errorMessage) ? Message.ERROR_DELETE : errorMessage);
                }

                return result;
            }

            return 0;
        }

        public async Task<int> UpdateMultipleAsync(List<T> entities, bool saveChanges = true, string errorMessage = "")
        {
            foreach (var entity in entities)
            {
                _db.Entry(entity).State = EntityState.Modified;
            }

            if (saveChanges)
            {
                int result = await _db.SaveChangesAsync();

                if (result == 0)
                {
                    throw new CrudFailedException(string.IsNullOrEmpty(errorMessage) ? Message.ERROR_UPDATE : errorMessage);
                }

                return result;
            }

            return 0;
        }

        public int RecordCount(Func<T, bool>? condition = null)
        {
            DbSet<T> db = _db.Set<T>();

            if (condition == null)
            {
                return db.AsQueryable().Count();
            }

            return db.Where(condition).AsQueryable().Count();
        }

        public async Task<int> RecordCountAsync(Func<T, bool>? condition = null)
        {
            DbSet<T> db = _db.Set<T>();

            if (condition == null)
            {
                return await db.AsQueryable().CountAsync();
            }

            return await db.Where(condition).AsQueryable().CountAsync();
        }
    }
}
