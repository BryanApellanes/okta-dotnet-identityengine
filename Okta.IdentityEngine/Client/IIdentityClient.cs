// <copyright file="IIdentityClient.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Threading.Tasks;

namespace Okta.IdentityEngine.Client
{
    public interface IIdentityClient
    {
        IdentityClientConfiguration Configuration { get; set; }

        Task<IIdentitySession> InteractAsync(IIdentitySession state = null);

        Task<IdentityState> IntrospectAsync(IIdentitySession state);

        Task<IdentityState> IntrospectAsync(string interactionHandle);

        Task<TokenResponse> RedeemInteractionCodeAsync(IIdentitySession identitySession, string interactionCode);
    }
}
