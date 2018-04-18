using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MRS.DataTransferObjects;

namespace MRS.Website.UserControls
{
    public partial class WorkOrderList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadWorkOrders(ICollection<RequestListItemDto> requests)
        {

            gvWorkOrderList.DataSource = requests.Where(x => x.WorderOrderItem != null).Select(x => x.WorderOrderItem);
            gvWorkOrderList.DataBind();
        }
    }
}