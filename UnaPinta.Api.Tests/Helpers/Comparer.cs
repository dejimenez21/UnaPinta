using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;

namespace UnaPinta.Api.Tests.Unit.Helpers
{
    public class Comparer<T, TKey> : IEqualityComparer<T> where T : BaseEntity<TKey>
    {
        public bool Equals(T x, T y)
        {
            var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            return JsonConvert.SerializeObject(x, settings) == JsonConvert.SerializeObject(y, settings);
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            return HashCode.Combine(obj.Id, obj.CreatedAt, obj.LastUpdatedAt, obj.DeletedAt);
        }
    }
}
