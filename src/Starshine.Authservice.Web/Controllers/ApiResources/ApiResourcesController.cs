//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using Starshine.Authservice.Application.Contracts.Dtos.ApiResource;
//using Starshine.Authservice.Entity.Consts;
//using Starshine.Authservice.Application.Contracts.Services;

//namespace Starshine.Authservice.Controllers.ApiResources
//{
//    [Route("apiresource/[action]")]
//    [ApiController]
//    public class ApiResourcesController : BaseController
//    {
//        private readonly IApiResourcesService _apiResourcesService;

//        public ApiResourcesController(IApiResourcesService apiResourcesService)
//        {
//            _apiResourcesService = apiResourcesService;
//        }

//        /// <summary>
//        /// 数据列表页
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        [Authorize]
//        [Route("~/apiresource")]
//        public IActionResult Index()
//        {
//            ViewData["IsSuperAdmin"] = IsSuperAdmin;
//            return View();
//        }

//        /// <summary>
//        /// 数据列表
//        /// </summary>
//        /// <param name="param"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [Authorize]
//        public async Task<IActionResult> GetPage(ApiResourcePageParamDto param)
//        {
//            var result = await _apiResourcesService.GetPage(param);
//            return Success(result);
//        }

//        /// <summary>
//        /// 获取用户名明细信息
//        /// </summary>
//        /// <param name="id">用户id</param>
//        /// <returns></returns>
//        [HttpGet("{id}")]
//        public async Task<IActionResult> Get(string id)
//        {
//            var result = await _apiResourcesService.GetById(id);
//            return Success(result);
//        }

//        /// <summary>
//        /// 数据修改编辑
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        [Authorize(Policy = ConstKey.SuperAdmin)]
//        public async Task<IActionResult> AddOrUpdate(ApiResourceCreateParamDto request)
//        {
//            if (request == null) return Error("请求参数不正确");
//            var result = await _apiResourcesService.AddOrUpdate(request);
//            return Success(result);
//        }

//        /// <summary>
//        /// 数据修改编辑
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost("{id}")]
//        [Authorize(Policy = ConstKey.SuperAdmin)]
//        public async Task<IActionResult> Delete(string id)
//        {
//            var result = await _apiResourcesService.DeleteById(id);
//            return Success(result);
//        }

//    }
//}
