using Microsoft.EntityFrameworkCore;
using Movies.Application.Commands.Configuration;
using Movies.Domain.Contracts;

namespace Movies.Infrastructure.Persistance
{
    public class QueryDbContext : BaseDbContext, IQueryDbContext
    {
        public QueryDbContext(IConfigurationManager configurationManager) : base(configurationManager)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configurationManager.DBConnectionString).EnableSensitiveDataLogging();
        }
    }
}
