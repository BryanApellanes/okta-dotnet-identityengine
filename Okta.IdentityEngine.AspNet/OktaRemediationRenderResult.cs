using Microsoft.AspNetCore.Mvc;
using Okta.IdentityEngine.Client;

namespace Okta.IdentityEngine.AspNet
{
    public class OktaRemediationRenderResult : OktaSignInViewRenderResult
    {
        public OktaRemediationRenderResult(OktaSignInRenderContext context, Remediation remediation): base(context)
        {
            this.Remediation = remediation ?? throw new ArgumentNullException(nameof(remediation));
        }
        
        protected MvcIdentityStateRenderer MvcIdentityStateModelRenderer
        {
            get => (MvcIdentityStateRenderer)RenderContext.Renderer;
        }

        public Remediation Remediation { get; private set; }

        public override string Render(string id)
        {
            OktaIonFormRenderResult formRenderResult = new OktaIonFormRenderResult(RenderContext, Remediation.Form);
            return formRenderResult.Render(id);
        }

        protected override string GetId()
        {
            return Remediation.Name ?? Guid.NewGuid().ToString();
        }
    }
}
