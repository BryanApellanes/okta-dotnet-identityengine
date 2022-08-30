using Okta.IdentityEngine.Configuration;

namespace Okta.IdentityEngine.AspNet
{
    public class MvcOktaIdentityEngineOptions : OktaIdentityEngineOptions
    {
        public MvcOktaIdentityEngineOptions()
        {
            this.OktaServiceProvider = AspNetServices.Mvc;
        }
    }
}
