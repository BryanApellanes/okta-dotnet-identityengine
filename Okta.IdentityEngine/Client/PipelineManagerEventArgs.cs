// <copyright file="FlowManagerEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace Okta.IdentityEngine.Client
{
    public class PipelineManagerEventArgs : EventArgs
    {
        public IIdentityPipelineManager IdentityPipelineManager { get; set; }

        public IIdentitySession Session { get; set; }

        public IIdentityIonResponse PipelineState { get; set; }

        public Exception Exception { get; set; }
    }
}
