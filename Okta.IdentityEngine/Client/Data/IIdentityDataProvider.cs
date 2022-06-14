// <copyright file="IIdentityDataProvider.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Threading.Tasks;
using Okta.IdentityEngine.Client.View;
using Okta.IdentityEngine.Session;

namespace Okta.IdentityEngine.Client.Data
{
    public interface IIdentityDataProvider
    {
        event EventHandler<IdentityDataProviderEventArgs> PipelineStarting;

        event EventHandler<IdentityDataProviderEventArgs> PipelineStarted;
                
        event EventHandler<IdentityDataProviderEventArgs> PipelineStartExceptionThrown;

        IIdentitySessionProvider SessionProvider { get; set; }

        Task<IIdentitySession> StartSessionAsync();

        Task<IdentityState> GetIdentityPipelineState(string interactionHandle);
    }
}
