using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MapIt.Helpers
{
    public class SortHelper
    {
        public static IQueryable<T> SortList<T>(IQueryable<T> source, string sortExpression, string sortDirection)
        {
            try
            {
                string[] props = sortExpression.Split('.');
                Type type = typeof(T);
                ParameterExpression arg = Expression.Parameter(type, "x");
                Expression expr = arg;
                foreach (string prop in props)
                {
                    // use reflection (not ComponentModel) to mirror LINQ
                    PropertyInfo pi = type.GetProperty(prop);
                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }
                Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
                LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

                string methodName = "";
                switch (sortDirection.ToLower())
                {
                    case "desc":
                        methodName = "OrderByDescending";
                        break;
                    default:
                        methodName = "OrderBy";
                        break;
                }

                object result = typeof(Queryable).GetMethods().Single(
                        method => method.Name == methodName
                                && method.IsGenericMethodDefinition
                                && method.GetGenericArguments().Length == 2
                                && method.GetParameters().Length == 2)
                        .MakeGenericMethod(typeof(T), type)
                        .Invoke(null, new object[] { source, lambda });
                return (IOrderedQueryable<T>)result;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return null;
            }
        }
    }
}
