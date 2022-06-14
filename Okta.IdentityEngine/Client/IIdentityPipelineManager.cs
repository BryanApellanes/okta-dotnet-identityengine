// <copyright file="IFlowManager.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Threading.Tasks;
using Okta.IdentityEngine.Data;
using Okta.IdentityEngine.Client.Data;
using Okta.IdentityEngine.Logging;
using Okta.IdentityEngine.Session;
using Okta.IdentityEngine.Client.View;

namespace Okta.IdentityEngine.Client
{
    public interface IIdentityPipelineManager 
    {
        event EventHandler<PipelineManagerEventArgs> PipelineStarting;

        event EventHandler<PipelineManagerEventArgs> PipelineStartCompleted;

        event EventHandler<PipelineManagerEventArgs> PipelineStartExceptionThrown;

        event EventHandler<PipelineManagerEventArgs> PipelineContinuing;

        event EventHandler<PipelineManagerEventArgs> PipelineContinueCompleted;

        event EventHandler<PipelineManagerEventArgs> PipelineContinueExceptionThrown;

        event EventHandler<PolicyValidationEventArgs> Validating;

        event EventHandler<PolicyValidationEventArgs> ValidateCompleted;

        event EventHandler<PolicyValidationEventArgs> ValidateExceptionThrown;

        IIdentityClient IdentityClient { get; }

        IIdentityDataProvider DataProvider { get; }

        IIdentityPolicyProvider PolicyProvider { get; }

        IIdentitySessionProvider SessionProvider { get; }

        IStorageProvider StorageProvider { get; }

        IIdentityLoggingProvider LoggingProvider { get; }

        Task<IdentityState> StartPipelineAsync();

        Task<IIdentityPipeline?> GetPipelineAsync(string stateHandle);

        Task<IdentityState> ContinuePipelineAsync(string stateHandle);

        Task<IdentityState> ContinuePipelineAsync(IIdentityPipeline identitySession);

        Task<IdentityState> NextPipelineStateAsync(IdentityRequest idxRequest);

        Task<IPolicyValidationResult> ValidateStateAsync(IIdentityIonResponse identityIonResponse);
    }
}
