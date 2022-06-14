using Okta.IdentityEngine.Client.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client.Model
{
    public class OktaPageModel<TRenderContext, TResult> where TRenderContext : IRenderContext<TRenderContext, TResult>
    {
        public TRenderContext RenderContext { get; set; }
    }
}
