using Newtonsoft.Json;
using Bam.Ion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client
{
    public class Remediation : IdentityRequest
    {
        public Remediation(IonValueObject valueObject) : base(valueObject) 
        {
            this.Form = valueObject.AsForm();
        }

        public string? Name => GetMemberValueToString("name");
        public string? Produces => GetMemberValueToString("produces");

        public IonForm Form
        {
            get;
            private set;
        }
    }
}
