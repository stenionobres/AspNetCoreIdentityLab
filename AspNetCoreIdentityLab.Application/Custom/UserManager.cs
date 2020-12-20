using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using AspNetCoreIdentityLab.Application.Tools;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;

namespace AspNetCoreIdentityLab.Application.Custom
{
    public class UserManager : UserManager<User>
    {
        private readonly IConfiguration _configuration;

        private bool EncryptionEnabled
        {
            get
            {
                bool.TryParse(_configuration["TwoFactorAuthentication:EncryptionEnabled"], out bool encryptionEnabled);

                return encryptionEnabled;
            }
        }

        private string EncryptionKey
        {
            get
            {
                return _configuration["TwoFactorAuthentication:EncryptionKey"];
            }
        }

        public UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, 
                           IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, 
                           IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, 
                           IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger, IConfiguration configuration) 
                           : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _configuration = configuration;
        }

        public override string GenerateNewAuthenticatorKey()
        {
            var originalAuthenticatorKey = base.GenerateNewAuthenticatorKey();

            var encryptedKey = EncryptionEnabled
                               ? new AesEncryptor(EncryptionKey).Encrypt(originalAuthenticatorKey)
                               : originalAuthenticatorKey;

            return encryptedKey;
        }

        public override async Task<string> GetAuthenticatorKeyAsync(User user)
        {
            var databaseKey = await base.GetAuthenticatorKeyAsync(user);

            if (databaseKey == null) return null;
            
            var originalAuthenticatorKey = EncryptionEnabled
                                           ? new AesEncryptor(EncryptionKey).Decrypt(databaseKey)
                                           : databaseKey;

            return originalAuthenticatorKey;
        }
    }
}
