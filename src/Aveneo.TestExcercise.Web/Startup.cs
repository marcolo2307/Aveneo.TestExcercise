using Aveneo.TestExcercise.ApplicationCore;
using Aveneo.TestExcercise.ApplicationCore.Services;
using Aveneo.TestExcercise.Web.Services;
using Aveneo.TestExcercise.Web.StartupConfigExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aveneo.TestExcercise.Web
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDatabase(_configuration);
            services.ConfigureMapping(_configuration);
            services.ConfigureRouting();

            services.AddTransient<IIconDecoder, FontAwesomeDecoder>();
            services.AddTransient<IDataObjectService, DataObjectService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
