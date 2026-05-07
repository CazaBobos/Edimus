using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Extensions;
using Tags.Core.Abstractions;

namespace Tags.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTagsCore(this IServiceCollection services)
    {
        services.AddMapster<ITagsRepository>();
        return services;
    }
}
