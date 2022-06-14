using Okta.IdentityEngine.Ion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client.View
{
    public abstract class IdentityStateRenderer<TRenderContext, TResult> : IIdentityStateRenderer<TRenderContext, TResult> where TRenderContext : IRenderContext<TRenderContext, TResult>
    {
        protected abstract TResult RenderStateModel(TRenderContext context, IdentityState model);
        protected abstract TResult RenderRemediation(TRenderContext context, Remediation remediation);
        protected abstract TResult RenderIonForm(TRenderContext context, IonForm ionForm);
        protected abstract TResult RenderIonFormField(TRenderContext context, IonFormField ionFormField);

        public virtual async Task<IdentityRenderResult<TRenderContext, TResult>> RenderStateModelAsync(TRenderContext renderContext, IdentityState model)
        {
            return await Task.FromResult(new IdentityRenderResult<TRenderContext, TResult>(RenderStateModel(renderContext, renderContext.StateModel)) { RenderContext = renderContext });
        }

        public virtual async Task<IdentityRenderResult<TRenderContext, TResult>> RenderRemediationAsync(TRenderContext renderContext, Remediation remediation)
        {
            return await Task.FromResult(new IdentityRenderResult<TRenderContext, TResult>(RenderRemediation(renderContext, remediation)) { RenderContext = renderContext });
        }

        public async Task<IdentityRenderResult<TRenderContext, TResult>> RenderIonFormAsync(TRenderContext renderContext, IonForm ionForm)
        {
            return await Task.FromResult(new IdentityRenderResult<TRenderContext, TResult>(RenderIonForm(renderContext, ionForm)) { RenderContext = renderContext });
        }

        public async Task<IdentityRenderResult<TRenderContext, TResult>> RenderIonFormFieldAsync(TRenderContext renderContext, IonFormField ionFormField)
        {
            return await Task.FromResult(new IdentityRenderResult<TRenderContext, TResult>(RenderIonFormField(renderContext, ionFormField)) { RenderContext = renderContext });
        }
    }
}
