using Ae.Poc.Identity.Ui.Dtos;
using Ae.Poc.Identity.Ui.Extensions;
using Ae.Poc.Identity.Ui.UiData;
using FluentAssertions;

namespace Ae.Poc.Identity.Ui.Tests;

public class MapperExtensionsTests
{
    [Fact]
    public void ToUiItem_AppClaimDto_ShouldMapCorrectly()
    {
        // Arrange
        var dto = new AppClaimDto
        {
            Id = Guid.NewGuid(),
            Type = "Role",
            Value = "Admin",
            ValueType = "string"
        };

        // Act
        var result = dto.ToUiItem();

        // Assert
        result.Should().BeEquivalentTo(dto);
    }

    [Fact]
    public void ToDto_AppClaimUiItem_ShouldMapCorrectly()
    {
        // Arrange
        var item = new AppClaimUiItem
        {
            Id = Guid.NewGuid(),
            Type = "Role",
            Value = "Admin",
            ValueType = "string"
        };

        // Act
        var result = item.ToDto();

        // Assert
        result.Should().BeEquivalentTo(item);
    }
}
