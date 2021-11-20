using System;
using System.Linq;
using System.Linq.Expressions;

namespace DddDotNet.CrossCuttingConcerns.ExtensionMethods
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paged<T>(this IQueryable<T> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }

        public static IQueryable<TSource> OrderByIf<TSource, TKey>(this IQueryable<TSource> query, bool condition, Expression<Func<TSource, TKey>> keySelector, bool ascending = true)
        {
            if (condition)
            {
                return ascending ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);
            }

            return query;
        }
    }
}
