// <copyright file="ISessionProvider.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.IdentityEngine.Data;
using Okta.IdentityEngine.Logging;

namespace Okta.IdentityEngine.Session
{
    public interface IIdentitySessionProvider
    {
        IIdentityLoggingProvider LoggingProvider { get; set; }

        IStorageProvider StorageProvider { get; set; }

        T Get<T>(string key);

        string Get(string key);

        void Set(string key, string value);
    }
}
