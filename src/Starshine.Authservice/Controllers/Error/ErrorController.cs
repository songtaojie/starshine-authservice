using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Starshine.Authservice.Application.Contracts.Dtos.Error;
using System.Threading.Tasks;

namespace Starshine.Authservice.Controllers.Error
{
    public class ErrorController : BaseController
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IHostEnvironment _environment;
        private readonly ILogger _logger;

        public ErrorController(IIdentityServerInteractionService interaction, IHostEnvironment environment, ILogger<ErrorController> logger)
        {
            _interaction = interaction;
            _environment = environment;
            _logger = logger;
        }
        public async Task<IActionResult> IndexAsync(string errorId)
        {
            var vm = new ErrorViewModel();

            // 从Identityserver检索错误详细信息
            var errorContext = await _interaction.GetErrorContextAsync(errorId);
            if (errorContext != null)
            {
                vm.Error = errorContext;
                if (!_environment.IsDevelopment())
                {
                    // 仅在开发中显示
                    vm.Error.ErrorDescription = null;
                }
            }

            return View(vm);
        }
    }
}
