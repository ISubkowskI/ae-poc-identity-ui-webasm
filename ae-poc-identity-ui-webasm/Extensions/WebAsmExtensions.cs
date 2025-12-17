using Ae.Poc.Identity.Ui.Services;
using Ae.Poc.Identity.Ui.Settings;
using Microsoft.Extensions.Options;

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
            .Configure<IdentityApiOptions>(config.GetSection(IdentityApiOptions.IdentityApi));

        return services;
    }

    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddHttpClient<IIdentityStorageClient, IdentityStorageClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<IdentityApiOptions>>().Value;
            if (!string.IsNullOrEmpty(options.ApiUrl))
            {
                client.BaseAddress = new Uri(options.ApiUrl);
            }
        });

        services.AddHttpClient<IIdentityClient, IdentityClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<IdentityApiOptions>>().Value;
             if (!string.IsNullOrEmpty(options.ApiUrl))
            {
                client.BaseAddress = new Uri(options.ApiUrl);
            }
        });

        return services;
    }
}
