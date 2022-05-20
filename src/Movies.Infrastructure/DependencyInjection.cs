using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Commands.Configuration;
using Movies.Domain.Contracts;
using Movies.Infrastructure.Configuration;
using Movies.Infrastructure.Persistance;

namespace Movies.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfigurationManager, AppSettingsConfigurationManager>();

            services.AddDbContext<CommandDbContext>(options => options.UseSqlServer(new AppSettingsConfigurationManager(configuration).DBConnectionString), ServiceLifetime.Transient);

            services.AddDbContext<QueryDbContext>(options => options.UseSqlServer(new AppSettingsConfigurationManager(configuration).DBConnectionString), ServiceLifetime.Transient);

            services.AddTransient<ICommandDBContext, CommandDbContext>();
            services.AddTransient<IQueryDbContext, QueryDbContext>();

            var serviceProvider = services.BuildServiceProvider();
            var commandContext = serviceProvider.GetService<ICommandDBContext>();
            commandContext.EnsureDbCreated();

            return services;
        }
    }
}
