using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AspNetCoreIdentityLab.Persistence.IdentityStores;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;

namespace AspNetCoreIdentityLab.Application.Services
{
    public class UserStoreService : IUserStore<User>, IUserPasswordStore<User>, IUserPhoneNumberStore<User>, IUserEmailStore<User>
    {
        private readonly UserStore _userStore;

        public UserStoreService(UserStore userStore)
        {
            _userStore = userStore;
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            return _userStore.CreateAsync(user, cancellationToken);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            return _userStore.DeleteAsync(user, cancellationToken);
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return _userStore.FindByIdAsync(userId, cancellationToken);
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _userStore.FindByNameAsync(normalizedUserName, cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return _userStore.GetNormalizedUserNameAsync(user, cancellationToken);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return _userStore.GetUserIdAsync(user, cancellationToken);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return _userStore.GetUserNameAsync(user, cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return _userStore.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            return _userStore.SetUserNameAsync(user, userName, cancellationToken);
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            return _userStore.UpdateAsync(user, cancellationToken);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            return _userStore.SetPasswordHashAsync(user, passwordHash, cancellationToken);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return _userStore.GetPasswordHashAsync(user, cancellationToken);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return _userStore.HasPasswordAsync(user, cancellationToken);
        }

        public Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            return _userStore.SetPhoneNumberAsync(user, phoneNumber, cancellationToken);
        }

        public Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            return _userStore.GetPhoneNumberAsync(user, cancellationToken);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return _userStore.GetPhoneNumberConfirmedAsync(user, cancellationToken);
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            return _userStore.SetPhoneNumberConfirmedAsync(user, confirmed, cancellationToken);
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            return _userStore.SetEmailAsync(user, email, cancellationToken);
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return _userStore.GetEmailAsync(user, cancellationToken);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return _userStore.GetEmailConfirmedAsync(user, cancellationToken);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            return _userStore.SetEmailConfirmedAsync(user, confirmed, cancellationToken);
        }

        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return _userStore.FindByEmailAsync(normalizedEmail, cancellationToken);
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return _userStore.GetNormalizedEmailAsync(user, cancellationToken);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return _userStore.SetNormalizedEmailAsync(user, normalizedEmail, cancellationToken);
        }

        public void Dispose()
        {

        }

    }
}
