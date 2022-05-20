using Movies.Application.Movie.Queries;
using System.Linq.Expressions;

namespace Movies.Application.Services;
public class MovieListExpressionBuilderQueryBuilder : IMovieListExpressionBuilderQueryBuilder
{
    private Expression<Func<Domain.Models.Movie, bool>> _expression;
    private ListMoviesQuery _query;

    public IMovieListExpressionBuilderQueryBuilder AddGenres()
    {
        if (_query?.Genres?.Count > 0)
            _expression = _expression.And(x => x.Genres.Any(y => _query.Genres.Contains(y.GenreId)));

        return this;
    }

    public IMovieListExpressionBuilderQueryBuilder AddTitle()
    {
        if (!string.IsNullOrEmpty(_query.Title))
            _expression = _expression.And(x => x.Title.Contains(_query.Title));

        return this;
    }

    public IMovieListExpressionBuilderQueryBuilder AddYearOfRelease()
    {
        if (_query.YearOfRelease > 0)
            _expression = _expression.And(x => x.YearOfRelease == _query.YearOfRelease);

        return this;
    }

    public Expression<Func<Domain.Models.Movie, bool>> Build()
    {
        return _expression;
    }

    public IMovieListExpressionBuilderQueryBuilder Init(ListMoviesQuery query)
    {
        _expression = ExpressionBuilder.True<Domain.Models.Movie>();
        _query = query;
        return this;
    }
}
