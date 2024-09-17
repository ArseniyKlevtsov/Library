using FluentValidation;
using FluentValidation.AspNetCore;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces.UseCases;
using Library.Application.Interfaces.UseCases.Auth;
using Library.Application.Interfaces.UseCases.Authors;
using Library.Application.Mapping;
using Library.Application.Services;
using Library.Application.UseCases.Auth;
using Library.Application.UseCases.AuthorsUseCases;
using Library.Application.Validators.AuthValidators;
using Library.Domain.Entities;
using Library.Infrastructure;
using Library.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Library.WebApi.Extentions;

public static class ServiceRegistor
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

        return services;
    }
    private static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IUserService, UserService>();

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
        return services;
    }
}
