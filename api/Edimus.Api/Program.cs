using Serilog;
using Categories.Extensions;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence;
using Companies.Extensions;
using Edimus.Api.Extensions;
using Layouts.Extensions;
using Premises.Extensions;
using Edimus.Api.FeatureProviders;
using Edimus.Api.Middleware;
using Identity.Extensions;
using Ingredients.Extensions;
using Microsoft.Extensions.Hosting.WindowsServices;
using NextjsStaticHosting.AspNetCore;
using Products.Extensions;
using Sectors.Extensions;
using Shared.Infrastructure.Extensions;
using AuditLogs.Extensions;
using Statistics.Extensions;
using Tables.Extensions;
using Tables.Hubs;
using Tags.Extensions;
using Users.Extensions;

DotNetEnv.Env.Load(Path.Combine(AppContext.BaseDirectory, ".env"));

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var options = new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default,
};

var builder = WebApplication.CreateBuilder(options);

builder.AddSerilog();

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
builder.Services.AddLayouts();
builder.Services.AddPremises();
builder.Services.AddProducts();
builder.Services.AddSectors();
builder.Services.AddAuditLogs();
builder.Services.AddStatistics();
builder.Services.AddTables();
builder.Services.AddTags();
builder.Services.AddUsers();
builder.Services.AddIdentity();

builder.Services.AddNextjsStaticHosting(options => options.RootPath = "StaticFiles");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    dbContext.Database.Migrate();
}

app.MapNextjsStaticHtmls(); // <= Serves Static HTMLs
app.UseNextjsStaticHosting(); // <= For other required files (i.e. css and js files)

app.UseSwagger();
app.UseStaticFiles();
app.UseSwaggerUI(options => options.InjectStylesheet("/swagger-dark.css"));

app.UseCors(options =>
{
    if (app.Environment.IsDevelopment())
        options.SetIsOriginAllowed(_ => true);
    else
        options.WithOrigins(builder.Configuration["Email:FrontendUrl"]!);

    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowCredentials();
});

if (app.Environment.IsDevelopment()) app.UseHttpsRedirection();

app.UseSerilogRequestLogging();
app.UseRateLimiter();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<ParamBracketRemoverMiddleware>();

app.UseAuthentication(); //<= JWT Auth
app.UseMiddleware<CompanyContextMiddleware>();

app.UseAuthorization();

app.MapControllers();
app.MapHub<TablesHub>("/hubs/tables");

app.Run();
