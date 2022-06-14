using Newtonsoft.Json;
using Okta.IdentityEngine.Session;

namespace Okta.IdentityEngine.Client
{
    public class IdentitySession : IdentityResponse, IIdentitySession
    {
        public IdentitySession()
            : this(new SecureSessionProvider())
        {
        }

        internal IdentitySession(SecureSessionProvider sessionProvider)
        {
            this.SessionProvider = sessionProvider;
        }

        [JsonIgnore]
        internal IdentityResponse IdentityResponse { get; set; }

        [JsonIgnore]
        public IIdentitySessionProvider SessionProvider { get; set; }

        public string CodeVerifier { get; set; }

        public string CodeChallenge { get; set; }

        public string CodeChallengeMethod { get; set; }

        public string State { get; set; }

        public string ToJson(bool indented = false)
        {
            return JsonConvert.SerializeObject(this, indented ? Formatting.Indented : Formatting.None);
        }

        public void Save()
        {
            this.Save(this.SessionProvider);
        }

        public void Save(IIdentitySessionProvider sessionProvider)
        {
            if (this.SessionProvider != sessionProvider)
            {
                this.SessionProvider = sessionProvider;
            }

            sessionProvider.Set(State, ToJson());
        }

        public void Load(string state)
        {
            IdentitySession identitySession = Load(this.SessionProvider, state);
            this.CodeVerifier = identitySession.CodeVerifier;
            this.CodeChallenge = identitySession.CodeChallenge;
            this.CodeChallengeMethod = identitySession.CodeChallengeMethod;
            this.InteractionHandle = identitySession.InteractionHandle;
            this.State = identitySession.State;
        }

        public static IdentitySession Load(IIdentitySessionProvider sessionProvider, string state)
        {
            return sessionProvider.Get<IdentitySession>(state);
        }
    }
}
