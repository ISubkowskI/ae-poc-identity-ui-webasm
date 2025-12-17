using Ae.Poc.Identity.Ui.Dtos;
using Ae.Poc.Identity.Ui.Exceptions;
using Ae.Poc.Identity.Ui.Services;
using Ae.Poc.Identity.Ui.Settings;
using Ae.Poc.Identity.Ui.UiData;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RichardSzalay.MockHttp;
using System.Net;
using System.Text.Json;

namespace Ae.Poc.Identity.Ui.Tests;

public class IdentityStorageClientTests
{
    private readonly Mock<ILogger<IdentityStorageClient>> _loggerMock;
    private readonly MockHttpMessageHandler _httpMock;
    private readonly IdentityApiOptions _options;

    public IdentityStorageClientTests()
    {
        _loggerMock = new Mock<ILogger<IdentityStorageClient>>();
        _httpMock = new MockHttpMessageHandler();
        _options = new IdentityApiOptions
        {
            ApiUrl = "http://localhost:5000",
            ApiBasePath = "/api/v1"
        };
    }

    private IdentityStorageClient CreateClient()
    {
        var httpClient = _httpMock.ToHttpClient();
        httpClient.BaseAddress = new Uri(_options.ApiUrl);

        var optionsMock = new Mock<IOptions<IdentityApiOptions>>();
        optionsMock.Setup(x => x.Value).Returns(_options);

        // IdentityStorageClient constructor might require IIdentityStorageApiOptions but in our code it uses IdentityApiOptions 
        // based on previous file views. Let's start with this.
        return new IdentityStorageClient(_loggerMock.Object, optionsMock.Object, httpClient);
    }

    [Fact]
    public async Task LoadClaimsAsync_ShouldReturnClaims_WhenApiReturnsSuccess()
    {
        // Arrange
        var claims = new[] { new AppClaimDto { Id = Guid.NewGuid(), Type = "Role", Value = "User" } };
        var json = JsonSerializer.Serialize(claims);

        _httpMock.When("http://localhost:5000/api/v1/masterdata/claims")
                 .Respond("application/json", json);

        var client = CreateClient();

        // Act
        var result = await client.LoadClaimsAsync();

        // Assert
        result.Should().HaveCount(1);
        result.First().Value.Should().Be("User");
    }

    [Fact]
    public async Task CreateClaimAsync_ShouldReturnCreatedClaim_WhenApiReturnsSuccess()
    {
        // Arrange
        var input = new AppClaimUiItem { Type = "Role", Value = "Admin" };
        var outputDto = new AppClaimDto { Id = Guid.NewGuid(), Type = "Role", Value = "Admin" };
        
        _httpMock.When(HttpMethod.Post, "http://localhost:5000/api/v1/masterdata/claims")
                 .Respond("application/json", JsonSerializer.Serialize(outputDto));

        var client = CreateClient();

        // Act
        var result = await client.CreateClaimAsync(input);

        // Assert
        result.Value.Should().Be("Admin");
    }

    [Fact]
    public async Task CreateClaimAsync_ShouldThrowIdentityApiException_WhenApiReturnsError()
    {
        // Arrange
        var input = new AppClaimUiItem { Type = "Role", Value = "Admin" };

        _httpMock.When(HttpMethod.Post, "http://localhost:5000/api/v1/masterdata/claims")
                 .Respond(HttpStatusCode.BadRequest, "application/json", "Invalid data");

        var client = CreateClient();

        // Act
        var act = async () => await client.CreateClaimAsync(input);

        // Assert
        await act.Should().ThrowAsync<IdentityApiException>();
    }

    [Fact]
    public async Task DeleteClaimAsync_ShouldThrowIdentityApiException_WhenApiReturnsError()
    {
        // Arrange
        var id = "claim-1";
        
        _httpMock.When(HttpMethod.Delete, $"http://localhost:5000/api/v1/masterdata/claims/{id}")
                 .Respond(HttpStatusCode.NotFound, "application/json", "Not Found");

        var client = CreateClient();

        // Act
        var act = async () => await client.DeleteClaimAsync(id);

        // Assert
        await act.Should().ThrowAsync<IdentityApiException>();
    }
}
