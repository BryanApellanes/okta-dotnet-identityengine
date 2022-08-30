using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Okta.IdentityEngine.Client;
using Okta.IdentityEngine.Client.View;
using Okta.IdentityEngine.Configuration;
using Okta.IdentityEngine.Logging;
using Okta.IdentityEngine.Session;

namespace Okta.IdentityEngine.AspNet
{
    public static class OktaMvcServiceCollectionExtensions
    {
        public static void AddOktaIdentityEngine(this IServiceCollection aspServices, string mapPath)
        {
            AddOktaIdentityEngine(aspServices, mapPath, (oieoptions) => { });
        }

        public static void AddOktaIdentityEngine(this IServiceCollection aspServices, Action<OktaIdentityEngineOptions>? configure)
        {
            AddOktaIdentityEngine(aspServices, "/oktaidentityengine", configure);
        }

        public static void AddOktaIdentityEngine(this IServiceCollection aspServices, string mapPath, Action<OktaIdentityEngineOptions>? configure)
        {
            if(aspServices == null)
            {
                throw new ArgumentNullException(nameof(aspServices));
            }
            
            MvcOktaIdentityEngineOptions options = new MvcOktaIdentityEngineOptions();

            if (configure != null)
            {
                configure(options);
            }

            aspServices.AddOktaServices();

            aspServices.AddSingleton(new OktaMiddlewarePath(mapPath));
            aspServices.AddSingleton(options);
            aspServices.AddSingleton<IIdentityStateRenderer<OktaSignInRenderContext, IActionResult>, MvcIdentityStateRenderer>();
            aspServices.AddSingleton<IIdentityViewProvider<OktaSignInRenderContext, IActionResult>, MvcIdentityViewProvider>();
            aspServices.AddSingleton<IIdentityPipelineManager>(svcProvider => 
            {
                MvcOktaIdentityEngineOptions oktaOptions = svcProvider.GetService<MvcOktaIdentityEngineOptions>() ?? new MvcOktaIdentityEngineOptions();
                if (oktaOptions != null)
                {
                    return new MvcIdentityPipelineManager(oktaOptions);
                }
                return new MvcIdentityPipelineManager();
            });

            aspServices.AddSingleton<OktaMvcPlatform>();            
            aspServices.AddScoped<OktaMiddleware>();
        }

        private static void AddOktaServices(this IServiceCollection aspServices)
        {
            aspServices.AddSingleton(AspNetServices.Mvc.GetService<IIdentityPolicyProvider>());
            aspServices.AddSingleton(AspNetServices.Mvc.GetService<IIdentitySessionProvider>());
            aspServices.AddSingleton(AspNetServices.Mvc.GetService<IIdentityLoggingProvider>());
        }
    }
}
