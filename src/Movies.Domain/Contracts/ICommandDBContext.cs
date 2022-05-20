namespace Movies.Domain.Contracts
{
    public interface ICommandDBContext : IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
