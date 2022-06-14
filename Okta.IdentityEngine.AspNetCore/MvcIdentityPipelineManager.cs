using Microsoft.AspNetCore.Mvc;
using Okta.IdentityEngine.Client;
using Okta.IdentityEngine.Client.View;
using Okta.IdentityEngine.Configuration;

namespace Okta.IdentityEngine.AspNetCore
{
    public class MvcIdentityPipelineManager : IdentityPipelineManager
    {
        public MvcIdentityPipelineManager(): base(AspNetServices.Mvc)
        {
        }

        public MvcIdentityPipelineManager(MvcOktaIdentityEngineOptions options) : base(options)
        {
        }
    }
}
