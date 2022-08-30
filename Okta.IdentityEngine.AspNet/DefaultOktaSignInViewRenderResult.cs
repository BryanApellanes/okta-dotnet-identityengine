using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Okta.IdentityEngine.Client;
using Okta.IdentityEngine.Client.Data;
using System.Text;

namespace Okta.IdentityEngine.AspNet
{
    public class DefaultOktaSignInViewRenderResult : OktaViewRenderResult
    {
        public DefaultOktaSignInViewRenderResult(MvcIdentityStateRenderer stateModelRenderer, OktaSignInRenderContext renderContext)
        {
            this.StateRenderer = stateModelRenderer ?? throw new ArgumentNullException(nameof(stateModelRenderer));
            this.RenderContext = renderContext ?? throw new ArgumentNullException(nameof(renderContext));
        }

        protected MvcIdentityStateRenderer StateRenderer { get; private set; }
        protected OktaSignInRenderContext RenderContext { get; private set; }
        protected override string GetId()
        {
            AppData appData = RenderContext.StateModel.App;
            return $"id-{appData.Name}";
        }

        public override string Render(string id)
        {
            IdentityState stateModel = RenderContext.StateModel;
            
            OktaStateModelRenderResult oktaStateModelRenderResult = StateRenderer.GetStateModelRenderResult(RenderContext, stateModel);

            return oktaStateModelRenderResult.Render(id);
        }
    }
}
