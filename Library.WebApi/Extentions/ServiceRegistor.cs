using Library.Domain.Entities;
using Library.Infrastructure;
using Library.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.WebApi.Extentions;

public static class ServiceRegistor
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddInfrastructure(configuration);

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));

        services
            .AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<LibraryDbContext>()
            .AddDefaultTokenProviders();

        services
            .AddScoped<UnitOfWork>();

        return services;
    }
}
