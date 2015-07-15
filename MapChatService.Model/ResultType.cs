using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MapChatService.Model
{
    public enum ResultType
    {
        //公共的 
        [Description("未知错误")]
        unknowError = 0,

        [Description("成功")]
        success = 1,

        [Description("验证失败")]
        validateFailed = 2,

        [Description("token为空")]
        tokenIsNull = 3,

        [Description("密码验证失败")]
        validatePWDFailed = 4,

        [Description("未找到")]
        NotFound = 5,


        /// <summary>
        /// 登录用户
        /// </summary>
        [Description("不存在该用户")]
        MemberNotFound = 101,
        [Description("用户名或密码错误")]
        nameOrPwdIsError = 102,

        /// <summary>
        /// 注册用户
        /// </summary>
        [Description("该用户名已经存在")]
        userIsExist = 103,

        [Description("该邮箱已被注册")]
        EmailIsExist = 104,

        [Description("该手机号码已被注册")]
        PhoneIsExist = 105,

        /// <summary>
        /// 修改密码
        /// </summary>
        [Description("密码修改失败")]
        MemberUpdatePWDFail = 107,

    }
}
