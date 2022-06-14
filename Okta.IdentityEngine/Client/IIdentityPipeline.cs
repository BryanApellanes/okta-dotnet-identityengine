using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client
{
    public interface IIdentityPipeline
    {
        string StateHandle { get; }

        string InteractionHandle { get; }

        IIdentityPipelineManager PipelineManager { get; }

        IIdentitySession? Session { get; set; }

        IEnumerable<IIdentityIonResponse>? Responses { get; }

        IIdentityIonResponse? Current { get; }

        IPolicyValidationResult? PolicyValidationResult { get; set; }

        void AddResponse(IIdentityIonResponse response);
    }
}
