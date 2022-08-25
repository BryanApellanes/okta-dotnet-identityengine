namespace Okta.IdentityEngine.AspNetCore
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
