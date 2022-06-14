using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client
{
    public interface IRequestReader
    {
        Type Type { get; }
        object ReadRequestStream(Stream stream);
        object ReadRequest(string body);
    }
}
