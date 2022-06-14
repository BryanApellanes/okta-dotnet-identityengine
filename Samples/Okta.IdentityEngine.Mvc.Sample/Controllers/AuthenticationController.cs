using Microsoft.AspNetCore.Mvc;
using Okta.IdentityEngine.AspNetCore;
using Okta.IdentityEngine.Client;
using Okta.IdentityEngine.Mvc.Sample.Models;

namespace Okta.IdentityEngine.Mvc.Sample.Controllers
{
    public class AuthenticationController : Controller
    {
        private OktaMvcPlatform oktaMvcPlatform;

        public AuthenticationController(OktaMvcPlatform oktaPlatform)
        { 
            this.oktaMvcPlatform = oktaPlatform;
        }

        public async Task<IActionResult> SignIn()
        {
            return (await oktaMvcPlatform.GetSignInViewAsync(this)).Result;
        }

        public async Task<IActionResult> DefaultSignIn()
        {
            return (await oktaMvcPlatform.GetSignInViewAsync()).Result;
        }

    }
}
