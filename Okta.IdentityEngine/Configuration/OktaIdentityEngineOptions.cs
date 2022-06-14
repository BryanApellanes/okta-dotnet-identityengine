namespace Okta.IdentityEngine.Configuration
{
    public abstract class OktaIdentityEngineOptions
    {
        public OktaIdentityEngineOptions()
        {
            OktaServiceProvider = ServiceProvider.Default;
        }

        public IServiceProvider OktaServiceProvider { get; set; }

        public bool Debug { get; set; }
    }
}
