using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Extensions;
using Shared.Core.Persistence;

namespace Tags.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTagsCore(this IServiceCollection services)
    {
        services.AddMapster<ITagsRepository>();
        return services;
    }
}
