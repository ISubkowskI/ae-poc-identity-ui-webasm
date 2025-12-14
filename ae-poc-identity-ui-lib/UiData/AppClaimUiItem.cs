using System.ComponentModel.DataAnnotations;

namespace Ae.Poc.Identity.Ui.UiData;

public sealed class AppClaimUiItem
{
    public Guid Id { get; set; } = Guid.Empty;

    [Required(ErrorMessage = "The Type field is required")]
    public string Type { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Value field is required")]
    public string Value { get; set; } = string.Empty;

    [Required(ErrorMessage = "Value type is mandatory")]
    public string ValueType { get; set; } = string.Empty;

    [Required(ErrorMessage = "Text is mandatory")]
    public string DisplayText { get; set; } = string.Empty;

    public IDictionary<string, string>? Properties { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }
}
