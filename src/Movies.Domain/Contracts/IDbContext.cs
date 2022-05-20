using Microsoft.EntityFrameworkCore;
using Movies.Domain.Models;

namespace Movies.Domain.Contracts
{
    public interface IDbContext
    {
        DbSet<Movie> Movies { get; set; }
        DbSet<Rating> Ratings { get; set; }
        DbSet<User> Users { get; set; }

        //Create an initial database
        bool EnsureDbCreated();
    }
}
