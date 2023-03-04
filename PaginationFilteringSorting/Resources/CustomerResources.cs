using PaginationFilteringSorting.Core.Constants;
using PaginationFilteringSorting.Core.Dto;
using PaginationFilteringSorting.Core.Models.Domain;
using PaginationFilteringSorting.Core.Models;
using PaginationFilteringSorting.Data.Data;
using PaginationFilteringSorting.Helpers;
using PaginationFilteringSorting.Core.Helpers;

namespace PaginationFilteringSorting.Resources;
public static class CustomerResources
{
    public static WebApplication AddCustomerRoutes(this WebApplication app)
    {
        app.MapGet(ApiResources.Customers, async ([AsParameters] RequestQueryParameters parameters, NorthwindContext context) =>
        {
            var query = context.Customers.AsQueryable();

            query = QueryHelpers.BuildQuery(query, parameters);

            var result = await PaginatedList<Customer, CustomerDto>.CreateAsync(query, parameters.Page, parameters.PageSize);
            return result;
        });
        return app;
    }
}
