using Microsoft.AspNetCore.Mvc;
using Okta.IdentityEngine.Client;
using Okta.IdentityEngine.Client.Data;

namespace Okta.IdentityEngine.AspNet
{
    public class OktaStateModelRenderResult : OktaSignInViewRenderResult
    {
        public OktaStateModelRenderResult(OktaSignInRenderContext renderContext, IdentityState stateModel): base(renderContext)
        {
            this.StateModel = stateModel;
        }

        protected MvcIdentityStateRenderer MvcIdentityStateModelRenderer 
        {
            get => (MvcIdentityStateRenderer)RenderContext.Renderer;
        }

        protected override string GetId()
        {
            AppData data = StateModel.App;
            return $"id-{data.Name}";
        }
   

        public IdentityState StateModel
        {
            get;
            private set;
        }

        public override string Render(string id)
        {
            Tag parentDiv = new Tag("div");

            // for each Remediation get a render result for the remediation
            foreach (Remediation remediation in StateModel.Remediations)
            {
                OktaRemediationRenderResult remediationRenderResult = MvcIdentityStateModelRenderer.GetRemediationRenderResult(RenderContext, remediation);
                
                Tag remediationTag = new Tag("div")
                    .Html(remediationRenderResult.Render(remediation.Name));

                parentDiv.AddHtml(remediationTag);
            }

            return parentDiv.Render();
        }
    }
}
