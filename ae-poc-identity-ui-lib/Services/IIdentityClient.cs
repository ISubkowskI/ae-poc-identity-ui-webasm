using Ae.Poc.Identity.Ui.UiData;

namespace Ae.Poc.Identity.Ui.Services;

public interface IIdentityClient
{
    Task<IEnumerable<AppAccountUiItem>> LoadAccountsAsync(CancellationToken ct = default);
}
