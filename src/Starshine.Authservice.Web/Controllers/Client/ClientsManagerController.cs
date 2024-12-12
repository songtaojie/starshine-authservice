//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using Starshine.Authservice.Application.Contracts.Dtos.ClientManager;
//using Starshine.Authservice.Application.Contracts.Services;
//using Starshine.Authservice.Entity.Consts;

//namespace Starshine.Authservice.Controllers.Client
//{
//    [Route("client/[action]")]
//    [ApiController]
//    [Authorize]
//    public class ClientsManagerController : BaseController
//    {
//        private readonly IClientsManagerService _clientsManagerService;

//        public ClientsManagerController(IClientsManagerService clientsManagerService)
//        {
//            _clientsManagerService = clientsManagerService;
//        }

//        /// <summary>
//        /// 数据列表页
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        [Authorize]
//        [Route("~/client")]
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
//        public async Task<IActionResult> GetPage(ClientPageParamDto param)
//        {
//            var result = await _clientsManagerService.GetPage(param);
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
//            var result = await _clientsManagerService.GetById(id);
//            return Success(result);
//        }


//        /// <summary>
//        /// 数据修改编辑
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        [Authorize(Policy = ConstKey.SuperAdmin)]
//        public async Task<IActionResult> AddOrUpdate(ClientCreateParamDto request)
//        {
//            if (request == null) return Error("请求参数不正确");
//            var result = await _clientsManagerService.AddOrUpdate(request);
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
//            var result = await _clientsManagerService.DeleteById(id);
//            return Success(result);
//        }


//        #region 作用域和允许跨域
//        /// <summary>
//        /// 数据列表页
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        [Authorize]
//        [Route("~/client/sc")]
//        public IActionResult ScIndex()
//        {
//            ViewData["IsSuperAdmin"] = IsSuperAdmin;
//            return View();
//        }

//        /// <summary>
//        /// 作用域和允许跨域的数据列表
//        /// </summary>
//        /// <param name="param"></param>
//        /// <returns></returns>
//        [HttpPost]
//        public async Task<IActionResult> GetScPage(ClientPageParamDto param)
//        {
//            var result = await _clientsManagerService.GetScPage(param);
//            return Success(result);
//        }

//        /// <summary>
//        /// 获取作用域/允许跨域详情数据
//        /// </summary>
//        /// <param name="id">客户端id</param>
//        /// <returns></returns>
//        [HttpGet("{clientId}")]
//        public async Task<IActionResult> GetSc(string clientId)
//        {
//            var result = await _clientsManagerService.GetScById(clientId);
//            return Success(result);
//        }

//        /// <summary>
//        /// 数据修改编辑
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        [Authorize(Policy = ConstKey.SuperAdmin)]
//        public async Task<IActionResult> UpdateSc(ClientScCreateParamDto request)
//        {
//            if (request == null) return Error("请求参数不正确");
//            var result = await _clientsManagerService.UpdateSc(request);
//            return Success(result);
//        }
//        #endregion

//        #region 回调地址/退出回调

//        /// <summary>
//        /// 数据列表页
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        [Authorize]
//        [Route("~/client/ru")]
//        public IActionResult RuIndex()
//        {
//            ViewData["IsSuperAdmin"] = IsSuperAdmin;
//            return View();
//        }

//        /// <summary>
//        /// 作用域和允许跨域的数据列表
//        /// </summary>
//        /// <param name="param"></param>
//        /// <returns></returns>
//        [HttpPost]
//        public async Task<IActionResult> GetRuPage(ClientPageParamDto param)
//        {
//            var result = await _clientsManagerService.GetRuPage(param);
//            return Success(result);
//        }

//        /// <summary>
//        /// 获取作用域/允许跨域详情数据
//        /// </summary>
//        /// <param name="id">客户端id</param>
//        /// <returns></returns>
//        [HttpGet("{clientId}")]
//        public async Task<IActionResult> GetRu(string clientId)
//        {
//            var result = await _clientsManagerService.GetRuById(clientId);
//            return Success(result);
//        }

//        /// <summary>
//        /// 数据修改编辑
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        [Authorize(Policy = ConstKey.SuperAdmin)]
//        public async Task<IActionResult> UpdateRu(ClientRuCreateParamDto request)
//        {
//            if (request == null) return Error("请求参数不正确");
//            var result = await _clientsManagerService.UpdateRu(request);
//            return Success(result);
//        }

//        #endregion
//    }
//}
