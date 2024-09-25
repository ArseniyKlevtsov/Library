using Library.Application.Interfaces.UseCases.Auth;
using Library.Application.Interfaces.UseCases.Authors;
using Library.Application.Interfaces.UseCases.BookUseCases;
using Library.Application.Interfaces.UseCases.GenreUseCases;
using Library.Application.Interfaces.UseCases.OrderUseCases;
using Library.Application.Interfaces.UseCases.ProfileUseCases;
using Library.Application.UseCases.Auth;
using Library.Application.UseCases.AuthorsUseCases;
using Library.Application.UseCases.BooksUseCases;
using Library.Application.UseCases.BookUseCases;
using Library.Application.UseCases.GenresUseCases;
using Library.Application.UseCases.OrderUseCases;
using Library.Application.UseCases.ProfileUseCases;

namespace Library.WebApi.Extentions.ServiceRegistrators;

public static class UseCasesRegistrator
{
    public static IServiceCollection AddUseCases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILoginUseCase, Login>();
        services.AddScoped<IRegisterUseCase, Register>();
        services.AddScoped<IRefreshAuthorizationUseCase, RefreshAuthorization>();

        services.AddScoped<ICreateAuthor, CreateAuthor>();
        services.AddScoped<IDeleteAuthor, DeleteAuthor>();
        services.AddScoped<IGetAuthorById, GetAuthorById>();
        services.AddScoped<IUpdateAuthor, UpdateAuthor>();
        services.AddScoped<IGetAuthorsPage, GetAuthorsPage>();

        services.AddScoped<ICreateBook, CreateBook>();
        services.AddScoped<IDeleteBook, DeleteBook>();
        services.AddScoped<IUpdateBook, UpdateBook>();
        services.AddScoped<IGetBookById, GetBookById>();
        services.AddScoped<IGetBooksPage, GetBooksPage>();
        services.AddScoped<IGetBookEditInfo, GetBookEditInfo>();
        services.AddScoped<IGetBookInfo, GetBookInfo>();

        services.AddScoped<ICreateGenre, CreateGenre>();
        services.AddScoped<IGetGenreById, GetGenreById>();
        services.AddScoped<IDeleteGenre, DeleteGenre>();
        services.AddScoped<IUpdateGenre, UpdateGenre>();
        services.AddScoped<IGetGenresPage, GetGenresPage>();


        services.AddScoped<IPlaceOrder, PlaceOrder>();
        services.AddScoped<IGetUserOrders, GetUserOrders>();

        return services;
    }
}
