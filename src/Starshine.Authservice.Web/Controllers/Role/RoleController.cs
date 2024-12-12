//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Threading.Tasks;
//using Starshine.Authservice.Application.Contracts.Dtos.Role;
//using Starshine.Authservice.Application.Contracts.Services;
//using Starshine.Authservice.Entity.Consts;

//namespace Starshine.Authservice.Controllers.Role
//{
//    /// <summary>
//    /// 角色管理
//    /// </summary>
//    [Route("role/[action]")]
//    [ApiController]
//    [Authorize]
//    public class RoleController : BaseController
//    {
//        private readonly IRoleService _roleService;

//        public RoleController(IRoleService roleService)
//        {
//            _roleService = roleService;
//        }

//        /// <summary>
//        /// 数据列表页
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        [Route("~/role")]
//        public IActionResult Index()
//        {
//            ViewData["IsSuperAdmin"] = IsSuperAdmin;
//            return View();
//        }

//        /// <summary>
//        /// 数据列表，不分页的
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        public async Task<IActionResult> GetList()
//        {
//            var result = await _roleService.GetList();
//            return Success(result);
//        }

//        /// <summary>
//        /// 数据列表
//        /// </summary>
//        /// <param name="param"></param>
//        /// <returns></returns>
//        [HttpPost]
//        public async Task<IActionResult> GetPage(RolePageParamDto param)
//        {
//            //没有过滤的记录数
//            var result = await _roleService.GetPage(param);
//            return Success(result);
//        }
//        /// <summary>
//        /// 获取用户名明细信息
//        /// </summary>
//        /// <param name="id">用户id</param>
//        /// <returns></returns>
//        [HttpGet("{roleId}")]
//        public async Task<IActionResult> Get(string roleId)
//        {
//            var result = await _roleService.GeById(roleId);
//            return Success(result);
//        }

//        /// <summary>
//        /// 数据修改编辑
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        [Authorize(Policy = ConstKey.SuperAdmin)]
//        public async Task<IActionResult> Create(RoleCreateParamDto request)
//        {
//            if (request == null) return Error("请求参数不正确");
//            var result = await _roleService.Create(request);
//            return Success(result);
//        }

//        /// <summary>
//        /// 数据修改编辑
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        [Authorize(Policy = ConstKey.SuperAdmin)]
//        public async Task<IActionResult> Update(RoleCreateParamDto request)
//        {
//            if (request == null) return Error("请求参数不正确");
//            var result = await _roleService.Update(request);
//            return Success(result);
//        }

//        /// <summary>
//        /// 数据修改编辑
//        /// </summary>
//        /// <returns></returns>
//        [HttpDelete("{id}")]
//        [Authorize(Policy = ConstKey.SuperAdmin)]
//        public async Task<IActionResult> Delete(string id)
//        {
//            try
//            {
//                var result = await _roleService.DeleteById(id);
//                return Success(result);
//            }
//            catch (Exception e)
//            {
//                return Error(e.Message);
//            }
//        }

//    }
//}
