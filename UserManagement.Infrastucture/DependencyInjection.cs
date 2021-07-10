using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Infrastucture
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddUserDbContext(this IServiceCollection services, IConfiguration configuation)
        {
            services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();

            var connectionString = configuation.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString, b => b.MigrationsAssembly("UserManagement")));

            return services;
        }

    }
}
