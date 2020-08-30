using System;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;
using AspNetCoreIdentityLab.Persistence.EntityFrameworkContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AspNetCoreIdentityLab.Application.Areas.Identity.IdentityHostingStartup))]
namespace AspNetCoreIdentityLab.Application.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}