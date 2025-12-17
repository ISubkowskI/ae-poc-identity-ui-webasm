using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using Ae.Poc.Identity.Ui.Dtos;
using Ae.Poc.Identity.Ui.UiData;
using Ae.Poc.Identity.Ui.Settings;
using Ae.Poc.Identity.Ui.Extensions;

namespace Ae.Poc.Identity.Ui.Services;

public sealed class IdentityStorageClient : IIdentityStorageClient
{
    private readonly ILogger<IdentityStorageClient> _logger;
    private readonly HttpClient _httpClient;
    private readonly IdentityApiOptions _apiOptions;

    public IdentityStorageClient(
        ILogger<IdentityStorageClient> logger,
        IOptions<IdentityApiOptions> identityStorageApiOptions,
        HttpClient httpClient)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _apiOptions = identityStorageApiOptions?.Value ?? throw new ArgumentNullException(nameof(identityStorageApiOptions));

        _httpClient.BaseAddress = new Uri(_apiOptions.ApiUrl);
    }

    public async Task<IEnumerable<AppAccountUiItem>> LoadAccountsAsync(CancellationToken ct = default)
    {
        const string ApiEndPoint = "accounts";
        _logger.LogInformation("Start {MethodName} ...", nameof(LoadAccountsAsync));
        try
        {
            string requestUri = Flurl.Url.Combine(_apiOptions.ApiUrl, _apiOptions.ApiBasePath, ApiEndPoint);

            var res = await _httpClient.GetFromJsonAsync<IEnumerable<AppAccountDto>>(requestUri: requestUri, cancellationToken: ct);
            return res.ToUiItems();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in {MethodName} ...", nameof(LoadAccountsAsync));
            throw;
        }
    }

    public async Task<IEnumerable<AppClaimUiItem>> LoadClaimsAsync(CancellationToken ct = default)
    {
        const string ApiEndPoint = "masterdata/claims";

        _logger.LogInformation("Start {MethodName} ...", nameof(LoadClaimsAsync));

        try
        {
            string requestUri = Flurl.Url.Combine(_apiOptions.ApiBasePath, ApiEndPoint);
            var res = await _httpClient.GetFromJsonAsync<IEnumerable<AppClaimDto>>(requestUri: requestUri, cancellationToken: ct);
            return res.ToUiItems();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in {MethodName} ...", nameof(LoadClaimsAsync));
            throw;
        }
    }

    public async Task<AppClaimUiItem> LoadClaimDetailsAsync(string claimId, CancellationToken ct = default)
    {
        const string ApiEndPoint = "masterdata/claims";
        _logger.LogInformation("Start {MethodName} ...", nameof(LoadClaimDetailsAsync));

        try
        {
            string requestUri = Flurl.Url.Combine(_apiOptions.ApiBasePath, ApiEndPoint, claimId);
            var res = await _httpClient.GetFromJsonAsync<AppClaimDto>(requestUri: requestUri, cancellationToken: ct);
            return res.ToUiItem();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in {MethodName} ...", nameof(LoadClaimDetailsAsync));
            throw;
        }
    }

    public async Task<AppClaimUiItem> DeleteClaimAsync(string claimId, CancellationToken ct = default)
    {
        const string ApiEndPoint = "masterdata/claims";
        _logger.LogInformation("Start {MethodName} ...", nameof(DeleteClaimAsync));
        try
        {
            string requestUri = Flurl.Url.Combine(_apiOptions.ApiBasePath, ApiEndPoint, claimId);
            var httpResponse = await _httpClient.DeleteAsync(requestUri: requestUri, cancellationToken: ct);
            if (httpResponse.IsSuccessStatusCode)
            {
                var res = await httpResponse.Content.ReadFromJsonAsync<AppClaimDto>(cancellationToken: ct);
                return res.ToUiItem();
            }
            else
            {
                var errorMessage = await httpResponse.Content.ReadAsStringAsync(ct);
                throw new Exception($"Error deleting claim: {errorMessage}");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in {MethodName} ...", nameof(DeleteClaimAsync));
            throw;
        }
    }

    public async Task<AppClaimUiItem> CreateClaimAsync(AppClaimUiItem appClaimUiItem, CancellationToken ct = default)
    {
        const string ApiEndPoint = "masterdata/claims";
        _logger.LogInformation("Start {MethodName} ...", nameof(CreateClaimAsync));

        try
        {
            string requestUri = Flurl.Url.Combine(_apiOptions.ApiBasePath, ApiEndPoint);
            var requestData = appClaimUiItem.ToDto();
            var httpResponse = await _httpClient.PostAsJsonAsync(requestUri: requestUri, value: requestData, cancellationToken: ct);
            if (httpResponse.IsSuccessStatusCode)
            {
                var res = await httpResponse.Content.ReadFromJsonAsync<AppClaimDto>(cancellationToken: ct);
                return res.ToUiItem();
            }
            else
            {
                var errorMessage = await httpResponse.Content.ReadAsStringAsync(ct);
                throw new Exception($"Error updating claim: {errorMessage}");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in {MethodName} ...", nameof(CreateClaimAsync));
            throw;
        }
    }

    public async Task<AppClaimUiItem> UpdateClaimAsync(string claimId, AppClaimUiItem appClaimUiItem, CancellationToken ct = default)
    {
        const string ApiEndPoint = "masterdata/claims";
        _logger.LogInformation("Start {MethodName} ...", nameof(UpdateClaimAsync));
        try
        {
            string requestUri = Flurl.Url.Combine(_apiOptions.ApiBasePath, ApiEndPoint, claimId);
            var requestData = appClaimUiItem.ToDto();
            var httpResponse = await _httpClient.PatchAsJsonAsync(requestUri: requestUri, value: requestData, cancellationToken: ct);
            if (httpResponse.IsSuccessStatusCode)
            {
                var res = await httpResponse.Content.ReadFromJsonAsync<AppClaimDto>(cancellationToken: ct);
                return res.ToUiItem();
            }
            else
            {
                var errorMessage = await httpResponse.Content.ReadAsStringAsync(ct);
                throw new Exception($"Error updating claim: {errorMessage}");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in {MethodName} ...", nameof(UpdateClaimAsync));
            throw;
        }
    }
}
