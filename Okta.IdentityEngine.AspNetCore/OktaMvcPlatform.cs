using Microsoft.AspNetCore.Mvc;
using Okta.IdentityEngine.Client;
using Okta.IdentityEngine.Client.View;
using Okta.IdentityEngine.Logging;
using Okta.IdentityEngine.Session;

namespace Okta.IdentityEngine.AspNetCore
{
    public class OktaMvcPlatform
    {
        public OktaMvcPlatform(OktaMiddlewarePath middlewarePath, MvcOktaIdentityEngineOptions options, IIdentityPipelineManager identityPipelineManager, IIdentityViewProvider<OktaSignInRenderContext, IActionResult> viewProvider, IIdentityStateRenderer<OktaSignInRenderContext, IActionResult> stateModelRenderer, IIdentityPolicyProvider policyProvider, IIdentitySessionProvider sessionProvider, IIdentityLoggingProvider loggingProvider)
        {
            this.MiddlewarePath = middlewarePath;
            this.Options = options;
            this.IdentityPipelineManager = identityPipelineManager;
            this.ViewProvider = viewProvider;
            this.StateRenderer = stateModelRenderer;
            this.PolicyProvider = policyProvider;
            this.SessionProvider = sessionProvider;
            this.LoggingProvider = loggingProvider;
        }

        public OktaMiddlewarePath MiddlewarePath { get; private set; }
        public MvcOktaIdentityEngineOptions Options { get; private set; }
        public IIdentityPipelineManager IdentityPipelineManager { get; private set; }
        public IIdentityViewProvider<OktaSignInRenderContext, IActionResult> ViewProvider { get; private set; }
        public IIdentityStateRenderer<OktaSignInRenderContext, IActionResult> StateRenderer { get; private set; }

        public IIdentityPolicyProvider PolicyProvider { get; private set; }
        public IIdentitySessionProvider SessionProvider { get; private set; }

        public IIdentityLoggingProvider LoggingProvider { get; private set; }

        public async Task<IIdentityRenderResult<OktaSignInRenderContext, IActionResult>> GetSignInViewAsync(Controller? callingController = null)
        {
            return await GetSignInViewAsync("SignIn", callingController);
        }
        
        public async Task<IIdentityRenderResult<OktaSignInRenderContext, IActionResult>> GetSignInViewAsync(string viewName, Controller? callingController = null)
        {
            IdentityState identityModel = await IdentityPipelineManager.StartPipelineAsync();

            return await GetSignInViewAsync(identityModel, viewName, callingController);
        }

        public async Task<IIdentityRenderResult<OktaSignInRenderContext, IActionResult>> GetSignInViewAsync(string stateHandle, string viewName, Controller? callingController = null)
        {
            IdentityState identityModel = await IdentityPipelineManager.ContinuePipelineAsync(stateHandle);
            return await GetSignInViewAsync(identityModel, viewName, callingController);
        }

        public async Task<IIdentityRenderResult<OktaSignInRenderContext, IActionResult>> GetSignInViewAsync(IdentityState identityModel, string viewName, Controller? callingController = null)
        {
            OktaSignInRenderContext mvcRenderContext = new OktaSignInRenderContext(this, identityModel, viewName, callingController);

            return await mvcRenderContext.RenderAsync();
        }

        public async Task<IIdentityRenderResult<OktaSignInRenderContext, IActionResult>> GetSignInPartialViewAsync(Controller? callingController = null)
        {
            return await GetSignInPartialViewAsync("SignInPartial", callingController);
        }

        public async Task<IIdentityRenderResult<OktaSignInRenderContext, IActionResult>> GetSignInPartialViewAsync(string partialViewName, Controller? callingController = null)
        {
            IdentityState identityModel = await IdentityPipelineManager.StartPipelineAsync();

            return await GetSignInPartialViewAsync(identityModel, partialViewName, callingController);
        }

        public async Task<IIdentityRenderResult<OktaSignInRenderContext, IActionResult>> GetSignInPartialViewAsync(string stateHandle, string partialViewName, Controller? callingController = null)
        {
            IdentityState identityModel = await IdentityPipelineManager.ContinuePipelineAsync(stateHandle);

            return await GetSignInPartialViewAsync(identityModel, partialViewName, callingController);
        }

        public async Task<IIdentityRenderResult<OktaSignInRenderContext, IActionResult>> GetSignInPartialViewAsync(IdentityState identityModel, string partialViewName, Controller? callingController = null)
        {
            OktaSignInRenderContext mvcRenderContext = new OktaSignInRenderContext(this, identityModel, partialViewName, callingController);

            return await mvcRenderContext.RenderPartialAsync();
        }
    }
}
