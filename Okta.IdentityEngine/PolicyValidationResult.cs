using Okta.IdentityEngine.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine
{
    public class PolicyValidationResult : IPolicyValidationResult
    {
        public bool Success
        {
            get;
            internal set;
        }

        public string? Message
        {
            get => Exception?.Message;
        }

        public Exception? Exception { get; set; }
    }
}
