using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using MRS.Application.Interfaces;
using MRS.Infrastructure.Common.Commands;

namespace MRS.Website.Code
{
    public class BasePage : Page
    {
        public IRequestService RequestService;
        public ILookupService LookupService;

        new public UserPrincipal User
        {
            get
            {
                if (Request.IsAuthenticated)
                    return ((UserPrincipal)Context.User);
                return null;
            }
        }

        public BasePage()
        {
            RequestService = Global.GetInstance<IRequestService>();
            LookupService = Global.GetInstance<ILookupService>();
        }

        public ICommandHandler<T> GetCommandHandler<T>()
        {
            return Global.GetInstance<ICommandHandler<T>>();
        }
    }
}