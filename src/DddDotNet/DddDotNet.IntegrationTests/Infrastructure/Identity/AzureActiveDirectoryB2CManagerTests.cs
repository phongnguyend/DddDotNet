using DddDotNet.Infrastructure.Identity;
using DddDotNet.Infrastructure.Identity.Azure;
using Elasticsearch.Net.Specification.WatcherApi;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Infrastructure.Identity;

public class AzureActiveDirectoryB2CManagerTests
{
    private readonly AzudeAdB2COptions _options = new();

    public AzureActiveDirectoryB2CManagerTests()
    {
        var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddUserSecrets("09f024f8-e8d1-4b78-9ddd-da941692e8fa")
        .Build();

        config.GetSection("Identity:AzureActiveDirectoryB2C").Bind(_options);
    }

    [Fact]
    public async Task CreateAsync_Success()
    {
        var sut = new AzureActiveDirectoryB2CManager(_options);

        var users = await sut.GetUsersAsync();

        var newGuid = Guid.NewGuid().ToString().Replace("-", string.Empty);
        var user = new User
        {
            Username = $"{newGuid}@ddddotnet.onmicrosoft.com",
            Password = Guid.NewGuid().ToString(),
            FirstName = "FirstName",
            LastName = "LastName",
            Email = $"{newGuid}@ddddotnet.onmicrosoft.com",
        };

        await sut.CreateUserAsync(user);

        var userById = await sut.GetUserById(user.Id);
        var userByUsername = await sut.GetUserByUsernameAsync(user.Username);

        user.FirstName += "XXX";
        user.LastName += "XXX";

        await sut.UpdateUserAsync(user.Id, user);

        var userByIdAfterUpdate = await sut.GetUserById(user.Id);
        var userByUsernameAfterUpdate = await sut.GetUserByUsernameAsync(user.Username);

        var usersAgain = await sut.GetUsersAsync();

        await sut.DeleteUserAsync(user.Id);

        var userByIdAgain = await sut.GetUserById(user.Id);
        var userByUsernameAgain = await sut.GetUserByUsernameAsync(user.Username);

        Assert.NotNull(userById.Id);
        Assert.True(usersAgain.Count > users.Count);
        Assert.Equal(userById.Id, userByUsername.Id);
        Assert.Null(userByIdAgain);
        Assert.Null(userByUsernameAgain);
    }
}
