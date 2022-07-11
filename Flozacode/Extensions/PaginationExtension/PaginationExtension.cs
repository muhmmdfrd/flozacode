using Flozacode.Extensions.SortExtension;
using Flozacode.Extensions.StringExtension;
using Flozacode.Models.Paginations;
using Microsoft.EntityFrameworkCore;

namespace Flozacode.Extensions.PaginationExtension
{
    public static class PaginationExtension
    {
		public static IQueryable<T> Paging<T>(this IQueryable<T> source, TableFilter filter)
		{
			if (filter.SortDir.IsNullOrEmpty())
				filter.SortDir = "asc";

			if (filter.SortName.IsNullOrEmpty())
				filter.SortName = "id";

			return source
				.SortBy(filter.SortName, filter.SortDir)
				.Skip(filter.Skip)
				.Take(filter.Size);
		}

		public static List<T> PagingToList<T>(this IQueryable<T> source, TableFilter filter)
		{
			return source.Paging(filter).ToList();
		}

		public static async Task<List<T>> PagingToListAsync<T>(this IQueryable<T> source, TableFilter filter)
		{
			var result = source.Paging(filter);
			return await result.ToListAsync();
		}

		public static PagingWithCount<T> PagingToListWithCount<T>(this IQueryable<T> source, TableFilter filter)
		{
			var query = source.Paging(filter);
			var count = query.Count();
			var list = query.ToList();

			return new PagingWithCount<T>
			{
				List = list,
				Count = count
			};
		}

		public async static Task<PagingWithCount<T>> PagingToListWithCountAsync<T>(this IQueryable<T> source, TableFilter filter)
		{
			var query = source.Paging(filter);
			var count = await query.CountAsync();
			var list = await query.ToListAsync();

			return new PagingWithCount<T>
			{
				List = list,
				Count = count
			};
		}
	}
}
