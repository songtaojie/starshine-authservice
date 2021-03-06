// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Hx.IdentityServer.Model.Consent;

namespace Hx.IdentityServer.Model.Device
{
    public class DeviceAuthorizationCreateModel : ConsentCreateModel
    {
        public string UserCode { get; set; }
    }
}