using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client
{
    public class IdentityPipeline : IIdentityPipeline
    {
        public IdentityPipeline(IdentityPipelineManager identityPipelineManager, IIdentitySession session)
        {
            PipelineManager = identityPipelineManager;
            Session = session;
            responses = new List<IIdentityIonResponse>();
        }

        public string StateHandle
        {
            get => Session?.State ?? string.Empty;
        }

        public string InteractionHandle
        {
            get => Session?.InteractionHandle ?? string.Empty;
        }

        public IIdentityPipelineManager PipelineManager
        {
            get;
            private set;
        }

        public IIdentitySession? Session
        {
            get;
            set;
        }

        List<IIdentityIonResponse> responses;
        public IEnumerable<IIdentityIonResponse>? Responses
        {
            get => responses.ToArray();
        }

        public IIdentityIonResponse? Current
        {
            get;
            private set;
        }

        public IPolicyValidationResult? PolicyValidationResult
        {
            get;
            set;
        }

        public void AddResponse(IIdentityIonResponse response)
        {
            if (!response.Raw.Equals(Current?.Raw))
            {
                Current = response;
                responses.Add(response);
            }
        }
    }
}
