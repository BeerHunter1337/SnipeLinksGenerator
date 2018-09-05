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
            bool IsOfNullableType<T>(T o)
            {
                var type = typeof(T);
                return type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>));
            }

            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var properties = query.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(query, null) != null);

            var result = new Dictionary<string, string>();

            foreach (var property in properties)
            {
                string value;
                if ((property.PropertyType == typeof(IEnumerable)) && (property.PropertyType != typeof(string)))
                {
                    continue;
                }

                var display = (DisplayNameAttribute)property.GetCustomAttribute(typeof(DisplayNameAttribute));
                var name = display != null ? display.DisplayName : property.Name;

                var rawValue = property.GetValue(query, null);

                if (IsOfNullableType(rawValue) && rawValue is bool b1)
                {
                    value = b1 ? "1" : "0";
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

            return string.Join("&", result
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }
    }
}
