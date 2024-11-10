// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Starshine.Authservice.Application.Contracts.Dtos.Consent;

namespace Starshine.Authservice.Application.Contracts.Dtos.Device
{
    public class DeviceAuthorizationCreateParamDto : ConsentCreateParamDto
    {
        public string UserCode { get; set; }
    }
}