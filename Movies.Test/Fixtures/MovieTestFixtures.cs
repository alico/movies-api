using Movies.Application.Movie.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Test.Fixtures
{
    public static class MovieTestFixtures
    {
        public static List<MovieListItemDto> GetMovieList() => new()
        {
            new() 
            {
                Id = Guid.NewGuid(),
                AverageRating = 3,
                RunningTime = 120,
                Title = "Titanic",
                YearOfRelease = 1997
            }
        };
       
    }
}
