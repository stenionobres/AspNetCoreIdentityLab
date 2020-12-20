using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using AspNetCoreIdentityLab.Application.Tools;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;
using System.Linq;

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

        protected override string CreateTwoFactorRecoveryCode()
        {
            var originalRecoveryCode = base.CreateTwoFactorRecoveryCode();

            var encryptedRecoveryCode = EncryptionEnabled
                                        ? new AesEncryptor(EncryptionKey).Encrypt(originalRecoveryCode)
                                        : originalRecoveryCode;

            return encryptedRecoveryCode;
        }

        public override async Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(User user, int number)
        {
            var tokens = await base.GenerateNewTwoFactorRecoveryCodesAsync(user, number);

            var generatedTokens = EncryptionEnabled
                                  ? tokens.Select(token => new AesEncryptor(EncryptionKey).Decrypt(token))
                                  : tokens;

            return generatedTokens;
        }

        public override Task<IdentityResult> RedeemTwoFactorRecoveryCodeAsync(User user, string code)
        {
            if (EncryptionEnabled && !string.IsNullOrEmpty(code))
            {
                code = new AesEncryptor(EncryptionKey).Encrypt(code);
            }

            return base.RedeemTwoFactorRecoveryCodeAsync(user, code);
        }
    }
}
