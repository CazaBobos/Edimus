using Microsoft.Extensions.DependencyInjection;
using Tags.Core.Abstractions;
using Tags.Infrastructure.Persistence;

namespace Tags.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTagsInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITagsRepository, TagsRepository>();
        return services;
    }
}
