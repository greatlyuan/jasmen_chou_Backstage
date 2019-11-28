using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jasmen_chou_Backstage.Models
{
    public class UserInfo
    {
        /// <summary>
        /// 姓
        /// </summary>
        [Required(ErrorMessage = "*必填项")]
        [StringLength(5, ErrorMessage = "*超过长度了")]
        public string FirstName { get; set; }
        /// <summary>
        /// 名
        /// </summary>
        [Required(ErrorMessage = "*必填项")]
        [StringLength(5, ErrorMessage = "*超过长度了")]
        public string LastName { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [Required(ErrorMessage = "*必填项")]
        [RegularExpression(@"^[1]+[3,5]+\d{9}", ErrorMessage = "*手机号码格式错误")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Required(ErrorMessage = "*必填项")]
        [RegularExpression(@"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}$", ErrorMessage = "*邮箱格式错误")]
        public string Eamil { get; set; }
        /// <summary>
        /// 留言
        /// </summary>
        public string Messages { get; set; }
    }
}