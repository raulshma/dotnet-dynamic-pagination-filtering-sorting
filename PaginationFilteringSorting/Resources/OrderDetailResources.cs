using PaginationFilteringSorting.Core.Constants;
using PaginationFilteringSorting.Core.Dto;
using PaginationFilteringSorting.Core.Models.Domain;
using PaginationFilteringSorting.Core.Models;
using PaginationFilteringSorting.Data.Data;
using PaginationFilteringSorting.Helpers;
using PaginationFilteringSorting.Core.Helpers;

namespace PaginationFilteringSorting.Resources;
public static class OrderDetailResources
{
    public static WebApplication AddOrderDetailRoutes(this WebApplication app)
    {
        app.MapGet(ApiResources.OrderDetails, async ([AsParameters] RequestQueryParameters parameters, NorthwindContext context) =>
        {
            var query = context.OrderDetails.AsQueryable();

            query = QueryHelpers.BuildQuery(query, parameters);

            var result = await PaginatedList<OrderDetail, OrderDetailDto>.CreateAsync(query, parameters.Page, parameters.PageSize);
            return result;
        });
        return app;
    }
}
