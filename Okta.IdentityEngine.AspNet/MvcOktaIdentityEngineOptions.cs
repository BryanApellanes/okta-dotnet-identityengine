using Okta.IdentityEngine.Configuration;

namespace Okta.IdentityEngine.AspNetCore
{
    public class MvcOktaIdentityEngineOptions : OktaIdentityEngineOptions
    {
        public MvcOktaIdentityEngineOptions()
        {
            this.OktaServiceProvider = AspNetServices.Mvc;
        }
    }
}
