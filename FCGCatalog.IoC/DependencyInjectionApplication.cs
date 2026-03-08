using System.Globalization;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace FCGCatalog.IoC;
public static class DependencyInjectionApplication
{
    extension(IServiceCollection services)
    {
        internal void AddApplication()
        {
            services.AddFluentValidation();

            
        }
        private void AddFluentValidation()
        {
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
            services.AddFluentValidationClientsideAdapters();
            services.AddFluentValidationAutoValidation();

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-Br");
            ValidatorOptions.Global.LanguageManager = new CustomLanguageManager();
        }
    }
}