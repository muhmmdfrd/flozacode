using AutoMapper;
using Flozacode.Exceptions;
using Flozacode.Extensions.StringExtension;
using Flozacode.Models.Constants;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Flozacode.Extensions.ExpressionExtension
{
    public static class ExpressionExtension
	{
		public static LambdaExpression CreateExpression(this Type type, string propertyName)
		{
			var param = Expression.Parameter(type, "x");
			Expression body = param;

			foreach (string member in propertyName.Split('.'))
			{
				body = Expression.PropertyOrField(body, member);
			}

			return Expression.Lambda(body, param);
		}

		public static Expression<Func<T, bool>> CreateExpression<T, T1>(string method, string propertyName, T1 value)
		{
			var arg = Expression.Parameter(typeof(T), "x");
			var body = Expression.Call(
				Expression.Property(arg, propertyName),
				method,
				null,
				Expression.Constant(value));
			var predicate = Expression.Lambda<Func<T, bool>>(body, arg);

			return predicate;
		}

		public static T FindOrThrow<T>(this IQueryable<T> source, object id, string errorMessage = "")
		{
			if (source == null)
			{
				throw new ArgumentException("Cannot accept nullable value.", nameof(source));
			}

			var expression = CreateExpression<T, object>("Equals", "id", id);
			var result = source.FirstOrDefault(expression);

			if (result == null)
			{
				errorMessage = errorMessage.Equals(string.Empty) ? "Data not found." : errorMessage;

				throw new RecordNotFoundException(errorMessage);
			}

			return result;
		}

		public static TDestination FindOrThrow<TSource, TDestination>(this IQueryable<TSource> source, object id, string errorMessage = "")
		{
			if (source == null)
			{ 
				throw new ArgumentException("Cannot accept nullable value.", nameof(source));
			}

			var expression = CreateExpression<TSource, object>("Equals", "id", id);
			var result = source.FirstOrDefault(expression);

			if (result == null)
			{
				errorMessage = errorMessage.IsNullOrEmpty() ?
					string.Format(Message.ERROR_DATA_ID_NOT_FOUND, id) : 
					errorMessage;

				throw new RecordNotFoundException(errorMessage);
			}

			var configMapper = new MapperConfiguration(x => x.CreateMap<TSource, TDestination>());
			var mapper = configMapper.CreateMapper();

			return mapper.Map<TDestination>(result);
		}

		public static async Task<T> FindOrThrowAsync<T>(this IQueryable<T> source, object id, string errorMessage = "")
		{
			if (source == null)
			{
				throw new ArgumentException("Cannot accept nullable value.", nameof(source));
			}

			var expression = CreateExpression<T, object>("Equals", "id", id);
			var result = await source.FirstOrDefaultAsync(expression);

			if (result == null)
			{
				errorMessage = errorMessage.IsNullOrEmpty() ? 
					string.Format(Message.ERROR_DATA_ID_NOT_FOUND, id) : 
					errorMessage;

				throw new RecordNotFoundException(errorMessage);
			}

			return result;
		}

		public static async Task<TDestination> FindOrThrowAsync<TSource, TDestination>(this IQueryable<TSource> source, object id, string errorMessage = "")
		{
			if (source == null)
			{
				throw new ArgumentException("Cannot accept nullable value.", nameof(source));
			}

			var expression = CreateExpression<TSource, object>("Equals", "id", id);
			var result = await source.FirstOrDefaultAsync(expression);

			if (result == null)
			{
				errorMessage = errorMessage.Equals(string.Empty) ? 
					string.Format(Message.ERROR_DATA_ID_NOT_FOUND, id) : 
					errorMessage;

				throw new RecordNotFoundException(errorMessage);
			}

			var configMapper = new MapperConfiguration(x => x.CreateMap<TSource, TDestination>());
			var mapper = configMapper.CreateMapper();

			return mapper.Map<TDestination>(result);
		}

		public static IQueryable<T> WhereOrThrow<T>(this IQueryable<T> source, string propertyName, object value, string errorMessage = "")
		{
			if (source == null)
			{
				throw new ArgumentException("Cannot accept nullable value.", nameof(source));
			}

			var expression = CreateExpression<T, object>("Equals", propertyName, value);
			var result = source.Where(expression);

			if (!result.Any())
			{
				errorMessage = errorMessage.Equals(string.Empty) ? 
					"Data not found." : 
					errorMessage;

				throw new RecordNotFoundException(errorMessage);
			}

			return result;
		}

		public static IQueryable<T> WhereOrThrow<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, string errorMessage = "")
		{
			if (source == null)
			{
				throw new ArgumentException("Cannot accept nullable value.", nameof(source));
			}

			var result = source.Where(predicate);

			if (!result.Any())
			{
				errorMessage = errorMessage.Equals(string.Empty) ? "Data not found." : errorMessage;

				throw new RecordNotFoundException(errorMessage);
			}

			return result;
		}

		public static async Task<IQueryable<T>> WhereOrThrowAsync<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, string errorMessage = "")
		{
			if (source == null)
			{
				throw new ArgumentException("Cannot accept nullable value.", nameof(source));
			}

			var result = source.Where(predicate);

			if (!result.Any())
			{
				errorMessage = errorMessage.Equals(string.Empty) ? "Data not found." : errorMessage;

				throw new RecordNotFoundException(errorMessage);
			}

			return await Task.Run(() => result);
		}

		public static T FirstOrThrow<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, string errorMessage = "")
		{
			if (source == null)
			{
				throw new ArgumentException("Canot accept nullable value.", nameof(source));
			}

			var result = source.FirstOrDefault(predicate);

			if (result == null)
			{
				errorMessage = errorMessage.Equals(string.Empty) ? "Data not found." : errorMessage;

				throw new RecordNotFoundException(errorMessage);
			}

			return result;
		}

		public static TDestination FirstOrThrow<TSource, TDestination>(
			this IQueryable<TSource> source,
			Expression<Func<TSource, bool>> predicate,
			string errorMessage = ""
		)
		{
			if (source == null)
			{
				throw new ArgumentException("Canot accept nullable value.", nameof(source));
			}

			var result = source.FirstOrDefault(predicate);

			if (result == null)
			{
				errorMessage = errorMessage.Equals(string.Empty) ? "Data not found." : errorMessage;

				throw new RecordNotFoundException(errorMessage);
			}

			var configMapper = new MapperConfiguration(x => x.CreateMap<TSource, TDestination>());
			var mapper = configMapper.CreateMapper();

			return mapper.Map<TDestination>(result);
		}

		public static async Task<T> FirstOrThrowAsync<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, string errorMessage = "")
		{
			if (source == null)
			{
				throw new ArgumentException("Cannot accept nullable value.", nameof(source));
			}

			var result = await source.FirstOrDefaultAsync(predicate);

			if (result == null)
			{
				errorMessage = errorMessage.Equals(string.Empty) ? "Data not found." : errorMessage;

				throw new RecordNotFoundException(errorMessage);
			}

			return result;
		}

		public static async Task<TDestination> FirstOrThrowAsync<TSource, TDestination>(
			this IQueryable<TSource> source,
			Expression<Func<TSource, bool>> predicate,
			string errorMessage = ""
		)
		{
			if (source == null)
			{
				throw new ArgumentException("Cannot accept nullable value.", nameof(source));
			}

			var result = await source.FirstOrDefaultAsync(predicate);

			if (result == null)
			{
				errorMessage = errorMessage.Equals(string.Empty) ? "Data not found." : errorMessage;

				throw new RecordNotFoundException(errorMessage);
			}

			var configMapper = new MapperConfiguration(x => x.CreateMap<TSource, TDestination>());
			var mapper = configMapper.CreateMapper();

			return mapper.Map<TDestination>(result);
		}

		public static bool HasProperty(this object obj, string propertyName)
        {
			return obj.GetType().GetProperty(propertyName) != null;
        }

		public static bool HasPropertyAndIntanceOf<T>(this object obj, string propertyName)
        {
			return obj.HasProperty(propertyName) && obj.GetType() == typeof(T);
        }
	}
}
