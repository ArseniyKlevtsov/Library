using FluentValidation.AspNetCore;
using FluentValidation;
using Library.Application.Mapping;
using Library.Application.Validators.AuthValidators;

namespace Library.WebApi.Extentions.ServiceRegistrators;

public static class ApplicationRegistrator
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(UserProfile).Assembly);

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(RegisterRequestDtoValidator).Assembly);
        return services;
    }
}
