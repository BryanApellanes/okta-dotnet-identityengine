namespace Okta.IdentityEngine.AspNet
{
    public abstract class OktaSignInViewRenderResult : OktaViewRenderResult
    {
        public OktaSignInViewRenderResult(OktaSignInRenderContext renderContext)
        {
            this.RenderContext = renderContext;
        }

        protected OktaSignInRenderContext RenderContext { get; private set; }
    }
}
