// <copyright file="IIdentityClientConfigurationProvider.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.IdentityEngine.Client;

namespace Okta.IdentityEngine.Configuration
{
    public interface IIdentityClientConfigurationProvider
    {
        IdentityClientConfiguration GetConfiguration();
    }
}
