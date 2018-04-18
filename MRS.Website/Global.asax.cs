using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using MRS.Infrastructure.DI;
using MRS.Application.Interfaces;
using MRS.Website.Code;

namespace MRS.Website
{
    public class Global : System.Web.HttpApplication
    {
        private static IContainer diContainer;

        protected void Application_Start(object sender, EventArgs e)
        {
            Global.diContainer = new SimpleInjectorDIContainer();            
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                var formsIdentity = Context.User.Identity as FormsIdentity;  
                var userPrincial = new UserPrincipal(formsIdentity,  GetInstance<IRequestService>());
                Context.User = userPrincial;
                Thread.CurrentPrincipal = userPrincial;
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        public static T GetInstance<T>() where T : class
        {
            return diContainer.GetInstance<T>();
        }
    }
}