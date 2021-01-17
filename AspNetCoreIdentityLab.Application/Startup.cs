using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AspNetCoreIdentityLab.Persistence.EntityFrameworkContexts;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using AspNetCoreIdentityLab.Application.IdentityValidators;
using Microsoft.AspNetCore.Identity.UI.Services;
using AspNetCoreIdentityLab.Application.EmailSenders;
using System;
using AspNetCoreIdentityLab.Application.Services;
using AspNetCoreIdentityLab.Application.Custom;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using AspNetCoreIdentityLab.Persistence.Mappers;
using AspNetCoreIdentityLab.Persistence.IdentityStores;

namespace AspNetCoreIdentityLab.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var persistenceWithDapper = Convert.ToBoolean(Configuration["PersistenceWithDapper"]);
            var identityBuilder = services.AddDefaultIdentity<User>(options => GetDefaultIdentityOptions(options))
                                          .AddRoles<IdentityRole<int>>()
                                          .AddUserManager<UserManager>()
                                          .AddUserValidator<CustomUserValidator>()
                                          .AddPasswordValidator<CustomPasswordValidator>();

            if (persistenceWithDapper)
            {
                services.AddTransient<IUserStore<User>, UserStoreService>();
                services.AddTransient<UserStore>();
            }
            else
            {
                identityBuilder.AddEntityFrameworkStores<AspNetCoreIdentityLabDbContext>();
            }

            services.AddDbContext<AspNetCoreIdentityLabDbContext>();

            services.ConfigureApplicationCookie(cookieOptions => GetCookieAuthenticationOptions(cookieOptions));

            // Default Identity TokenLifespan value.
            services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(1));

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddHttpClient();

            services.AddAuthentication()
                    .AddFacebook(facebookOptions => GetFacebookOptions(facebookOptions))
                    .AddGoogle(googleOptions => GetGoogleOptions(googleOptions));

            services.AddTransient<IEmailSender, EmailSmtpSender>(email => GetEmailConfiguration());
            services.AddTransient<GoogleRecaptchaService>();
            services.AddTransient<UserLoginIPService>();
            services.AddTransient<UserLoginIPMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private void GetDefaultIdentityOptions(IdentityOptions identityOptions)
        {
            // Default Identity Password settings.
            identityOptions.Password.RequireDigit = true;
            identityOptions.Password.RequireLowercase = true;
            identityOptions.Password.RequireNonAlphanumeric = true;
            identityOptions.Password.RequireUppercase = true;
            identityOptions.Password.RequiredLength = 6;
            identityOptions.Password.RequiredUniqueChars = 1;

            identityOptions.SignIn.RequireConfirmedAccount = false;
            identityOptions.SignIn.RequireConfirmedEmail = false;

            identityOptions.Lockout.AllowedForNewUsers = true;
            identityOptions.Lockout.MaxFailedAccessAttempts = 3;
            identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        }

        private void GetCookieAuthenticationOptions(CookieAuthenticationOptions cookieOptions)
        {
            var loginExpireTimeInMinutes = Convert.ToDouble(Configuration["LoginExpireTimeInMinutes"]);

            cookieOptions.ExpireTimeSpan = TimeSpan.FromMinutes(loginExpireTimeInMinutes);
            cookieOptions.LoginPath = "/Identity/Account/Login";
            cookieOptions.SlidingExpiration = true;
        }

        private EmailSmtpSender GetEmailConfiguration()
        {
            return new EmailSmtpSender(
                Configuration["EmailSmtpSender:Host"],
                Configuration.GetValue<int>("EmailSmtpSender:Port"),
                Configuration.GetValue<bool>("EmailSmtpSender:EnableSSL"),
                Configuration["EmailSmtpSender:UserName"],
                Configuration["EmailSmtpSender:Password"]
            );
        }

        private void GetFacebookOptions(FacebookOptions facebookOptions)
        {
            facebookOptions.AppId = Configuration["SocialNetworkAuthentication:Facebook:AppId"];
            facebookOptions.AppSecret = Configuration["SocialNetworkAuthentication:Facebook:AppSecret"];
            facebookOptions.SaveTokens = true;
        }

        private void GetGoogleOptions(GoogleOptions googleOptions)
        {
            googleOptions.ClientId = Configuration["SocialNetworkAuthentication:Google:ClientId"];
            googleOptions.ClientSecret = Configuration["SocialNetworkAuthentication:Google:ClientSecret"];
            googleOptions.SaveTokens = true;
        }
    }
}
