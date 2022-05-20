using Movies.Application.Movie.Queries;
using System.Linq.Expressions;

namespace Movies.Application.Services;

public interface IMovieListExpressionBuilderQueryBuilder
{

    IMovieListExpressionBuilderQueryBuilder Init(ListMoviesQuery query);

    IMovieListExpressionBuilderQueryBuilder AddTitle();

    IMovieListExpressionBuilderQueryBuilder AddYearOfRelease();

    IMovieListExpressionBuilderQueryBuilder AddGenres();

    Expression<Func<Domain.Models.Movie, bool>> Build();
}
