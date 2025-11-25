using Asp.Versioning;
using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System.Linq;

namespace WebApi_BestPractices.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                );
            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureSqlContext(
            this IServiceCollection services,
            IConfiguration configuration
        ) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(
                    configuration.GetConnectionString("sqlConnection"),
                    b => b.MigrationsAssembly("Repository")
                )
            );

        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void AddCustomMediaType(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var newtonsoftJsnoOutputFormatter = config
                    .OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>()
                    ?.FirstOrDefault();

                if (newtonsoftJsnoOutputFormatter is not null)
                {
                    newtonsoftJsnoOutputFormatter.SupportedMediaTypes.Add("application/vnd.hateoas+json");
                    newtonsoftJsnoOutputFormatter.SupportedMediaTypes.Add(
                        "application/vnd.apiroot+json"
                    );
                }

                var xmlOutputFormatter = config
                    .OutputFormatters.OfType<XmlDataContractSerializerInputFormatter>()
                    .FirstOrDefault();

                if (xmlOutputFormatter is not null)
                {
                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.apiroot+xml");
                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.hateoas+xml");
                }
            });
        }
    }
}
