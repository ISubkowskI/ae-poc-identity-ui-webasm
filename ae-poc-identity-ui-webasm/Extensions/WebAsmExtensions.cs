using Ae.Poc.Identity.Ui.Profiles;
using Ae.Poc.Identity.Ui.Services;
using Ae.Poc.Identity.Ui.Settings;

namespace Ae.Poc.Identity.Ui.Extensions;

public static class WebAsmExtensions
{
    public static IServiceCollection AddAppConfiguration(this IServiceCollection services,
         IConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(config);

        services
            .Configure<AppOptions>(config.GetSection(AppOptions.App))
            .Configure<IdentityStorageApiOptions>(config.GetSection(IdentityStorageApiOptions.IdentityStorageApi))
            .Configure<IdentityApiOptions>(config.GetSection(IdentityApiOptions.IdentityApi));

        return services;
    }

    public static IServiceCollection AddAppMapper(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddAutoMapper(m =>
        {
            m.AddProfile<UiDataProfile>();
        });
        return services;
    }

    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddHttpClient<IIdentityStorageClient, IdentityStorageClient>();
        services.AddHttpClient<IIdentityClient, IdentityClient>();

        return services;
    }
}
