using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MRS.DataTransferObjects;
using MRS.Website.Code;

namespace MRS.Website
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void CreateAuthenticationTicket(UserDto userData)
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var authenticationSection = (AuthenticationSection)config.GetSection("system.web/authentication");
            var expiration = authenticationSection.Forms.Timeout;

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                userData.IDNumber,
                DateTime.Now,
                DateTime.Now.Add(expiration),
                true,
                userData.Serialize<UserDto>(),
                FormsAuthentication.FormsCookiePath
            );

            // Encrypt the ticket.
            string encTicket = FormsAuthentication.Encrypt(ticket);


            // Create the cookie.
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            cookie.Expires = DateTime.Now.Add(expiration);

            Response.Cookies.Add(cookie);
        }

        protected void lBtnLogin_Click(object sender, EventArgs e)
        {
            var user = RequestService.AuthenticateUser(txtIdNumber.Text, txtPassword.Text);

            // TODO: Replace empty string with subsidiary id from UI drop down list
            if (user != null)
            {                
                CreateAuthenticationTicket(user);

                Response.Redirect(FormsAuthentication.GetRedirectUrl(user.IDNumber, false));
                
            }
        }
    }
}