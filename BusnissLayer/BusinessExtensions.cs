using DataAccessLayer.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusnissLayer.Managers;

namespace BusnissLayer
{
    public static class BusinessExtensions
    {
        public static void AddBusinessServices(
        this IServiceCollection services
        )
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IProjectManager, ProjectManager>();
            services.AddScoped<IBugManager, BugManager>();

            services.AddScoped<IAttachmentManager, AttachmentManager>();
            services.AddScoped<IUserBugManager, UserBugManager>();

        }
    }
}
