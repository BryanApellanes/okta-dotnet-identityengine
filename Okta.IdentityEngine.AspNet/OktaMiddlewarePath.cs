namespace Okta.IdentityEngine.AspNetCore
{
    public class OktaMiddlewarePath
    {
        public static implicit operator string(OktaMiddlewarePath path)
        {
            return path.Value;
        }

        public OktaMiddlewarePath() 
        {
            this.Value = "/oktaidentityengine";
        }

        public OktaMiddlewarePath(string path)
        {
            this.Value = path;
        }

        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
