using AutoMapper;
using Aveneo.TestExcercise.Web.Profiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aveneo.TestExcercise.Web.StartupConfigExtensions
{
    public static class MappingConfigExtensions
    {
        public static void ConfigureMapping(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<DataProfile>();
            });
            services.AddAutoMapper(typeof(DataProfile));
        }
    }
}
