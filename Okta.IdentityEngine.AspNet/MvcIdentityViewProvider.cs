using Microsoft.AspNetCore.Mvc;
using Okta.IdentityEngine.Client;
using Okta.IdentityEngine.Client.View;

namespace Okta.IdentityEngine.AspNetCore
{
    public class MvcIdentityViewProvider : IIdentityViewProvider<OktaSignInRenderContext, IActionResult>
    {
        public MvcIdentityViewProvider(IIdentityStateRenderer<OktaSignInRenderContext, IActionResult> defualtRenderer)
        {
            this.DefaultRenderer = defualtRenderer;
        }

        public MvcIdentityStateRenderer MvcIdentityStateModelRenderer
        {
            get
            {
                return (MvcIdentityStateRenderer)this.DefaultRenderer;
            }
        }

        public IIdentityStateRenderer<OktaSignInRenderContext, IActionResult> DefaultRenderer 
        {
            get;
            private set;
        }

        public async Task<IdentityRenderResult<OktaSignInRenderContext, IActionResult>> GetRenderResultAsync(OktaSignInRenderContext renderContext)
        {
            if (renderContext == null)
            {
                throw new ArgumentNullException(nameof(renderContext));
            }
            if (renderContext.StateModel == null)
            {
                throw new ArgumentNullException(nameof(renderContext.StateModel));
            }

            if (renderContext.CallingController != null)
            {
                return new IdentityRenderResult<OktaSignInRenderContext, IActionResult>(renderContext.CallingController.View(renderContext.ViewName, renderContext.StateModel));
            };

            return await Task.FromResult(new IdentityRenderResult<OktaSignInRenderContext, IActionResult>(new DefaultOktaSignInViewRenderResult(MvcIdentityStateModelRenderer, renderContext)));
        }

        public async Task<IdentityRenderResult<OktaSignInRenderContext, IActionResult>> GetPartialRenderResultAsync(OktaSignInRenderContext renderContext)
        {
            if (renderContext == null)
            {
                throw new ArgumentNullException(nameof(renderContext));
            }
            if (renderContext.StateModel == null)
            {
                throw new ArgumentNullException(nameof(renderContext.StateModel));
            }

            if (renderContext.CallingController != null)
            {
                return new IdentityRenderResult<OktaSignInRenderContext, IActionResult>(renderContext.CallingController.PartialView(renderContext.ViewName, renderContext.StateModel));
            }

            return await Task.FromResult(new IdentityRenderResult<OktaSignInRenderContext, IActionResult>(new DefaultOktaSignInPartialViewRenderResult(MvcIdentityStateModelRenderer, renderContext)));
        }
    }
}
