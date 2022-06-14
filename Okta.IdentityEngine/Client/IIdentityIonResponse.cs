// <copyright file="IIdentityIntrospection.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.IdentityEngine.Client.Data;
using Okta.IdentityEngine.Ion;

namespace Okta.IdentityEngine.Client
{
    public interface IIdentityIonResponse : IIdentityResponse
    {
        IIdentityPipeline Pipeline { get; set; }
        IonValueObject IonRoot { get; }

        List<Remediation> Remediations { get; }

        Authenticator CurrentAuthenticator { get; }

        List<Authenticator> Authentcators { get; }

        List<AuthenticatorEnrollment> AuthenticatorEnrollments { get; }

        AuthenticatorEnrollment EnrollmentAuthenticator { get; }

        User User { get; }

        IdentityRequest Cancel { get; }

        AppData App { get; }
    }
}
