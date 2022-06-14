using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client.View
{
    public interface IRenderContext<TRenderContext, TPlatformResult> where TRenderContext: IRenderContext<TRenderContext, TPlatformResult>
    {
        IdentityState StateModel { get; set; }
        TPlatformResult? Result { get; set; }

        IIdentityViewProvider<TRenderContext, TPlatformResult> ViewProvider { get; }
        IIdentityStateRenderer<TRenderContext, TPlatformResult> Renderer { get; }
        //Task<IdentityRenderResult<TRenderContext, TPlatformResult>> RenderDefaultAsync();
        Task<IdentityRenderResult<TRenderContext, TPlatformResult>> RenderAsync();
        Task<IdentityRenderResult<TRenderContext, TPlatformResult>> RenderPartialAsync();
    }
}
