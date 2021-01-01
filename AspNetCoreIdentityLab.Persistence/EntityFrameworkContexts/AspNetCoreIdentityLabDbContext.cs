using AspNetCoreIdentityLab.Persistence.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspNetCoreIdentityLab.Persistence.EntityFrameworkContexts
{
    public class AspNetCoreIdentityLabDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        private const string ConnectionString = @"Server=192.168.1.14,22331;Database=AspNetCoreIdentityLab;User ID=sa;Password=sqlserver.252707;
                                                  Encrypt=False;Trusted_Connection=False;Connection Timeout=3000;";

        public static readonly ILoggerFactory LoggerFactoryToConsole = LoggerFactory.Create(builder => builder.AddConsole());

        public DbSet<UserLoginIp> UserLoginIp { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            optionsBuilder.UseSqlServer(ConnectionString);
            optionsBuilder.UseLoggerFactory(LoggerFactoryToConsole);
        }
    }
}
