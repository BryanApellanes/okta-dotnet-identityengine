using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client
{
    public class PolicyValidationResult : IPolicyValidationResult
    {
        public bool Success
        {
            get;
            set;
        }

        string? message;
        public string? Message
        {
            get
            {
                if (string.IsNullOrEmpty(message))
                {
                    if (Exception != null)
                    {
                        message = Exception.Message;
                    }
                }
                return message;
            }
            set
            {
                message = value;
            }
        }

        public Exception? Exception
        {
            get;
            set;
        }
    }
}
