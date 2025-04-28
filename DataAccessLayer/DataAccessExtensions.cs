using DataAccessLayer.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 

namespace DataAccessLayer
{
    public static class DataAccessExtensions
    {
        public static void AddDataAccessServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<BTSDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.MigrationsAssembly("DataAccessLayer")));
        }
    }
}


