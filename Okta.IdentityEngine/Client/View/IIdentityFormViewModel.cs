// <copyright file="IIdentityFormViewModel.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.IdentityEngine.Client.View
{
    public interface IIdentityFormViewModel
    {
        IIdentityIonResponse Response { get; set; }

        Exception Exception { get; set; }

        bool IsException { get; }
    }
}
