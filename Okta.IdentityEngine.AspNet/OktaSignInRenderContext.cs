using Microsoft.AspNetCore.Mvc;
using Okta.IdentityEngine.Client;
using Okta.IdentityEngine.Client.View;
using Okta.IdentityEngine.Logging;
using Okta.IdentityEngine.Session;

namespace Okta.IdentityEngine.AspNet
{
    public class OktaSignInRenderContext : IRenderContext<OktaSignInRenderContext, IActionResult>
    {
        public OktaSignInRenderContext(OktaMvcPlatform platform, IdentityState model, string? viewName = null, Controller? callingController = null)
        {
            this.Platform = platform;
            this.StateModel = model;
            this.ViewName = viewName ?? "SignIn";
            this.CallingController = callingController;
        }

        protected OktaMvcPlatform Platform { get; private set; }

        public MvcOktaIdentityEngineOptions Options => Platform.Options;

        public OktaMiddlewarePath MiddlewarePath => Platform.MiddlewarePath;

        public IIdentityViewProvider<OktaSignInRenderContext, IActionResult> ViewProvider => Platform.ViewProvider;

        public IIdentityStateRenderer<OktaSignInRenderContext, IActionResult> Renderer => Platform.StateRenderer;

        public IIdentityPolicyProvider PolicyProvider => Platform.PolicyProvider;

        public IIdentitySessionProvider SessionProvider => Platform.SessionProvider;

        public IIdentityLoggingProvider LoggingProvider => Platform.LoggingProvider;

        public IdentityState StateModel
        {
            get;
            set;
        }

        public IActionResult? Result
        {
            get;
            set;
        }

        public string ViewName { get; private set; }

        public Controller? CallingController { get; set; }

        public Task<IdentityRenderResult<OktaSignInRenderContext, IActionResult>> RenderAsync()
        {
            return ViewProvider.GetRenderResultAsync(this);
        }

        public Task<IdentityRenderResult<OktaSignInRenderContext, IActionResult>> RenderPartialAsync()
        {
            return ViewProvider.GetPartialRenderResultAsync(this);
        }
    }
}
