﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starshine.Authservice.Domain.DataSeeder
{
    public interface IClientDataSeeder
    {
        Task CreateClientsAsync();
    }
}
