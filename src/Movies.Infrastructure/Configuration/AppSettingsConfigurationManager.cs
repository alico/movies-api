using Microsoft.Extensions.Configuration;
using Movies.Application.Commands.Configuration;
using Movies.Domain.Enums;

namespace Movies.Infrastructure.Configuration
{
    public class AppSettingsConfigurationManager : IConfigurationManager
    {
        private readonly IConfiguration _configuration;

        public AppSettingsConfigurationManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public EnvironmentType EnvironmentType => (EnvironmentType)_configuration.GetValue<int>("ApplicationSettings:Environment");

        public string DBConnectionString => _configuration.GetValue<string>("ApplicationSettings:DBConnectionString");

    }
}
