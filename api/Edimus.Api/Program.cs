using Categories.Extensions;
using Companies.Extensions;
using Edimus.Api.Extensions;
using Edimus.Api.FeatureProviders;
using Edimus.Api.Middleware;
using Identity.Extensions;
using Ingredients.Extensions;
using Microsoft.Extensions.Hosting.WindowsServices;
using NextjsStaticHosting.AspNetCore;
using Products.Extensions;
using Sectors.Extensions;
using Shared.Infrastructure.Extensions;
using Tables.Extensions;
using Tables.Hubs;
using Users.Extensions;

var options = new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default,
};

var builder = WebApplication.CreateBuilder(options);

// Configures Web API to be hosted as Windows Service
builder.Host.UseWindowsService();

// Add services to the container.
builder.Services.AddControllers()
    .ConfigureApplicationPartManager(manager =>
    {
        manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerWithAuth();
builder.Services.AddJWTAuthentication(builder.Environment);
builder.Services.AddGlobalRateLimiter();

builder.Services.AddSignalR();
builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
builder.Services.AddDatabaseContext();
builder.Services.AddJwtService();
builder.Services.AddMailService();

builder.Services.AddCategories();
builder.Services.AddCompanies();
builder.Services.AddIngredients();
builder.Services.AddProducts();
builder.Services.AddSectors();
builder.Services.AddTables();
builder.Services.AddUsers();
builder.Services.AddIdentity();

builder.Services.AddNextjsStaticHosting(options => options.RootPath = "StaticFiles");

var app = builder.Build();

app.MapNextjsStaticHtmls(); // <= Serves Static HTMLs
app.UseNextjsStaticHosting(); // <= For other required files (i.e. css and js files)

app.UseSwagger();
app.UseStaticFiles();
app.UseSwaggerUI(options => options.InjectStylesheet("/swagger-dark.css"));

app.UseCors(options =>
{
    options.SetIsOriginAllowed(_ => true);
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowCredentials();
});

if (app.Environment.IsDevelopment()) app.UseHttpsRedirection();

app.UseRateLimiter();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<ParamBracketRemoverMiddleware>();

app.UseAuthentication(); //<= JWT Auth

app.UseAuthorization();

app.MapControllers();
app.MapHub<TablesHub>("/hubs/tables");

app.Run();
