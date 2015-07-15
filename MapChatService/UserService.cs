using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using System.ServiceModel;
using System.ServiceModel.Web;
using MapChatService.Model;
using MapChatService.BLL;
using io.rong;
using MapChatService.Model.ReceivedModel;

namespace MapChatService
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class UserService
    {
        String appKey = "cpj2xarlj5k2n";
        String appSecret = "Ka1qL3cjh2";
        [OperationContract]
        [WebInvoke(UriTemplate = "Register", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public RetModel Register(UserInfo user)
        {
            Sys_UserInfoBLL bll = new Sys_UserInfoBLL();
            Sys_UserInfo model = new Sys_UserInfo()
            {
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Phone = user.Phone
            };
            int result = bll.Add(model);
            RetModel retmodel = new RetModel();
            switch (result)
            {
                case 0:
                    retmodel.StatusCode = Convert.ToInt32(ResultType.userIsExist).ToString();
                    break;
                case -1:
                    retmodel.StatusCode = Convert.ToInt32(ResultType.EmailIsExist).ToString();
                    break;
                case -2:
                    retmodel.StatusCode = Convert.ToInt32(ResultType.PhoneIsExist).ToString();
                    break;
                default:
                    retmodel.StatusCode = Convert.ToInt32(ResultType.success).ToString();
                    retmodel.RetValue = RongCloudServer.GetToken(appKey, appSecret, model.ID, model.UserName, "http://www.qqw21.com/article/UploadPic/2012-11/201211259378560.jpg");
                    break;
            }
            return retmodel;
        }
        [OperationContract]
        [WebGet(UriTemplate = "Login/{UserName}/{Password}", ResponseFormat = WebMessageFormat.Json)]
        public RetModel Login(string username, string password)
        {
            Sys_UserInfoBLL bll = new Sys_UserInfoBLL();
            int result = 0;
            string userid = "";
            result = bll.Login(username, password, ref userid);
            RetModel retmodel = new RetModel();
            switch (result)
            {
                case 0:
                    retmodel.StatusCode = Convert.ToInt32(ResultType.MemberNotFound).ToString();
                    break;
                case -1:
                    retmodel.StatusCode = Convert.ToInt32(ResultType.nameOrPwdIsError).ToString();
                    break;
                default:
                    retmodel.StatusCode = Convert.ToInt32(ResultType.success).ToString();
                    retmodel.RetValue = RongCloudServer.GetToken(appKey, appSecret, userid, username, "");
                    break;
            }
            return retmodel;
        }
    }
}