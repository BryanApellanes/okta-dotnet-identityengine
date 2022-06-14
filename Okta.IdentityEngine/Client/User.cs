using Okta.IdentityEngine.Ion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client
{
    public class User : IonValueObject
    {
        public User(IonValueObject ionValueObject) : base(ionValueObject)
        { }
    }
}
