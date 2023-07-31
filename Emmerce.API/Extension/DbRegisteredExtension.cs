
using Ecommerce.Data.Context;
using Ecommerce.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.API.Extension
{
    public static class DbRegisteredExtension
    {
        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DipoleEcommerceContext>().AddDefaultTokenProviders();
            services.AddDbContext<DipoleEcommerceContext>(dbContextOptions => dbContextOptions.UseSqlite(configuration["ConnectionStrings:DipoleConnectionString"]));
        }
    }
}
