using Starshine.Authservice.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;

namespace Starshine.Authservice.Domain
{
    /// <summary>
    /// 员工账户号信息
    /// </summary>
    public class IdentityAccount : IdentityUser
    {
        /// <summary>
        /// 0，保密，1：男，2：女
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; } = DateTime.Now;

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(100)]
        public string? Address { get; set; }
    }
}
