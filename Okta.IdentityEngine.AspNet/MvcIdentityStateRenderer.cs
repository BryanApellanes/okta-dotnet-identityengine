using Microsoft.AspNetCore.Mvc;
using Okta.IdentityEngine.Client;
using Okta.IdentityEngine.Client.View;
using Bam.Ion;

namespace Okta.IdentityEngine.AspNet
{
    public class MvcIdentityStateRenderer : IdentityStateRenderer<OktaSignInRenderContext, IActionResult>
    {
        protected override IActionResult RenderStateModel(OktaSignInRenderContext context, IdentityState model)
        {
            return GetStateModelRenderResult(context, model);
        }

        protected override IActionResult RenderRemediation(OktaSignInRenderContext context, Remediation remediation)
        {
            return GetRemediationRenderResult(context, remediation);
        }

        protected override IActionResult RenderIonForm(OktaSignInRenderContext context, IonForm ionForm)
        {
            return GetIonFormRenderResult(context, ionForm);
        }

        protected override IActionResult RenderIonFormField(OktaSignInRenderContext context, IonFormField ionFormField)
        {
            return GetIonFormFieldRenderResult(context, ionFormField);
        }

        public OktaStateModelRenderResult GetStateModelRenderResult(OktaSignInRenderContext context, IdentityState model)
        {
            return new OktaStateModelRenderResult(context, model);
        }

        public OktaRemediationRenderResult GetRemediationRenderResult(OktaSignInRenderContext context, Remediation remediation)
        {
            return new OktaRemediationRenderResult(context, remediation);
        }

        public OktaIonFormRenderResult GetIonFormRenderResult(OktaSignInRenderContext context, IonForm ionForm)
        {
            return new OktaIonFormRenderResult(context, ionForm);
        }

        public OktaIonFormFieldRenderResult GetIonFormFieldRenderResult(OktaSignInRenderContext context,IonFormField ionFormField)
        {
            return new OktaIonFormFieldRenderResult(context, ionFormField);
        }
    }
}
