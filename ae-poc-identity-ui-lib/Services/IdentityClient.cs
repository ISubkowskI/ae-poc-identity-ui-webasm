using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ae.Poc.Identity.Ui.Settings;
using Ae.Poc.Identity.Ui.Exceptions;
using Ae.Poc.Identity.Ui.Dtos;
using Ae.Poc.Identity.Ui.Extensions;
using Ae.Poc.Identity.Ui.UiData;
using System.Net.Http.Json;

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
    }

    public async Task<IEnumerable<AppAccountUiItem>> LoadAccountsAsync(CancellationToken ct = default)
    {
        _logger.LogInformation("Start {MethodName} ...", nameof(LoadAccountsAsync));
        try
        {
            // Use IdentityApiEndpoints.Accounts constant
            string requestUri = Flurl.Url.Combine(_apiOptions.ApiBasePath, IdentityApiEndpoints.Accounts);

            var res = await _httpClient.GetFromJsonAsync<IEnumerable<AppAccountDto>>(requestUri: requestUri, cancellationToken: ct);
            return res.ToUiItems();
        }
        catch (HttpRequestException e)
        {
            _logger.LogError(e, "Error in {MethodName} ...", nameof(LoadAccountsAsync));
            throw new IdentityApiException($"Failed to load accounts: {e.Message}", e);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in {MethodName} ...", nameof(LoadAccountsAsync));
            throw;
        }
    }
}
