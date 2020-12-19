using AspNetCoreIdentityLab.Persistence.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace AspNetCoreIdentityLab.Application.Custom
{
    public class UserManager : UserManager<User>
    {
        private readonly IConfiguration _configuration;

        public UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, 
                           IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, 
                           IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, 
                           IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger, IConfiguration configuration) 
                           : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _configuration = configuration;
        }
    }
}
