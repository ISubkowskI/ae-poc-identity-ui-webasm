using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ae.Poc.Identity.Ui.Settings;

namespace Ae.Poc.Identity.Ui.Services;

public sealed class IdentityClient : IIdentityClient
{
    private readonly ILogger<IdentityClient> _logger;
    private readonly HttpClient _httpClient;
    private readonly IdentityApiOptions _apiOptions;

    public IdentityClient(
        ILogger<IdentityClient> logger,
        IOptions<IdentityApiOptions> identityApiOptions,
        HttpClient httpClient)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _apiOptions = identityApiOptions?.Value ?? throw new ArgumentNullException(nameof(identityApiOptions));

        _httpClient.BaseAddress = new Uri(_apiOptions.ApiUrl);
    }
}
