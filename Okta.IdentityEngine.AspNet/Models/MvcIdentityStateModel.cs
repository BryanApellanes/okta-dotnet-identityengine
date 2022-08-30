using Microsoft.AspNetCore.Mvc;
using Okta.IdentityEngine.Client.Model;

namespace Okta.IdentityEngine.AspNet.Models
{
    public class MvcIdentityStateModel : IdentityStateModel<OktaSignInRenderContext, IActionResult>
    {
    }
}
