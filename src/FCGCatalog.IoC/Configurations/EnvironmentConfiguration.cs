using DotNetEnv;
using Microsoft.Extensions.Configuration;

namespace FCGCatalog.IoC.Configurations;

public static class EnvironmentConfiguration
{
	internal static void ConfigureEnvironment(this IConfigurationManager configuration)
	{
		Env.Load();
		configuration.AddEnvironmentVariables();
	}
}