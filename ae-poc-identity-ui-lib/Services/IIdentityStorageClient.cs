using Ae.Poc.Identity.Ui.UiData;

namespace Ae.Poc.Identity.Ui.Services;

public interface IIdentityStorageClient
{
    Task<IEnumerable<AppAccountUiItem>> LoadAccountsAsync(CancellationToken ct = default);

    Task<IEnumerable<AppClaimUiItem>> LoadClaimsAsync(CancellationToken ct = default);

    Task<AppClaimUiItem> LoadClaimDetailsAsync(string claimId, CancellationToken ct = default);

    Task<AppClaimUiItem> DeleteClaimAsync(string claimId, CancellationToken ct = default);

    Task<AppClaimUiItem> CreateClaimAsync(AppClaimUiItem appClaimUiItem, CancellationToken ct = default);

    Task<AppClaimUiItem> UpdateClaimAsync(string claimId, AppClaimUiItem appClaimUiItem, CancellationToken ct = default);
}
