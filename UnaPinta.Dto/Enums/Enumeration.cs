using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Dto.Enums
{
    public abstract class Enumeration : IComparable
    {
        private readonly int _value;
        private readonly string _description;

        public int Value { get { return _value; } }
        public string Description { get { return _description; } }

        protected Enumeration() { }

        protected Enumeration(int value, string description)
        {
            _value = value;
            _description = description;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach(var field in fields)
            {
                var instance = new T();
                var locatedValue = field.GetValue(instance) as T;

                if (locatedValue != null) yield return locatedValue;
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as Enumeration;

            if (other == null) return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = _value.Equals(other.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => _value.GetHashCode();

        protected static T parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);
            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                throw new ApplicationException(message);
            }

            return matchingItem;
        }


        public int CompareTo(object other) => Value.CompareTo(((Enumeration)other).Value);
        
    }
}
