using Okta.IdentityEngine.Client;

namespace Okta.IdentityEngine.AspNet
{
    /// <summary>
    /// Middleware responsible for receiving form posts from rendered sign in forms.
    /// </summary>
    public class OktaMiddleware : IMiddleware
    {
        public OktaMiddleware(OktaMiddlewarePath path, IIdentityPipelineManager identityPipelineManager, MvcOktaIdentityEngineOptions options)
        {
            this.Path = path.Value;
            this.IdentityPipelineManager = identityPipelineManager;
            this.Options = options;            
        }

        private async Task<bool> Execute(HttpContext context)
        {
            StreamReader streamReader = new StreamReader(context.Request.Body);
            //context.Request.Form
            string body = await streamReader.ReadToEndAsync();
            //IdentityPipelineManager.NextPipelineStateAsync(new IdentityRequest)
            await context.Response.WriteAsync($"<h1>Okta Identity Engine: path = {Path}</h1>");
            return false;
        }

        protected IIdentityPipelineManager IdentityPipelineManager { get; private set; }

        public string Path { get; private set; }
        public MvcOktaIdentityEngineOptions Options { get; private set; }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            bool executeNext = true;
            if (context.Request.Path.StartsWithSegments(this.Path))
            {
                executeNext = await Execute(context);
            }
            if (executeNext)
            {
                await next(context);
            }
        }
    }
}
