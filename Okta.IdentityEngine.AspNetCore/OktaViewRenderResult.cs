using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Okta.IdentityEngine.AspNetCore
{
    public abstract class OktaViewRenderResult : IActionResult
    {
        protected async Task WriteResponseOutputAsync(HttpResponse response, string contentType = "text/html")
        {
            response.ContentType = contentType;
            await response.WriteAsync(Render(GetId()));
        }

        protected abstract string GetId();

        public abstract string Render(string id);

        public virtual async Task ExecuteResultAsync(ActionContext context)
        {
            await WriteResponseOutputAsync(context.HttpContext.Response);
        }
    }
}
