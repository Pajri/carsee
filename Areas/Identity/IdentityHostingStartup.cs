using System;
using CarSee.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CarSee.Areas.Identity.IdentityHostingStartup))]
namespace CarSee.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<ApplicationIdentityDbContext>(o => o.UseNpgsql(context.Configuration.GetConnectionString("ApplicationIdentityDbContextConnection"),
                    options => options.SetPostgresVersion(new Version(9, 5))));

                services.AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;
                })
                    .AddEntityFrameworkStores<ApplicationIdentityDbContext>();
            });
        }
    }
}