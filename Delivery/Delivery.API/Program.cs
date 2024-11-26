using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Configuração de logging para exibir logs no console
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Configuração de serviços
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Order API", Version = "v1" });

    // Obtenha o caminho correto do arquivo XML
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

    // Verifique se o arquivo XML existe antes de incluí-lo
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
    else
    {
        Console.WriteLine($"Aviso: O arquivo XML de documentação não foi encontrado em {xmlPath}");
    }
});

var app = builder.Build();

// Configuração do pipeline de middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API V1");
    c.RoutePrefix = string.Empty; // Torna o Swagger acessível na raiz (http://localhost:5000)
});

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Mapeia automaticamente os controladores
});

// Log para indicar que a aplicação está sendo iniciada
Console.WriteLine("Aplicação iniciando...");
Console.WriteLine($"Ambiente: {builder.Environment.EnvironmentName}");

// Inicia o servidor
app.Run();