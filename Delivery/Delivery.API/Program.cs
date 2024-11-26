using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Configura��o de logging para exibir logs no console
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Configura��o de servi�os
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Order API", Version = "v1" });

    // Obtenha o caminho correto do arquivo XML
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

    // Verifique se o arquivo XML existe antes de inclu�-lo
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
    else
    {
        Console.WriteLine($"Aviso: O arquivo XML de documenta��o n�o foi encontrado em {xmlPath}");
    }
});

var app = builder.Build();

// Configura��o do pipeline de middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API V1");
    c.RoutePrefix = string.Empty; // Torna o Swagger acess�vel na raiz (http://localhost:5000)
});

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Mapeia automaticamente os controladores
});

// Log para indicar que a aplica��o est� sendo iniciada
Console.WriteLine("Aplica��o iniciando...");
Console.WriteLine($"Ambiente: {builder.Environment.EnvironmentName}");

// Inicia o servidor
app.Run();