using Okta.IdentityEngine.Client.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client.Model
{
    public class IdentityStateModel<TRenderContext, TResult> : OktaPageModel<TRenderContext, TResult> where TRenderContext : IRenderContext<TRenderContext, TResult>
    {
        public IdentityState IdentityState { get; set; }
    }
}
