﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client
{
    public interface IPolicyValidationResult
    {
        bool Success { get; }
        string? Message { get; }
    }
}
