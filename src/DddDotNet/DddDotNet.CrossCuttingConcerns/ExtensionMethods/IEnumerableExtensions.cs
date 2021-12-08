using System;
using System.Collections.Generic;
using System.Linq;

namespace DddDotNet.CrossCuttingConcerns.ExtensionMethods
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Paged<T>(this IEnumerable<T> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> query, bool condition, Func<T, bool> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }

        public static IEnumerable<TSource> OrderByIf<TSource, TKey>(this IEnumerable<TSource> query, bool condition, Func<TSource, TKey> keySelector, bool ascending = true)
        {
            if (condition)
            {
                return ascending ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);
            }

            return query;
        }
    }
}
