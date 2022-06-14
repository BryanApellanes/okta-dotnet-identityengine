using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client
{
    public class IdentityPolicyProvider : IIdentityPolicyProvider
    {
        public Task<IPolicyValidationResult> ValidateStateAsync(IIdentityIonResponse identityIonResponse)
        {
            return Task.FromResult(ValidateState(identityIonResponse));
        }

        protected virtual IPolicyValidationResult ValidateState(IIdentityIonResponse identityIonResponse)
        {
            return new PolicyValidationResult { Success = true };
        }
    }
}
