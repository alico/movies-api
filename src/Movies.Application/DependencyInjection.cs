using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Common.Behaviours;
using Movies.Application.Services;
using System.Reflection;

namespace Movies.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IMovieListExpressionBuilderQueryBuilder, MovieListExpressionBuilderQueryBuilder>();

            return services;
        }
    }
}
