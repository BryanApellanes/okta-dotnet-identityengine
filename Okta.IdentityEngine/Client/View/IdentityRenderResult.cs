namespace Okta.IdentityEngine.Client.View
{
    public class IdentityRenderResult<TRenderContext, TPlatformResult> : IIdentityRenderResult<TRenderContext, TPlatformResult> where TRenderContext : IRenderContext<TRenderContext, TPlatformResult>
    {
        public IdentityRenderResult(TPlatformResult result)
        {
            Result = result;
        }

        public TPlatformResult Result
        {
            get;
            set;
        }

        public IRenderContext<TRenderContext, TPlatformResult> RenderContext { get; set; }
    }
}
