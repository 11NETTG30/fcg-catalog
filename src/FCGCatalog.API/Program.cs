using DotNetEnv;
using FCGCatalog.Infrastructure.Configurations;
using FCGCatalog.IoC;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
