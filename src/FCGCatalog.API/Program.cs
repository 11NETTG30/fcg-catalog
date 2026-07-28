using FCG.Shared.Infrastructure.Configurations;
using FCG.Shared.Infrastructure.Extensions;
using FCGCatalog.API.Configurations;
using FCGCatalog.API.Middlewares;
using FCGCatalog.Infrastructure.Configurations;
using FCGCatalog.Infrastructure.Search;
using FCGCatalog.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.AddSecurity();
builder.RegisterIoCConfigurations();
builder.AddLoggingConfiguration();
builder.AddObservabilidade();
builder.Services.AddControllersConfiguration();
builder.Services.AddDocumentation();
builder.Services.AddProblemDetailsConfiguration();
builder.Services.ConfigureModelStateInvalid();

var app = builder.Build();

await app.AplicarMigracoesAsync();

using (var scope = app.Services.CreateScope())
{
	var indiceInicializador = scope.ServiceProvider.GetRequiredService<IndiceJogosInicializador>();
	await indiceInicializador.GarantirIndiceCriadoAsync();
}

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

