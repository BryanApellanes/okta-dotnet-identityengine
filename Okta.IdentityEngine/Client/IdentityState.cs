// <copyright file="IdentityIntrospection.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.IdentityEngine.Client;
using Okta.IdentityEngine.Client.Data;
using Okta.IdentityEngine.Ion;
using System.Net.Http;

namespace Okta.IdentityEngine.Client
{
    /// <summary>
    /// Represents the authentication state data model.
    /// </summary>
    public class IdentityState : IdentityResponse, IIdentityIonResponse
    {
        public IdentityState() 
        {
            this.RemediationTypeResolver = new RemediationTypeResolver();
        }

        internal IdentityState(HttpResponseMessage responseMessage)
            : base(responseMessage)
        {
            this.RemediationTypeResolver = new RemediationTypeResolver();
        }

        protected RemediationTypeResolver RemediationTypeResolver { get; set; }

        public IonValueObject IonRoot => IonValueObject.ReadObject(Raw);

        List<Remediation> remediations;
        public List<Remediation> Remediations // TODO: define RemediationCollection class
        {
            get
            {
                if (remediations == null)
                {
                    remediations = IonRoot["remediation"].CollectionValue().Select(ionValue => RemediationTypeResolver.GetRemediation(ionValue)).ToList();
                }
                return remediations;
            }
        }

        Authenticator currentAuthenticator;
        public Authenticator CurrentAuthenticator
        {
            get
            {
                if (currentAuthenticator == null)
                {
                    currentAuthenticator = new Authenticator(IonRoot["currentAuthenticator"].ValueObject());
                }
                return currentAuthenticator;
            }
        }

        List<Authenticator> authenticators;
        public List<Authenticator> Authentcators
        {
            get
            {
                if (authenticators == null)
                {
                    authenticators = IonRoot["authenticators"].CollectionValue().Select(ionValue => new Authenticator(ionValue)).ToList();
                }
                return authenticators;
            }
        }

        List<AuthenticatorEnrollment> enrollments;
        public List<AuthenticatorEnrollment> AuthenticatorEnrollments
        {
            get
            {
                if (enrollments == null)
                {
                    enrollments = IonRoot["authenticatorEnrollments"].CollectionValue().Select(ionValue => new AuthenticatorEnrollment(ionValue)).ToList();
                }
                return enrollments;
            }
        }

        AuthenticatorEnrollment enrollmentAuthenticator;
        public AuthenticatorEnrollment EnrollmentAuthenticator
        {
            get
            {
                if (enrollmentAuthenticator == null)
                {
                    enrollmentAuthenticator = new AuthenticatorEnrollment(IonRoot["enrollmentAuthenticator"].ValueObject());
                }
                return enrollmentAuthenticator;
            }
        }

        User user;
        public User User
        {
            get
            {
                if (user == null)
                {
                    user = new User(IonRoot["user"].ValueObject());
                }
                return user;
            }
        }

        IdentityRequest identityRequest;
        public IdentityRequest Cancel
        {
            get
            {
                if (identityRequest == null)
                {
                    identityRequest = new IdentityRequest(IonRoot["cancel"].ValueObject());
                }
                return identityRequest;
            }
        }

        AppInfo appInfo;
        public AppInfo AppInfo
        {
            get
            {
                if (appInfo == null)
                {
                    appInfo = new AppInfo(IonRoot["app"].ValueObject());
                }
                return appInfo;
            }
        }

        AppData appData;
        public AppData App
        {
            get
            {
                if (appData == null)
                {
                    appData = IonRoot["app"].ValueObject().ToInstance<AppData>();
                }
                return appData;
            }
        }

        public IIdentityPipeline Pipeline { get; set; }
    }
}
