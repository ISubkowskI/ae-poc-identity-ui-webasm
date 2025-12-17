using Ae.Poc.Identity.Ui.Dtos;
using Ae.Poc.Identity.Ui.Exceptions;
using Ae.Poc.Identity.Ui.Services;
using Ae.Poc.Identity.Ui.Settings;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RichardSzalay.MockHttp;
using System.Text.Json;

namespace Ae.Poc.Identity.Ui.Tests;

public class IdentityClientTests
{
    private readonly Mock<ILogger<IdentityClient>> _loggerMock;
    private readonly MockHttpMessageHandler _httpMock;
    private readonly IdentityApiOptions _options;

    public IdentityClientTests()
    {
        _loggerMock = new Mock<ILogger<IdentityClient>>();
        _httpMock = new MockHttpMessageHandler();
        _options = new IdentityApiOptions
        {
            ApiUrl = "http://localhost:5000",
            ApiBasePath = "/api/v1"
        };
    }

    private IdentityClient CreateClient()
    {
        var httpClient = _httpMock.ToHttpClient();
        httpClient.BaseAddress = new Uri(_options.ApiUrl);

        var optionsMock = new Mock<IOptions<IdentityApiOptions>>();
        optionsMock.Setup(x => x.Value).Returns(_options);

        return new IdentityClient(_loggerMock.Object, optionsMock.Object, httpClient);
    }

    [Fact]
    public async Task LoadAccountsAsync_ShouldReturnAccounts_WhenApiReturnsSuccess()
    {
        // Arrange
        var accounts = new[] { new AppAccountDto { Id = Guid.NewGuid(), DisplayName = "Test User" } };
        var json = JsonSerializer.Serialize(accounts);

        _httpMock.When("http://localhost:5000/api/v1/accounts")
                 .Respond("application/json", json);

        var client = CreateClient();

        // Act
        var result = await client.LoadAccountsAsync();

        // Assert
        result.Should().HaveCount(1);
        result.First().DisplayName.Should().Be("Test User");
    }

    [Fact]
    public async Task LoadAccountsAsync_ShouldThrowIdentityApiException_WhenApiReturnsError()
    {
        // Arrange
        _httpMock.When("http://localhost:5000/api/v1/accounts")
                 .Respond(System.Net.HttpStatusCode.InternalServerError);

        var client = CreateClient();

        // Act
        var act = async () => await client.LoadAccountsAsync();

        // Assert
        await act.Should().ThrowAsync<IdentityApiException>();
    }
}
