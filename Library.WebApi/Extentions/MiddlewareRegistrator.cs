using OnlineShop.WebApi.Middleware;

namespace Library.WebApi.Extentions;

public static class MiddlewareRegistrator
{
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app, bool isDevelopment)
    {
        if (isDevelopment)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors();
        app.UseAuthorization();
        app.UseMiddleware<GlobalExceptionHandler>();

        return app;
    }
}
