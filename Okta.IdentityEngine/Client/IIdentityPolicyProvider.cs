namespace Okta.IdentityEngine.Client
{
    public interface IIdentityPolicyProvider
    {
        // TODO:
        // - define a dev ApplicationConfigurationPolicyProvider
        //  - on application startup
        //      - ensure group exists
        //      - ensure enrollment policy exists -> "add user to group"
        //      - assign group to app
        //      - assign app to policy
        // - define a prod StateValidationPolicyProvider

        // TODO: add events

        Task<IPolicyValidationResult> ValidateStateAsync(IIdentityIonResponse identityIonResponse);
    }
}
