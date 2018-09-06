using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using SnipeLinksGenerator.Services.PoeTrade.Models;

namespace SnipeLinksGenerator.Services.PoeTrade
{
    public static class Helpers
    {
        public static string ToQueryString(this Query query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var properties = query.GetType().GetProperties()
                .Where(x => x.CanRead);

            var result = new Dictionary<string, string>();

            foreach (var property in properties)
            {
                string value;
                if (property.IsNonStringEnumerable())
                {
                    continue;
                }

                var display = (DisplayNameAttribute)property.GetCustomAttribute(typeof(DisplayNameAttribute));
                var name = display != null ? display.DisplayName : property.Name;

                var rawValue = property.GetValue(query, null);

                if (rawValue == null)
                {
                    value = string.Empty;
                    result.Add(name, value);
                    continue;
                }

                if (rawValue.IsOfNullableType() && rawValue is bool bn)
                {
                    value = bn ? "1" : "0";
                    result.Add(name, value);
                    continue;
                }

                switch (rawValue)
                {
                    case bool b:
                        value = b ? "x" : string.Empty;
                        break;

                    case Enum e:
                        var attribute = (DisplayNameAttribute)e.GetType().GetMember(e.ToString()).Single().GetCustomAttribute(typeof(DisplayNameAttribute));
                        value = attribute != null ? attribute.DisplayName : e.ToString("D");
                        break;

                    default:
                        value = rawValue.ToString();
                        break;
                }

                result.Add(name, value);
            }

            return string.Join("&", result.Select(x => $"{x.Key}={x.Value.Replace(" ", "+")}"));
        }

        public static bool IsNonStringEnumerable(this PropertyInfo pi)
        {
            return pi != null && pi.PropertyType.IsNonStringEnumerable();
        }

        public static bool IsNonStringEnumerable(this object instance)
        {
            return instance != null && instance.GetType().IsNonStringEnumerable();
        }

        public static bool IsNonStringEnumerable(this Type type)
        {
            if (type == null || type == typeof(string))
                return false;
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        public static bool IsOfNullableType<T>(this T o)
        {
            var type = typeof(T);
            return type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}
