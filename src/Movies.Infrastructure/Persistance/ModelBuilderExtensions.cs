using Microsoft.EntityFrameworkCore;
using Movies.Domain.Models;

namespace Movies.Infrastructure.Persistance;

public static class ModelBuilderExtensions
{
    private static List<Movie> _movies = new()
    {
        new()
        {
            Id = new Guid("214287b9-3a6a-409c-b7f5-2e38aae24e7d"),
            Title = "Titanic",
            YearOfRelease = 1997,
            RunningTime = 194
        },
        new()
        {
            Id = new Guid("7d72a734-079a-4d46-9a2f-52be2f029947"),
            Title = "The Game",
            YearOfRelease = 1997,
            RunningTime = 129

        },
        new()
        {
            Id = new Guid("525ea61d-7c95-4288-85cc-1abbd8b30311"),
            Title = "The Matrix",
            YearOfRelease = 1999,
            RunningTime = 180

        },
        new()
        {
            Id = new Guid("d248385a-adc8-433b-9b64-36c577410890"),
            Title = "The Lord of the Rings: The Two Towers",
            YearOfRelease = 2002,
            RunningTime = 240

        },
        new()
        {
            Id = new Guid("dd943f1d-b52e-480b-ad8c-f83027977db6"),
            Title = "Harry Potter and the Sorcerer's Stone",
            YearOfRelease = 2001,
            RunningTime = 177

        },
        new()
        {
            Id = new Guid("6a2de667-93b6-496f-8660-a68ae1bb0614"),
            Title = "Fight Club",
            YearOfRelease = 1999,
            RunningTime = 154

        },
        new()
        {
            Id = new Guid("a5ebe0d1-d25f-4cf8-952d-b11289897814"),
            Title = "The Sixth Sense",
            YearOfRelease = 1999,
            RunningTime = 130

        },
        new()
        {
            Id = new Guid("cdd3857f-5dd4-4d56-808b-122d20ceffd1"),
            Title = "City of Angels",
            YearOfRelease = 1999,
            RunningTime = 125

        },
        new()
        {
            Id = new Guid("0cccdcff-9698-4f3b-8620-c93760f1bf72"),
            Title = "Se7en",
            YearOfRelease = 1995,
            RunningTime = 140

        },
        new()
        {
            Id = new Guid("ad781eb5-d144-4155-af5c-740ab066404b"),
            Title = "The Usual Suspects",
            YearOfRelease = 1995,
            RunningTime = 194

        },
        new()
        {
            Id = new Guid("69c4d9df-db64-4831-950e-da92dc7501cf"),
            Title = "The Silence of the Lambs",
            YearOfRelease = 1991,
            RunningTime = 115
        }
    };
    private static List<Genre> _genres = new()
    {
        new()
        {
            Id = new Guid("cb44773a-8596-4d2c-b4c2-e7894d3f6427"),
            Name = "Drama",
        },
        new()
        {
            Id = new Guid("d4a3731b-128b-43b7-8265-313f5d54e9ee"),
            Name = "Romance"
        },
        new()
        {
            Id = new Guid("d287dd53-5258-4dc0-a0d7-b58f486c914c"),
            Name = "Action"
        },
        new()
        {
            Id = new Guid("fa97fa89-af1a-4421-a784-50f68f9284d4"),
            Name = "Adventure"
        },
        new()
        {
            Id = new Guid("48550a4c-adde-452f-998c-fab62fd67680"),
            Name = "Science Fiction"
        },
        new()
        {
            Id = new Guid("47cee80f-c70a-42a9-86cd-1485e322e386"),
            Name = "Fantasy"
        },
        new()
        {
            Id = new Guid("8ec1d526-bdc2-44c6-9bed-98bca9c69ddb"),
            Name = "Psychological thriller"
        }


    };
    private static List<User> _users = new()
    {
        new()
        {
            Id = new Guid("72d8d818-a181-4abf-8333-e161bde1db28"),
            Name = "Bob",
        },
        new()
        {
            Id = new Guid("6ca5f829-a543-4d4b-9e59-631f37de72d8"),
            Name = "Jon",
        },
        new()
        {
            Id = new Guid("d06eaf57-6f76-4917-b14f-4fe490f94f4f"),
            Name = "Philip",
        },
        new()
        {
            Id = new Guid("99442eb7-d26e-455d-b461-4fdca23bf4ed"),
            Name = "Luke",
        },
        new()
        {
            Id = new Guid("ec84784b-4de2-4b5b-b37d-c09b6676db3f"),
            Name = "Mark",
        },
        new()
        {
            Id = new Guid("756c392f-0fe9-4025-8333-f1579273646a"),
            Name = "Mike",
        },
    };
    private static List<Rating> _ratings = CreateRatingSeedData();

    public static void Seed(this ModelBuilder modelBuilder)
    {
        GenreSeedData(modelBuilder);
        MovieSeedData(modelBuilder);
        MovieGenreSeedData(modelBuilder);
        UserSeedData(modelBuilder);
        RatingSeedData(modelBuilder);
    }

    private static void MovieSeedData(ModelBuilder modelBuilder)
    {
        foreach (var movie in _movies)
        {
            movie.RatingCount = _ratings.Where(x => x.MovieId == movie.Id).Count();
            movie.TotalRatingScore = _ratings.Where(x => x.MovieId == movie.Id).Select(x => x.Score).Sum();
            //movie.AverageRating = movie.TotalRatingScore / movie.RatingCount;
        }

        modelBuilder.Entity<Movie>().HasData(_movies);
    }

    private static void UserSeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(_users);
    }

    private static void RatingSeedData(ModelBuilder modelBuilder)
    {
        var rnd = new Random();
        List<Rating> ratings = new();

        for (int i = 0; i < 100; i++)
        {
            var rate = new Rating()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[rnd.Next(0, _movies.Count)].Id,
                UserId = _users[rnd.Next(0, _users.Count)].Id,
                Score = rnd.Next(1, 5),
            };

            if (!ratings.Any(x => x.MovieId == rate.MovieId && x.UserId == rate.UserId))
            {
                ratings.Add(rate);
            }
        }

        modelBuilder.Entity<Rating>().HasData(ratings);
    }

    private static List<Rating> CreateRatingSeedData()
    {
        var rnd = new Random();
        List<Rating> ratings = new();

        for (int i = 0; i < 100; i++)
        {
            ratings.Add(new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[rnd.Next(0, _movies.Count)].Id,
                UserId = _users[rnd.Next(0, _users.Count)].Id,
                Score = rnd.Next(1, 5),
            });
        }

        return ratings;
    }

    private static void GenreSeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(_genres);
    }

    private static void MovieGenreSeedData(ModelBuilder modelBuilder)
    {
        List<MovieGenre> genres = new()
        {
            //Titanic
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[0].Id,
                GenreId = _genres[0].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[0].Id,
                GenreId = _genres[1].Id,
            },

            //The Game
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[1].Id,
                GenreId = _genres[2].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[1].Id,
                GenreId = _genres[3].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[1].Id,
                GenreId = _genres[0].Id,
            },

            //The Matrix
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[2].Id,
                GenreId = _genres[2].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[2].Id,
                GenreId = _genres[4].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[2].Id,
                GenreId = _genres[5].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[2].Id,
                GenreId = _genres[3].Id,
            },

            //The Lord of the Rings: The Two Towers
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[3].Id,
                GenreId = _genres[0].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[3].Id,
                GenreId = _genres[2].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[3].Id,
                GenreId = _genres[3].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[3].Id,
                GenreId = _genres[5].Id,
            },

            //Harry Potter and the Sorcerer's Stone
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[4].Id,
                GenreId = _genres[5].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[4].Id,
                GenreId = _genres[3].Id,
            },
            //Fight Club
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[5].Id,
                GenreId = _genres[0].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[5].Id,
                GenreId = _genres[2].Id,
            },
            //The Sixth Sense
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[6].Id,
                GenreId = _genres[6].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[6].Id,
                GenreId = _genres[0].Id,
            },
            //City of Angels
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[7].Id,
                GenreId = _genres[1].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[7].Id,
                GenreId = _genres[5].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[7].Id,
                GenreId = _genres[0].Id,
            },
            //Se7en
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[8].Id,
                GenreId = _genres[0].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[8].Id,
                GenreId = _genres[6].Id,
            },
            //The Usual Suspects
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[9].Id,
                GenreId = _genres[0].Id,
            },

            //The Silence of the Lambs
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[10].Id,
                GenreId = _genres[0].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = _movies[10].Id,
                GenreId = _genres[6].Id,
            },
        };

        modelBuilder.Entity<MovieGenre>().HasData(genres);
    }
}
