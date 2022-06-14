using Microsoft.AspNetCore.Mvc;
using Okta.IdentityEngine.Client;
using Okta.IdentityEngine.Client.Data;
using Okta.IdentityEngine.Client.View;
using Okta.IdentityEngine.Data;
using Okta.IdentityEngine.Ioc;
using Okta.IdentityEngine.Logging;
using Okta.IdentityEngine.Session;

namespace Okta.IdentityEngine.AspNetCore
{
    public static class AspNetServices
    {
        private static ServiceProvider? mvcServiceProvider;

        private static readonly object mvcLock = new object();

        public static ServiceProvider Mvc
        {
            get
            {
                if (mvcServiceProvider == null)
                {
                    lock (mvcLock)
                    {
                        if (mvcServiceProvider == null)
                        {
                            ServiceProvider temp = new ServiceProvider();
                            
                            temp.RegisterService<IServiceProvider>(temp);
                            temp.RegisterService<IIdentityClient>(new IdentityClient());
                            
                            temp.RegisterService<IIdentityPolicyProvider>(new IdentityPolicyProvider());
                            temp.RegisterService<IIdentitySessionProvider>(new SecureSessionProvider());
                            temp.RegisterService<IIdentityLoggingProvider>(new IdentityLoggingProvider());
                            temp.RegisterService<IStorageProvider, FileStorageProvider>();
                            temp.RegisterService<IIdentityDataProvider, IdentityDataProvider>();

                            temp.RegisterService<IIdentityPipelineManager, MvcIdentityPipelineManager>();
                            temp.RegisterService<IIdentityStateRenderer<OktaSignInRenderContext, IActionResult>, MvcIdentityStateRenderer>();
                            temp.RegisterService<IIdentityViewProvider<OktaSignInRenderContext, IActionResult>, MvcIdentityViewProvider>();
                            mvcServiceProvider = temp;
                        }
                    }
                }

                return mvcServiceProvider;
            }
        }
    }
}
