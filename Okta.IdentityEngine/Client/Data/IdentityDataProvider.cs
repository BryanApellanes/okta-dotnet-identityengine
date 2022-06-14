// <copyright file="IdentityDataProvider.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Threading.Tasks;
using Okta.IdentityEngine.Client.View;
using Okta.IdentityEngine.Session;

namespace Okta.IdentityEngine.Client.Data
{
    public class IdentityDataProvider : IIdentityDataProvider
    {
        public event EventHandler<IdentityDataProviderEventArgs> PipelineStarting;

        public event EventHandler<IdentityDataProviderEventArgs> PipelineStarted;

        public event EventHandler<IdentityDataProviderEventArgs> PipelineStartExceptionThrown;

        public event EventHandler<IdentityDataProviderEventArgs> GetIdentityModelStarted;

        public event EventHandler<IdentityDataProviderEventArgs> GetIdentityModelCompleted;

        public event EventHandler<IdentityDataProviderEventArgs> GetIdentityModelExcpetionThrown;

        public IdentityDataProvider(IIdentityClient identityClient, IIdentitySessionProvider sessionProvider)
        {
            this.IdentityClient = identityClient;
            this.SessionProvider = sessionProvider;
        }

        public IIdentityClient IdentityClient { get; set; }

        public IIdentitySessionProvider SessionProvider { get; set; }

        public async Task<IIdentitySession> StartSessionAsync()
        {
            try
            {
                this.PipelineStarting?.Invoke(this, new IdentityDataProviderEventArgs
                {
                    DataProvider = this,
                });
                IIdentitySession identitySession = await this.IdentityClient.InteractAsync();
                identitySession.Save(this.SessionProvider);
                this.PipelineStarted?.Invoke(this, new IdentityDataProviderEventArgs
                {
                    DataProvider = this,
                });

                return identitySession;
            }
            catch (Exception ex)
            {
                this.PipelineStartExceptionThrown?.Invoke(this, new IdentityDataProviderEventArgs
                {
                    Exception = ex,
                });
                return new IdentitySession { Exception = ex };
            }
        }

        /// <summary>
        /// Gets the current state of the specified authentication pipeline by calling `instrospect`.
        /// </summary>
        /// <param name="interactionHandle"></param>
        /// <returns></returns>
        public async Task<IdentityState> GetIdentityPipelineState(string interactionHandle)
        {
            try
            {
                this.GetIdentityModelStarted?.Invoke(this, new IdentityDataProviderEventArgs
                {
                    DataProvider = this,
                });

                IdentityState model = await this.IdentityClient.IntrospectAsync(interactionHandle);

                this.GetIdentityModelCompleted?.Invoke(this, new IdentityDataProviderEventArgs
                {
                    DataProvider = this,
                    Form = model,
                });

                return model;
            }
            catch (Exception ex)
            {
                this.GetIdentityModelExcpetionThrown?.Invoke(this, new IdentityDataProviderEventArgs
                {
                    Exception = ex,
                });
                return new IdentityState { Exception = ex };
            }
        }
    }
}
