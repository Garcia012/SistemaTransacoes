using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UmblerCielo.Server.Services;
using UmblerCielo.Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao cont�iner
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer(); // Adiciona suporte ao Swagger/OpenAPI
builder.Services.AddSwaggerGen();  // Swagger generation

// Configura CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigin",
        builder => builder.AllowAnyOrigin() // Permitir qualquer origem
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.AddHttpClient<CieloService>();

// Configura Kestrel usando appsettings.json
builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.Configure(context.Configuration.GetSection("Kestrel"));
});

var app = builder.Build();

// Configure o pipeline de requisi��o HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Ativa o Swagger
    app.UseSwaggerUI();  // Ativa o Swagger UI para visualizar a documenta��o da API
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigin");  // Adiciona o middleware CORS
app.UseRouting();  // Adiciona roteamento
app.UseAuthorization(); // Adicione se estiver usando autoriza��o

app.MapControllers(); // Usar� o controlador para rotas

app.Run();

