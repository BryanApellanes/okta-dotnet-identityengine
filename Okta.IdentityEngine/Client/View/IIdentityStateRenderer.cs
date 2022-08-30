using Bam.Ion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client.View
{
    public interface IIdentityStateRenderer<TRenderContext, TResult> where TRenderContext : IRenderContext<TRenderContext, TResult>
    {
        Task<IdentityRenderResult<TRenderContext, TResult>> RenderStateModelAsync(TRenderContext renderContext, IdentityState model);

        Task<IdentityRenderResult<TRenderContext, TResult>> RenderRemediationAsync(TRenderContext renderContext, Remediation remediation);

        Task<IdentityRenderResult<TRenderContext, TResult>> RenderIonFormAsync(TRenderContext renderContext, IonForm ionForm);

        Task<IdentityRenderResult<TRenderContext, TResult>> RenderIonFormFieldAsync(TRenderContext renderContext, IonFormField ionFormField);
    }
}
