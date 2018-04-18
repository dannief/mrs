using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MRS.Website.Code;

namespace MRS.Website
{
    public partial class Dashboard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var requests = RequestService.GetRequests(User.IDNumber);
            ucRequestList.LoadRequests(requests);
            ucWorkOrderList.LoadWorkOrders(requests);
        }
    }
}