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
            services.AddDbContext<AspNetCoreIdentityLabDbContext>();
            
            services.AddDefaultIdentity<User>(options => GetDefaultIdentityOptions(options))
                    .AddUserManager<UserManager>()
                    .AddUserValidator<CustomUserValidator>()
                    .AddPasswordValidator<CustomPasswordValidator>()
                    .AddEntityFrameworkStores<AspNetCoreIdentityLabDbContext>();

            // Default Identity TokenLifespan value.
            services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(1));

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddHttpClient();

            services.AddAuthentication().AddFacebook(facebookOptions => GetFacebookOptions(facebookOptions));

            services.AddTransient<IEmailSender, EmailSmtpSender>(email => GetEmailConfiguration());
            services.AddTransient<GoogleRecaptchaService>();
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

            identityOptions.SignIn.RequireConfirmedAccount = true;
            identityOptions.SignIn.RequireConfirmedEmail = true;
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
        }
    }
}
