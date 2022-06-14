// <copyright file="IViewProvider.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.IdentityEngine.Client.View
{
    public interface IIdentityViewProvider<TRenderContext, TResult> where TRenderContext: IRenderContext<TRenderContext, TResult>
    {
        IIdentityStateRenderer<TRenderContext, TResult> DefaultRenderer { get; }
/*        Task<IdentityRenderResult<TRenderContext, TResult>> GetDefaultRenderResultAsync(TRenderContext renderContext);
        Task<IdentityRenderResult<TRenderContext, TResult>> GetDefaultPartialRenderResultAsync(TRenderContext renderContext);*/
        Task<IdentityRenderResult<TRenderContext, TResult>> GetRenderResultAsync(TRenderContext renderContext);
        Task<IdentityRenderResult<TRenderContext, TResult>> GetPartialRenderResultAsync(TRenderContext renderContext);
    }
}
