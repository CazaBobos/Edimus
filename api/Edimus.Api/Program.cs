using Microsoft.Extensions.Hosting.WindowsServices;
using Edimus.Api.Extensions;
using Edimus.Api.FeatureProviders;
using Edimus.Api.Middleware;
using NextjsStaticHosting.AspNetCore;
using Shared.Infrastructure.Extensions;
using Categories.Extensions;
using Companies.Extensions;
using Ingredients.Extensions;
using Products.Extensions;
using Sectors.Extensions;
using Tables.Extensions;
using Users.Extensions;
using Identity.Extensions;

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
builder.Services.AddJWTAuthentication();

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
app.UseSwaggerUI();

app.UseCors(options =>
{
    options.WithOrigins("*");
    options.WithHeaders("*");
    options.WithMethods("*");
});

if (app.Environment.IsDevelopment()) app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<ParamBracketRemoverMiddleware>();

app.UseAuthentication(); //<= JWT Auth

app.UseAuthorization();

app.MapControllers();

app.Run();
