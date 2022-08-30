// <copyright file="IdentityRequest.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Bam.Ion;

namespace Okta.IdentityEngine.Client
{
    public class IdentityRequest : IonLink
    {
        public IdentityRequest(IonValueObject ionValueObject): base(ionValueObject)
        {
        }
    }
}
