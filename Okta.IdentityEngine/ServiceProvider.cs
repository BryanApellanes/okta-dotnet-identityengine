// <copyright file="ServiceProvider.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.IdentityEngine.Data;
using Okta.IdentityEngine.Client;
using Okta.IdentityEngine.Client.Data;
using Okta.IdentityEngine.Client.View;
using Okta.IdentityEngine.Logging;
using Okta.IdentityEngine.Session;
using Okta.IdentityEngine.Ioc;

namespace Okta.IdentityEngine
{
    public class ServiceProvider : IServiceProvider
    {
        private static ServiceProvider? defaultServiceProvider;

        private static readonly object defaultLock = new object();

        private readonly TinyIoCContainer container = new TinyIoCContainer();

        public static ServiceProvider Default
        {
            get
            {
                if (defaultServiceProvider == null)
                {
                    lock (defaultLock)
                    {
                        if (defaultServiceProvider == null)
                        {
                            ServiceProvider temp = new ServiceProvider();

                            temp.RegisterService<IServiceProvider>(temp);
                            temp.RegisterService<IIdentityClient>(new IdentityClient());
                            temp.RegisterService<IIdentityViewModelProvider, IdentityViewModelProvider>();
                            temp.RegisterService<IIdentityPolicyProvider>(new IdentityPolicyProvider());
                            temp.RegisterService<IIdentitySessionProvider>(new SecureSessionProvider());
                            temp.RegisterService<IIdentityLoggingProvider>(new IdentityLoggingProvider());
                            temp.RegisterService<IStorageProvider, FileStorageProvider>();
                            temp.RegisterService<IIdentityDataProvider, IdentityDataProvider>();
                            defaultServiceProvider = temp;
                        }
                    }
                }

                return defaultServiceProvider;
            }
        }

        public IType GetService<IType>()
            where IType : class
        {
            return this.container.Resolve<IType>();
        }

        public void RegisterService<IType, CType>()
            where IType : class
            where CType : class, IType
        {
            this.container.Register<IType, CType>();
        }

        public void RegisterService<IType>(IType implementation)
            where IType : class
        {
            this.container.Register<IType>(implementation);
        }
    }
}
