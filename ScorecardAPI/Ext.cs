using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

namespace ScorecardAPI
{
    public class Ext
    {
    }

    public static class IQueryableExtensions
    {
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> queryable) where T : class
        {
            var entityType = typeof(T);
            var properties = entityType.GetProperties().Where(p => typeof(IEnumerable).IsAssignableFrom(p.PropertyType) && p.PropertyType != typeof(string));

            foreach (var property in properties)
            {
                queryable = queryable.Include(property.Name);
            }

            return queryable;
        }
    }
}
