using Ae.Poc.Identity.Ui.Dtos;
using Ae.Poc.Identity.Ui.UiData;

namespace Ae.Poc.Identity.Ui.Extensions;

public static class MapperExtensions
{
    public static AppClaimUiItem ToUiItem(this AppClaimDto dto)
    {
        if (dto == null) return null!;
        return new AppClaimUiItem
        {
            Id = dto.Id,
            Type = dto.Type,
            Value = dto.Value,
            ValueType = dto.ValueType,
            DisplayText = dto.DisplayText,
            Properties = dto.Properties,
            Description = dto.Description
        };
    }

    public static AppClaimDto ToDto(this AppClaimUiItem item)
    {
        if (item == null) return null!;
        return new AppClaimDto
        {
            Id = item.Id,
            Type = item.Type,
            Value = item.Value,
            ValueType = item.ValueType,
            DisplayText = item.DisplayText,
            Properties = item.Properties,
            Description = item.Description
        };
    }

    public static IEnumerable<AppClaimUiItem> ToUiItems(this IEnumerable<AppClaimDto> dtos)
    {
        if (dtos == null) return Enumerable.Empty<AppClaimUiItem>();
        return dtos.Select(d => d.ToUiItem());
    }

    public static AppAccountUiItem ToUiItem(this AppAccountDto dto)
    {
        if (dto == null) return null!;
        return new AppAccountUiItem
        {
            Id = dto.Id,
            EmailAddress = dto.EmailAddress,
            DisplayName = dto.DisplayName,
            Description = dto.Description,
            IsLocked = dto.IsLocked,
            CreatedAtUtc = dto.CreatedAtUtc,
            EmploymentDate = dto.EmploymentDate,
            EmploymentExpiredDate = dto.EmploymentExpiredDate,
            LastLoginUtc = dto.LastLoginUtc,
            LastPasswordChangeUtc = dto.LastPasswordChangeUtc,
            PasswordExpiredOnUtc = dto.PasswordExpiredOnUtc,
            EmailVerifiedOnUtc = dto.EmailVerifiedOnUtc
        };
    }

    public static IEnumerable<AppAccountUiItem> ToUiItems(this IEnumerable<AppAccountDto> dtos)
    {
        if (dtos == null) return Enumerable.Empty<AppAccountUiItem>();
        return dtos.Select(d => d.ToUiItem());
    }
}
