using System;
using System.Web.Security;
using MRS.Website.Code;

namespace MRS.Website
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetUserName();
        }

        private void SetUserName()
        {
            if (Request.IsAuthenticated)
            {
                var userDto = ((UserPrincipal)Context.User);
                litUserName.Text = userDto.Type + ": " +  userDto.Name + " (" + userDto.IDNumber + ")";
            }
        }
                
        protected void lBtnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();

            Session.Contents.RemoveAll();
                        
            FormsAuthentication.SignOut();
                      
            Response.Redirect(FormsAuthentication.LoginUrl);
        }
    }
}