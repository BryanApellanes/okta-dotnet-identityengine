// <copyright file="IIdentityInteraction.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using Okta.IdentityEngine.Session;

namespace Okta.IdentityEngine.Client
{
    public interface IIdentitySession : IIdentityResponse
    {
        IIdentitySessionProvider SessionProvider { get; set; }

        string State { get; set; }

        string CodeChallenge { get; set; }

        string CodeChallengeMethod { get; set; }

        string CodeVerifier { get; set; }

        void Load(string state);

        void Save();

        void Save(IIdentitySessionProvider secureSessionProvider);

        string ToJson(bool indented = false);
    }
}
