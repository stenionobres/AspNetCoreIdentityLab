using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;

namespace AspNetCoreIdentityLab.Persistence.EntityFrameworkContexts
{
    public class AuthenticationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        private const string ConnectionString = @"Server=192.168.0.13,22331;Database=Authentication;User ID=sa;Password=sqlserver.252707;
                                                  Encrypt=False;Trusted_Connection=False;Connection Timeout=3000;";

        public static readonly ILoggerFactory LoggerFactoryToConsole = LoggerFactory.Create(builder => builder.AddConsole());

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            optionsBuilder.UseSqlServer(ConnectionString);
            optionsBuilder.UseLoggerFactory(LoggerFactoryToConsole);
        }
    }
}
