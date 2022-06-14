using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client
{
    public interface IRequestReader<T> : IRequestReader
    {
        new T Type { get; }
        new T ReadRequestStream(Stream stream);
        new T ReadRequest(string body);
    }
}
