using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using PaginationFilteringSorting.Data.Data;
using System.IO.Compression;

namespace PaginationFilteringSorting;
public static class ConfigureServices
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, string? connectionString)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();
        services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;

        });
        services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(connectionString));
        services.AddCors();
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });
        services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Fastest;
        });

        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.SmallestSize;
        });
        services.AddResponseCaching();
        return services;
    }
}