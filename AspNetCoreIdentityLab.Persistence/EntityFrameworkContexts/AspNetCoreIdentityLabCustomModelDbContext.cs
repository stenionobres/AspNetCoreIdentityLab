using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;

namespace AspNetCoreIdentityLab.Persistence.EntityFrameworkContexts
{
    public class AspNetCoreIdentityLabCustomModelDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        private const string ConnectionString = @"Server=192.168.0.13,22331;Database=AspNetCoreIdentityLabCustomModel;User ID=sa;Password=sqlserver.252707;
                                                  Encrypt=False;Trusted_Connection=False;Connection Timeout=3000;";

        public static readonly ILoggerFactory LoggerFactoryToConsole = LoggerFactory.Create(builder => builder.AddConsole());

        public DbSet<UserLoginIp> UserLoginIp { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            optionsBuilder.UseSqlServer(ConnectionString);
            optionsBuilder.UseLoggerFactory(LoggerFactoryToConsole);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("authentication");
            builder.Entity<User>().ToTable("User");
            builder.Entity<IdentityRole<int>>().ToTable("Role");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRole");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaim");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserToken");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogin");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");
        }
    }
}
