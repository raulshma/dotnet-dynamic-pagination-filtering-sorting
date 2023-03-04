using Serilog;
using PaginationFilteringSorting;
using PaginationFilteringSorting.Resources;
using PaginationFilteringSorting.Core.Constants;
using PaginationFilteringSorting.Helpers;

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Configuration.GetRequiredSection(nameof(AppSettings)).Get<AppSettings>();

if (appSettings == null) throw new Exception("Could not load app settings");

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.Seq(appSettings.SeqUrl)
    .CreateBootstrapLogger();


builder.Host.UseSerilog();
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddAppServices(connectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseResponseCompression();
app.UseResponseCaching();

app.MapGet(ApiResources.Health, () => Results.Ok());

app.AddCustomerRoutes();
app.AddOrderRoutes();
app.AddOrderDetailRoutes();
app.AddProductRoutes();

app.Run();