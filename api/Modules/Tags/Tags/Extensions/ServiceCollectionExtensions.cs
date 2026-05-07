using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Extensions;
using Tags.Controllers;
using Tags.Core.Extensions;
using Tags.Infrastructure.Extensions;

namespace Tags.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTags(this IServiceCollection services)
    {
        services.AddTagsCore();
        services.AddTagsInfrastructure();
        services.AddSwaggerComments<TagsController>();
        return services;
    }
}
