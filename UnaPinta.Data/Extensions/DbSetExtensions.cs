using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Data.Extensions
{
    public static class DbSetExtensions
    {
        public static IQueryable<T> IncludeMany<T>(this IQueryable<T> source, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes) where T : class
        {
            if (source == null) throw new NullReferenceException(nameof(source));
            if (includes != null) source = includes(source);
            return source;
        }
    }
}
