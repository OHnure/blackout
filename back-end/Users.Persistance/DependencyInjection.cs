using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Interfaces;
using Users.Domain;

namespace Users.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<Sql8580971Context>(options =>
                options.UseMySql("server=sql8.freemysqlhosting.net;port=3306;username=sql8580971;password=K9jSZ6uggi;database=sql8580971", ServerVersion.Parse("5.5.62-mysql")));
            services.AddScoped<IUsersDbContext>(provider => provider.GetService<Sql8580971Context>());
            return services;
        }
    }
}
