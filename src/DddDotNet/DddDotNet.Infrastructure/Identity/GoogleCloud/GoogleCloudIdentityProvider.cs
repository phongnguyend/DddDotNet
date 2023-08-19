using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Identity.GoogleCloud;

public class GoogleCloudIdentityProvider : IUserProvider
{
    private readonly GoogleCloudIdentityOptions _options;
    private readonly FirebaseApp _firebaseApp;

    public GoogleCloudIdentityProvider(GoogleCloudIdentityOptions options)
    {
        _options = options;

        _firebaseApp = FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile(_options.CredentialFilePath),
        });
    }

    public async Task CreateUserAsync(IUser user)
    {
        UserRecordArgs args = new UserRecordArgs()
        {
            Email = user.Username,
            DisplayName = $"{user.FirstName} {user.LastName}",
            Disabled = false,
        };

        UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);
        user.Id = userRecord.Uid;
    }

    public async Task DeleteUserAsync(string userId)
    {
        await FirebaseAuth.DefaultInstance.DeleteUserAsync(userId);
    }

    public async Task<IUser> GetUserById(string userId)
    {
        try
        {
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(userId);

            return new User
            {
                Id = userRecord.Uid,
                Username = userRecord.Email,
                Email = userRecord.Email,
                FirstName = userRecord.DisplayName?.Split(' ')[0],
                LastName = userRecord.DisplayName?.Split(' ')[1],
            };
        }
        catch (FirebaseAuthException ex)
        {
            if (ex.AuthErrorCode == AuthErrorCode.UserNotFound)
            {
                return null;
            }

            throw;
        }
    }

    public async Task<IUser> GetUserByUsernameAsync(string username)
    {
        try
        {
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(username);

            return new User
            {
                Id = userRecord.Uid,
                Username = userRecord.Email,
                Email = userRecord.Email,
                FirstName = userRecord.DisplayName?.Split(' ')[0],
                LastName = userRecord.DisplayName?.Split(' ')[1],
            };
        }
        catch (FirebaseAuthException ex)
        {
            if (ex.AuthErrorCode == AuthErrorCode.UserNotFound)
            {
                return null;
            }

            throw;
        }
    }

    public async Task<IList<IUser>> GetUsersAsync()
    {
        IList<IUser> users = new List<IUser>();

        var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
        var responses = pagedEnumerable.AsRawResponses().GetAsyncEnumerator();
        while (await responses.MoveNextAsync())
        {
            ExportedUserRecords response = responses.Current;
            foreach (ExportedUserRecord user in response.Users)
            {
                users.Add(new User
                {
                    Id = user.Uid,
                    Username = user.Email,
                    Email = user.Email,
                    FirstName = user.DisplayName?.Split(' ')[0],
                    LastName = user.DisplayName?.Split(' ')[1],
                });
            }
        }

        return users;
    }

    public async Task UpdateUserAsync(string userId, IUser user)
    {
        UserRecordArgs args = new UserRecordArgs()
        {
            Uid = userId,
            DisplayName = $"{user.FirstName} {user.LastName}",
            Disabled = false,
        };

        UserRecord userRecord = await FirebaseAuth.DefaultInstance.UpdateUserAsync(args);
    }
}
