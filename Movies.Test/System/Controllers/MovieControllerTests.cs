using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Movies.API.Controllers;
using Movies.Application.Movie.Queries;
using Movies.Test.Fixtures;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Movies.Test.System.Controllers;
public class MovieControllerTests
{
    [Fact]
    public async Task Get_WhenIsCalled_ReturnsOk()
    {
        //Arrange
        var loggerMock = new Mock<ILogger<MovieController>>();
        var mediatrMock = new Mock<IMediator>();
        var request = new ListMoviesQuery();
        var sut = new MovieController(loggerMock.Object, mediatrMock.Object);
        var movies = MovieTestFixtures.GetMovieList();

        mediatrMock.Setup(x => x.Send(request, It.IsAny<CancellationToken>())).ReturnsAsync(movies);

        //Act
        var response = await sut.Get(It.IsAny<CancellationToken>(), request);

        //Assert
        response.Should().NotBeNull();
        response.Result.Should().BeOfType<OkObjectResult>();
        var result = (OkObjectResult)response.Result;
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<List<MovieListItemDto>>();
        result.StatusCode.Should().Be(200);
        var returnedMovies = (List<MovieListItemDto>)result.Value;
        returnedMovies.Count.Should().Be(movies.Count);

        for (int i = 0; i < movies.Count; i++)
        {
            returnedMovies[i].Id.Should().Be(movies[i].Id);
            returnedMovies[i].Title.Should().Be(movies[i].Title);
            returnedMovies[i].YearOfRelease.Should().Be(movies[i].YearOfRelease);
            returnedMovies[i].RunningTime.Should().Be(movies[i].RunningTime);
            returnedMovies[i].AverageRating.Should().Be(movies[i].AverageRating);
        }
    }

    [Fact]
    public async Task Get_WhenMovieNotFound_ReturnsNotFound()
    {
        //Arrange
        var loggerMock = new Mock<ILogger<MovieController>>();
        var mediatrMock = new Mock<IMediator>();
        var request = new ListMoviesQuery();
        var sut = new MovieController(loggerMock.Object, mediatrMock.Object);

        mediatrMock.Setup(x => x.Send(request, It.IsAny<CancellationToken>())).ReturnsAsync(new List<MovieListItemDto>());

        //Act
        var response = await sut.Get(It.IsAny<CancellationToken>(), request);

        //Assert
        response.Should().NotBeNull();
        response.Result.Should().BeOfType<NotFoundResult>();
        var result = (NotFoundResult)response.Result;
        result.StatusCode.Should().Be(404);
    }
}