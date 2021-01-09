using Dapper;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;

namespace AspNetCoreIdentityLab.Persistence.IdentityStores
{
    public class UserStore
    {
        private const string ConnectionString = @"Server=192.168.1.5,22331;Database=AspNetCoreIdentityLab;User ID=sa;Password=sqlserver.252707;
                                                  Encrypt=False;Trusted_Connection=False;Connection Timeout=3000;";
        
        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync(cancellationToken);

                var query = $@"INSERT INTO [AspNetUsers] 
                                ([UserName], 
                                 [NormalizedUserName], 
                                 [Email],
                                 [NormalizedEmail], 
                                 [EmailConfirmed], 
                                 [PasswordHash], 
                                 [SecurityStamp], 
                                 [ConcurrencyStamp], 
                                 [PhoneNumber], 
                                 [PhoneNumberConfirmed], 
                                 [TwoFactorEnabled],
                                 [LockoutEnd],
                                 [LockoutEnabled],
                                 [AccessFailedCount],
                                 [Occupation])
                                VALUES (
                                    @{nameof(User.UserName)}, 
                                    @{nameof(User.NormalizedUserName)}, 
                                    @{nameof(User.Email)},
                                    @{nameof(User.NormalizedEmail)}, 
                                    @{nameof(User.EmailConfirmed)}, 
                                    @{nameof(User.PasswordHash)},
                                    @{nameof(User.SecurityStamp)},
                                    @{nameof(User.ConcurrencyStamp)},
                                    @{nameof(User.PhoneNumber)}, 
                                    @{nameof(User.PhoneNumberConfirmed)}, 
                                    @{nameof(User.TwoFactorEnabled)},
                                    @{nameof(User.LockoutEnd)},
                                    @{nameof(User.LockoutEnabled)},
                                    @{nameof(User.AccessFailedCount)},
                                    @{nameof(User.Occupation)});
                                SELECT CAST(SCOPE_IDENTITY() as int)";

                user.Id = await connection.QuerySingleAsync<int>(query, user);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($"DELETE FROM [AspNetUsers] WHERE [Id] = @{nameof(user.Id)}", user);
            }

            return IdentityResult.Success;
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<User>($@"SELECT * FROM [AspNetUsers]
                             WHERE [Id] = @{nameof(userId)}", new { userId });
            }
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<User>($@"SELECT * FROM [AspNetUsers]
                             WHERE [NormalizedUserName] = @{nameof(normalizedUserName)}", new { normalizedUserName });
            }
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"UPDATE [AspNetUsers] SET
                    [UserName] = @{nameof(user.UserName)},
                    [NormalizedUserName] = @{nameof(user.NormalizedUserName)},
                    [Email] = @{nameof(user.Email)},
                    [NormalizedEmail] = @{nameof(user.NormalizedEmail)},
                    [EmailConfirmed] = @{nameof(user.EmailConfirmed)},
                    [PasswordHash] = @{nameof(user.PasswordHash)},
                    [SecurityStamp] = @{nameof(user.SecurityStamp)},
                    [ConcurrencyStamp] = @{nameof(user.ConcurrencyStamp)},
                    [PhoneNumber] = @{nameof(user.PhoneNumber)},
                    [PhoneNumberConfirmed] = @{nameof(user.PhoneNumberConfirmed)},
                    [TwoFactorEnabled] = @{nameof(user.TwoFactorEnabled)},
                    [LockoutEnd] = @{nameof(user.LockoutEnd)},
                    [LockoutEnabled] = @{nameof(user.LockoutEnabled)},
                    [AccessFailedCount] = @{nameof(user.AccessFailedCount)},
                    [Occupation] = @{nameof(user.Occupation)}
                    WHERE [Id] = @{nameof(user.Id)}", user);
            }

            return IdentityResult.Success;
        }

        public Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<User>($@"SELECT * FROM [AspNetUsers]
                             WHERE [NormalizedEmail] = @{nameof(normalizedEmail)}", new { normalizedEmail });
            }
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }
        
    }
}
