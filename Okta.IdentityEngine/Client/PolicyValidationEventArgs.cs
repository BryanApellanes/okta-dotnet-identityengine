using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client
{
    public class PolicyValidationEventArgs : EventArgs
    {
        public IdentityPipelineManager IdentityPipelineManager { get; set; }
        public IPolicyValidationResult Result { get; set; }
        public IIdentityIonResponse Response { get; set; }

        public Exception Exception { get; set; }
    }
}
