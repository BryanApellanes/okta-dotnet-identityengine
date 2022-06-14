using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine
{
    public interface IOktaResult<TPlatformResult>
    {
        TPlatformResult PlatformResult { get; set; }
    }
}
