using Aveneo.TestExcercise.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aveneo.TestExcercise.Web.StartupConfigExtensions
{
    public static class DatabaseConfigExtensions
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("Data"), 
                    o => o.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));
        }
    }
}
