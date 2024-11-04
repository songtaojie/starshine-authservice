using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starshine.Authservice.Application.Contracts.Dtos.Error
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {
        }

        public ErrorViewModel(string error)
        {
            Error = new ErrorMessage { Error = error };
        }

        public ErrorMessage Error { get; set; }
    }
}
