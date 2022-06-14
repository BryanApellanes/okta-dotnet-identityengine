using Okta.IdentityEngine.Ion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client
{
    public class RemediationTypeResolver
    {
        public Remediation GetRemediation(IonValueObject ionValueObject)
        {
            return new Remediation(ionValueObject);
        }
    }
}
