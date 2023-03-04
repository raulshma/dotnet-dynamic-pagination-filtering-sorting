using PaginationFilteringSorting.Core.Enums;
using PaginationFilteringSorting.Core.Extensions;
using PaginationFilteringSorting.Core.Models;
using System.Linq.Expressions;
using System.Reflection;
using static System.Linq.Expressions.Expression;

namespace PaginationFilteringSorting.Core.Helpers;
public static class QueryHelpers
{
    public static IQueryable<T> BuildQuery<T>(IQueryable<T> query, RequestQueryParameters parameters)
    {
        var filterParameters = FilterParameters.Create(parameters);

        if (filterParameters.FilterBy.IsNullEmpty().Not() && filterParameters.FilterValue.IsNullEmpty().Not() && filterParameters.FilterOperator.IsNullEmpty().Not())
        {
            query = BuildFilter(query, filterParameters.FilterBy!, filterParameters.FilterValue!, filterParameters.FilterOperator!);
        }
        if (parameters.Sort.IsNullEmpty().Not() && parameters.SortBy.IsNullEmpty().Not())
        {
            query = BuildOrder(query, parameters.Sort!, parameters.SortBy!);
        }
        return query;
    }

    public static bool IsOrdered<T>(this IQueryable<T> queryable)
    {
        if (queryable == null)
        {
            throw new ArgumentNullException(nameof(queryable));
        }

        return queryable.Expression.Type == typeof(IOrderedQueryable<T>);
    }
    public static IQueryable<T> BuildPagination<T>(IQueryable<T> query, RequestQueryParameters parameters)
    {
        if (parameters.Page != default && parameters.PageSize != default)
        {
            query = query.Skip((parameters.Page - 1) * parameters.PageSize).Take(parameters.PageSize);
        }
        return query;
    }
    private static IQueryable<T> BuildOrder<T>(IQueryable<T> query, string sort, string sortBy)
    {
        var objectType = typeof(T);
        var property = objectType.GetProperty(sort); //.GetProperties().FirstOrDefault(e => e.Name == sort);

        if (property == null)
            return query;

        var parameter = Parameter(objectType);
        var accessor = PropertyOrField(parameter, property.Name);

        var expr = Lambda<Func<T, object>>(accessor, parameter);

        if (sortBy == nameof(OrderTypes.asc))
            return query.OrderBy(expr);

        return query.OrderByDescending(expr);
    }

    private static IQueryable<T> BuildFilter<T>(IQueryable<T> query, string filterBy, string filterValue, string op = "eq")
    {
        var objectType = typeof(T);
        var property = objectType.GetProperty(filterBy); //.FirstOrDefault(e => e.Name == filterBy);

        if (property == null) return query;

        var propertyType = property.PropertyType;
        ParameterExpression prm = Parameter(objectType);

        var queryOp = op switch
        {
            "eq" => "Equals",
            "cont" => "Contains",
            _ => "Equals"
        };

        var prop = Property(prm, property);
        var method = propertyType.GetMethod(queryOp, new[] { propertyType })!;

        Expression body = Call(
            prop,
            method,
            Constant(filterValue)
        );

        if (op == "ne") body = Not(body);

        Expression<Func<T, bool>> expr = Lambda<Func<T, bool>>(body, prm);

        if (expr != null)
            query = query.Where(expr);

        return query;
    }
}