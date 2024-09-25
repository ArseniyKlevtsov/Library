using FluentValidation;
using FluentValidation.AspNetCore;
using Library.Application.Interfaces.UseCases.Auth;
using Library.Application.Interfaces.UseCases.Authors;
using Library.Application.Interfaces.UseCases.BookUseCases;
using Library.Application.Interfaces.UseCases.GenreUseCases;
using Library.Application.Interfaces.UseCases.OrderUseCases;
using Library.Application.Interfaces.UseCases.ProfileUseCases;
using Library.Application.Mapping;
using Library.Application.UseCases.Auth;
using Library.Application.UseCases.AuthorsUseCases;
using Library.Application.UseCases.BooksUseCases;
using Library.Application.UseCases.BookUseCases;
using Library.Application.UseCases.GenresUseCases;
using Library.Application.UseCases.OrderUseCases;
using Library.Application.UseCases.ProfileUseCases;
using Library.Application.Validators.AuthValidators;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Services;
using Library.Infrastructure;
using Library.Infrastructure.Authentication;
using Library.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Library.WebApi.Extentions;

public static class ServiceRegistrator
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddInfrastructureServices(configuration);
        services.AddApplicationServices(configuration);
        services.AddAuhtServices(configuration);
        services.AddAnyServices(configuration);
        services.AddUseCases(configuration);
        return services;
    }

    private static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));

        services
            .AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<LibraryDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<UnitOfWork>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();


        return services;
    }
    private static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(UserProfile).Assembly);

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(RegisterRequestDtoValidator).Assembly);
        return services;
    }

    private static IServiceCollection AddAuhtServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            options.AddPolicy("User", policy => policy.RequireRole("User"));
            options.AddPolicy("AdminOrUser", policy => policy.RequireRole("Admin", "User"));
        });

        return services;
    }

    private static IServiceCollection AddAnyServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });
        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services, IConfiguration configuration)
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
