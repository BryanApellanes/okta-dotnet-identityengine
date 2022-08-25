using Microsoft.AspNetCore.Mvc;
using Okta.IdentityEngine.Client.Data;

namespace Okta.IdentityEngine.AspNetCore
{
    public class DefaultOktaSignInPartialViewRenderResult : OktaViewRenderResult
    {
        public DefaultOktaSignInPartialViewRenderResult(MvcIdentityStateRenderer stateModelRenderer, OktaSignInRenderContext renderContext)
        {
            this.StateModelRenderer = stateModelRenderer;
            this.RenderContext = renderContext;
        }

        protected MvcIdentityStateRenderer StateModelRenderer { get; private set; }
        protected OktaSignInRenderContext RenderContext { get; private set; }

        public override string Render(string id)
        {
            throw new NotImplementedException();
        }

        protected override string GetId()
        {
            AppData appData = RenderContext.StateModel.App;
            return $"id-{appData.Name}";
        }
    }
}
