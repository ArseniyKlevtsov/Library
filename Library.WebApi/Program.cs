
using Library.WebApi.Extentions;
using Library.WebApi.Extentions.ServiceRegistrators;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();
app.UseMiddlewares(app.Environment.IsDevelopment());

app.MapControllers();
app.Run();
