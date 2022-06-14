﻿namespace Okta.IdentityEngine.Ion
{
    /// <summary>
    /// Models an `IonCollection` as a resource.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    public class CollectionResource<T> : CollectionResource
    {
    }

    public class CollectionResource : IonCollection, IIonResource
    {
    }
}
