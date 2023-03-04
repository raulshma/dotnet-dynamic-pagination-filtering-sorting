using PaginationFilteringSorting.Core.Constants;
using PaginationFilteringSorting.Core.Dto;
using PaginationFilteringSorting.Core.Models.Domain;
using PaginationFilteringSorting.Core.Models;
using PaginationFilteringSorting.Core.Helpers;
using PaginationFilteringSorting.Data.Data;
using PaginationFilteringSorting.Helpers;

namespace PaginationFilteringSorting.Resources;
public static class ProductResources
{
    public static WebApplication AddProductRoutes(this WebApplication app)
    {
        app.MapGet(ApiResources.Products, async ([AsParameters] RequestQueryParameters parameters, NorthwindContext context) =>
        {
            var query = context.Products.AsQueryable();

            query = QueryHelpers.BuildQuery(query, parameters);

            var result = await PaginatedList<Product, ProductDto>.CreateAsync(query, parameters.Page, parameters.PageSize);
            return result;
        });
        return app;
    }
}
