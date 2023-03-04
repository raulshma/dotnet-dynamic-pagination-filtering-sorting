using PaginationFilteringSorting.Core.Constants;
using PaginationFilteringSorting.Core.Dto;
using PaginationFilteringSorting.Core.Models.Domain;
using PaginationFilteringSorting.Core.Models;
using PaginationFilteringSorting.Data.Data;
using PaginationFilteringSorting.Helpers;
using PaginationFilteringSorting.Core.Helpers;

namespace PaginationFilteringSorting.Resources;
public static class OrderResources
{
    public static WebApplication AddOrderRoutes(this WebApplication app)
    {
        app.MapGet(ApiResources.Orders, async ([AsParameters] RequestQueryParameters parameters, NorthwindContext context) =>
        {
            var query = context.Orders.AsQueryable();

            query = QueryHelpers.BuildQuery(query, parameters);

            var result = await PaginatedList<Order, OrderDto>.CreateAsync(query, parameters.Page, parameters.PageSize);
            return result;
        });
        return app;
    }
}
