namespace Okta.IdentityEngine.AspNet
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseOktaIdentityEngine(this IApplicationBuilder app)
        {
            app.UseMiddleware<OktaMiddleware>();
            return app;
        }
    }
}
