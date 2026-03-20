using FCG.Shared.Infrastructure.Configurations;
using FCGCatalog.API.Configurations;
using FCGCatalog.API.Middlewares;
using FCGCatalog.Infrastructure.Configurations;
using FCGCatalog.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterIoCConfigurations();

builder.AddLoggingConfiguration();
builder.AddObservabilidade();
builder.Services.AddControllersConfiguration();
builder.Services.AddDocumentation();
builder.Services.AddProblemDetailsConfiguration();
builder.Services.ConfigureModelStateInvalid();

var app = builder.Build();

await app.AplicarMigracoesAsync();

if (app.Configuration.GetValue<bool>("Documentacao:Habilitada"))
{
	app.UseDocumentation();
}

app.UseGlobalExceptionMiddleware();
app.UseDomainExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

