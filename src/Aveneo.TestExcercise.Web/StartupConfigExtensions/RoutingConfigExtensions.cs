using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Aveneo.TestExcercise.Web.StartupConfigExtensions
{
    public static class RoutingConfigExtensions
    {
        public static void ConfigureRouting(this IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
    }
}
