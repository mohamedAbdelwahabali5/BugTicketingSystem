using DataAccessLayer.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 
using DataAccessLayer;
using DataAccessLayer.Repositories;
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


            // Add other services related to data access here
            services.AddScoped(typeof(IGenaricRepository<>), typeof(GeneraicRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBugRepository, BugRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IUserBugRepository, UserBugRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}


