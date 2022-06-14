// <copyright file="AuthenticationPipelineManager.cs" company="Okta, Inc">
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
using Okta.IdentityEngine.Configuration;

namespace Okta.IdentityEngine.Client
{
    public abstract class IdentityPipelineManager : IIdentityPipelineManager
    {
        Dictionary<string, IIdentityPipeline?> pipelines;

        public IdentityPipelineManager()
            : this(IdentityEngine.ServiceProvider.Default)
        {
        }

        public IdentityPipelineManager(IServiceProvider serviceProvider)
            : this(serviceProvider, serviceProvider.GetService<IIdentityClient>(), serviceProvider.GetService<IIdentityDataProvider>(), serviceProvider.GetService<IIdentityPolicyProvider>(), serviceProvider.GetService<IIdentitySessionProvider>(), serviceProvider.GetService<IStorageProvider>(), serviceProvider.GetService<IIdentityLoggingProvider>())
        {
        }

        public IdentityPipelineManager(IServiceProvider serviceProvider, IIdentityClient idxClient, IIdentityDataProvider dataProvider, IIdentityPolicyProvider policyProvider, IIdentitySessionProvider sessionProvider, IStorageProvider storageProvider, IIdentityLoggingProvider loggingProvider)
        {
            pipelines = new Dictionary<string, IIdentityPipeline?>();
            ServiceProvider = serviceProvider;
            IdentityClient = idxClient;
            DataProvider = dataProvider;
            SessionProvider = sessionProvider;
            StorageProvider = storageProvider;
            LoggingProvider = loggingProvider;
            PolicyProvider = policyProvider;
        }

        public IdentityPipelineManager(OktaIdentityEngineOptions options) : this(options.OktaServiceProvider)
        {
            Options = options;
        }

        protected OktaIdentityEngineOptions Options { get; set; }

        public event EventHandler<PipelineManagerEventArgs> PipelineStarting;

        public event EventHandler<PipelineManagerEventArgs> PipelineStartCompleted;

        public event EventHandler<PipelineManagerEventArgs> PipelineStartExceptionThrown;

        public event EventHandler<PipelineManagerEventArgs> PipelineContinuing;

        public event EventHandler<PipelineManagerEventArgs> PipelineContinueCompleted;

        public event EventHandler<PipelineManagerEventArgs> PipelineContinueExceptionThrown;

        public event EventHandler<PolicyValidationEventArgs> Validating;

        public event EventHandler<PolicyValidationEventArgs> ValidateCompleted;

        public event EventHandler<PolicyValidationEventArgs> ValidateExceptionThrown;

        public IServiceProvider ServiceProvider { get; }

        public IIdentityClient IdentityClient { get; }

        public IIdentityDataProvider DataProvider { get; }

        public IIdentityPolicyProvider PolicyProvider { get; }

        public IIdentitySessionProvider SessionProvider { get; }

        public IStorageProvider StorageProvider { get; }

        public IIdentityLoggingProvider LoggingProvider { get; }


        public Task<IIdentityPipeline?> GetPipelineAsync(string stateHandle)
        {
            if (pipelines.ContainsKey(stateHandle))
            {
                return Task.FromResult(pipelines[stateHandle]);
            }
            return Task.FromResult<IIdentityPipeline?>(null);
        }

        public async Task<IdentityState> ContinuePipelineAsync(string stateHandle)
        {
            IIdentityPipeline? pipeline = await GetPipelineAsync(stateHandle);
            if (pipeline != null)
            {
                return await ContinuePipelineAsync(pipeline);
            }
            throw new InvalidOperationException($"Pipeline for specified {nameof(stateHandle)} not found: {stateHandle}");
        }

        public async Task<IdentityState> ContinuePipelineAsync(IIdentityPipeline pipeline)
        {
            try
            {
                PipelineContinuing?.Invoke(this, new PipelineManagerEventArgs
                {
                    IdentityPipelineManager = this,
                });


                IdentityState pipelinestate = await DataProvider.GetIdentityPipelineState(pipeline.InteractionHandle);
                pipeline.AddResponse(pipelinestate);

                PipelineContinueCompleted?.Invoke(this, new PipelineManagerEventArgs
                {
                    IdentityPipelineManager = this,
                });

                return pipelinestate;
            }
            catch (Exception ex)
            {
                PipelineContinueExceptionThrown?.Invoke(this, new PipelineManagerEventArgs
                {
                    IdentityPipelineManager = this,
                    Exception = ex
                });
                return new IdentityState { Exception = ex };
            }
        }

        public Task<IdentityState> NextPipelineStateAsync(IdentityRequest identityRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityState> StartPipelineAsync()
        {
            try
            {
                PipelineStarting?.Invoke(this, new PipelineManagerEventArgs
                {
                    IdentityPipelineManager = this,
                });

                IIdentitySession session = await DataProvider.StartSessionAsync();
                _ = Task.Run(() => SessionProvider.Set(session.State, session.ToJson()));

                IdentityState pipelineState = await DataProvider.GetIdentityPipelineState(session.InteractionHandle);
                IIdentityPipeline pipeline = new IdentityPipeline(this, session);
                pipelineState.Pipeline = pipeline;
                pipelines.Add(session.State, pipeline);

                PipelineStartCompleted?.Invoke(this, new PipelineManagerEventArgs
                {
                    IdentityPipelineManager = this,
                    Session = session,
                    PipelineState = pipelineState,
                });

                pipeline.PolicyValidationResult = await ValidateStateAsync(pipelineState);

                return pipelineState;
            }
            catch (Exception ex)
            {
                PipelineStartExceptionThrown?.Invoke(this, new PipelineManagerEventArgs
                {
                    IdentityPipelineManager = this,
                    Exception = ex,
                });

                return new IdentityState { Exception = ex };
            }
        }

        public async Task<IPolicyValidationResult> ValidateStateAsync(IIdentityIonResponse identityIonResponse)
        {
            try
            {
                Validating?.Invoke(this, new PolicyValidationEventArgs
                {
                    IdentityPipelineManager = this,
                    Response = identityIonResponse
                });

                IPolicyValidationResult result = await PolicyProvider.ValidateStateAsync(identityIonResponse);

                ValidateCompleted?.Invoke(this, new PolicyValidationEventArgs
                {
                    IdentityPipelineManager = this,
                    Response = identityIonResponse,
                    Result = result,
                });

                return result;
            }
            catch (Exception ex)
            {
                ValidateExceptionThrown?.Invoke(this, new PolicyValidationEventArgs
                {
                    IdentityPipelineManager = this,
                    Response = identityIonResponse,
                    Exception = ex
                });

                return new PolicyValidationResult
                {
                    Success = false,
                    Exception = ex
                };
            }
        }
    }
}
