namespace Okta.IdentityEngine.Client.View
{
    public interface IIdentityRenderResult<TRenderContext, TPlatformResult> where TRenderContext : IRenderContext<TRenderContext, TPlatformResult>
    {
        TPlatformResult Result { get; set; }
        IRenderContext<TRenderContext, TPlatformResult> RenderContext { get; set; }
    }
}