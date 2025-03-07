using Microsoft.OpenApi.Models;
using Delivery.Domain.Services;
using Delivery.Domain.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();

builder.Services.AddScoped<IOrderDeliveryService, OrderDeliveryService>();
builder.Services.AddScoped<IOrderDeliveryRepository, OrderDeliveryRepository>(); // Se existir uma implementação

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Order Delivery API", Version = "v1" });

    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Delivery API V1");
    c.RoutePrefix = string.Empty;
});

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    if (endpoints != null)
        endpoints.MapControllers();
    else
        throw new ArgumentNullException(nameof(endpoints));
});

app.Run();
