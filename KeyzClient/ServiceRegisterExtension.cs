using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyzClient
{
    public static class ServiceRegisterExtension
    {
        public static void AddKeyzClient(this IServiceCollection services, string ip, int port)
        {
            services.AddSingleton<IKeyz>(new Keyz(ip, port));
        }
    }
}
