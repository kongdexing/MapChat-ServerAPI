using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.ServiceModel.Activation;

namespace MapChatService
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            RegisterRoutes();
        }

        #region RouteServer方法

        /// <summary>
        /// 注册
        /// </summary>
        private void RegisterRoutes()
        {
            RouteTable.Routes.Add(new ServiceRoute("UserService", new WebServiceHostFactory(), typeof(UserService)));
        }

        #endregion

    }
}
