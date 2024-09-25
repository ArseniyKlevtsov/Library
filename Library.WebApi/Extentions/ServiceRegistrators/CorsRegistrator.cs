namespace Library.WebApi.Extentions.ServiceRegistrators;

public static class CorsRegistrator
{
    public static IServiceCollection AddCorsServices(this IServiceCollection services, IConfiguration configuration)
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
}
