using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Lisa.Breakpoint.WebApi.Data
{
    public static class Utility
    {            
        // id, members { role, users/name }
        public static object ExcecuteQuery<T>(T item, string query)
            where T : class
        {
            query = query.Replace(" ", "");
            var result = new ExpandoObject() as IDictionary<string, object>;
            string sub = "";

            for (int i = 0; i < query.Length; i++)
            {
                var curChar = query[i];

                switch(curChar)
                {
                    case('{'):
                        var prop = typeof(T).GetProperty(sub);
                        result.Add(sub, ExcecuteQuery(prop.GetValue(item), RemoveFirst(query.Substring(++i), '}')));
                        sub = "";
                        break;
                    case('/'):
                        prop = typeof(T).GetProperty(sub);
                        if (prop.PropertyType.IsArray || typeof(IEnumerable<>).IsAssignableFrom(prop.PropertyType))
                        {
                            result.Add(sub, GetValues(prop.GetValue(item) as T[], GetRemainder(query.Substring(++i))));
                        }
                        else
                        {
                            result.Add(sub, prop.PropertyType.GetProperty(GetRemainder(query.Substring(++i))));
                        }
                        sub = "";
                        break;
                    case(','):
                        result.Add(sub, typeof(T).GetProperty(sub).GetValue(item));
                        sub = "";
                        break;
                    default:
                        sub += curChar;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(sub))
            {
                result.Add(sub, typeof(T).GetProperty(sub).GetValue(item));
            }

            return result as ExpandoObject;
        }

        private static object[] GetValues<T>(T[] item, string prop)
        {
            List<object> result = new List<object>();

            foreach (T p in item)
            {
                result.Add(typeof(T).GetProperty(prop).GetValue(p));
            }

            return result.ToArray();
        }

        private static string GetRemainder(string value)
        {
            var result = "";

            for (int i = 0; i < value.Length; i++)
            {
                var curChar = value[i];
                if (curChar == '/' || curChar == '{' || curChar == '}' || curChar == ',')
                {
                    break;
                }
                else
                {
                    result += curChar;
                }
            }

            return result;
        }

        private static string RemoveFirst(string value, char c)
        {
            var result = "";
            bool found = false;

            for (int i = 0; i < value.Length; i++)
            {
                char curChar = value[i];
                if (curChar == c && !found)
                {
                    found = true;
                }
                else
                {
                    result += curChar;
                }
            }

            return result;
        }
    }
}