using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração de logging 
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Configuração de serviços
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Order Delivery API", Version = "v1" });

    //Obter o caminho do XML
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configuração do pipeline de middleware
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