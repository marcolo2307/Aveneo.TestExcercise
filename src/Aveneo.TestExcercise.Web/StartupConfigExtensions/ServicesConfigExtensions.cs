﻿using Aveneo.TestExcercise.Web.Services;
using Aveneo.TestExcercise.Web.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Aveneo.TestExcercise.Web.StartupConfigExtensions
{
    public static class ServicesConfigExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IIconDecoder, FontAwesomeDecoder>();
        }
    }
}