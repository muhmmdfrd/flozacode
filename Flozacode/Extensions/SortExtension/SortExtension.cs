using Flozacode.Extensions.ExpressionExtension;

namespace Flozacode.Extensions.SortExtension
{
    public static class SortExtension
    {
        public static IOrderedQueryable<T> SortBy<T>(this IQueryable<T> source, string sortName, string sortDir)
        {
            sortDir = string.IsNullOrEmpty(sortDir) ? "asc" : sortDir;
            sortDir = sortDir.ToLower();

            var acceptableSortDir = new List<string>
            {
                "asc",
                "desc",
                "ascending",
                "descending"
            };

            bool isValidSortDir = false;

            foreach (var item in acceptableSortDir)
            {
                if (sortDir.Equals(item))
                {
                    isValidSortDir = true;
                    break;
                }
            }

            sortDir = !isValidSortDir ? "asc" : sortDir;

            var ascending = (sortDir == "asc" || sortDir == "ascending" || sortDir == "a");

            if (string.IsNullOrWhiteSpace(sortName))
                sortName = "Id";

            if (sortName.Equals("Id"))
                ascending = false;

            Type type = typeof(T);
            var lambda = (dynamic)type.CreateExpression(sortName);
            var sorted = ascending
                ? Queryable.OrderBy(source, lambda)
                : Queryable.OrderByDescending(source, lambda);

            return sorted;
        }
    }
}
