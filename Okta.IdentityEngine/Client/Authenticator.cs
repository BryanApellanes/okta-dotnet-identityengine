using Okta.IdentityEngine.Ion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client
{
    public class Authenticator : IonValueObject
    {
        public Authenticator(IonValueObject ionValueObject) : base(ionValueObject)
        { 
        }
    }
}
