using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using MRS.DataTransferObjects;
using MRS.Website.Code;
using MRS.Website.UserControls;
using MRS.Application.Command;

namespace MRS.Website
{
    public partial class Request : BasePage
    {
        [WebMethod]
        public static string GetLocations()
        {
            var basePage = HttpContext.Current.Handler as BasePage;
            var lookupService = basePage.LookupService;
            var user = Thread.CurrentPrincipal as UserPrincipal;

            var locations = lookupService
                .GetLocationsUserCanCreateRequestFor(user.IDNumber)
                .ToArray()
                .Serialize<LookupDataDto[]>();

            return locations;
        }

        private void InitializeStateButtons(ICollection<LookupDataDto> states)
        {
            pnlStateButtons.Controls
                .OfType<WebControl>()
                .Where(x => states.Any(s => x.Attributes["data-stateid"] == s.ID))
                .ToList()
                .ForEach(ctrl => ctrl.Visible = true);
        }
               
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var mainCategoryId = ddlCategory.SelectedValue;
            LoadSubCategoriesInDropdownlist(mainCategoryId);
        }

        protected void lBtnSaveRequest_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var requestNumber = litRequestNumber.Text;

                if (string.IsNullOrEmpty(requestNumber))
                {
                    var requestNumberGuid = Guid.NewGuid();
                    requestNumber = requestNumberGuid.ToString();

                    var createRequestCommand = new CreateRequestCommand
                    (
                        requestNumberGuid,
                        txtTitle.Text,
                        txtRequestDescription.Text,
                        hfLocation.Value,
                        ddlSubcategory.SelectedValue,
                        ddlSeverity.SelectedValue,
                        User.IDNumber
                    );

                    GetCommandHandler<CreateRequestCommand>().Handle(createRequestCommand);
                }
                else
                {
                    var updateRequestDto = new UpdateRequestDto
                    {
                        UserIDNumber = User.IDNumber,
                        RequestNumber = requestNumber,
                        Title = txtTitle.Text,
                        Description = txtRequestDescription.Text
                    };

                    RequestService.UpdateRequestDetails(updateRequestDto);
                }

                Response.Redirect("Request.aspx?RequestNumber=" + requestNumber);
            }
        }

        protected void lBtnState_Click(object sender, EventArgs e)
        {
            var stateId = ((LinkButton)sender).Attributes["data-stateid"];
            var reason = hfRejectReason.Value;
            var requestNumber = litRequestNumber.Text;

            switch(stateId)
            {
                case "Approved":
                    RequestService.ApproveRequest(requestNumber, User.IDNumber);
                    break;
                case "Completed":
                    RequestService.CompleteWork(requestNumber, User.IDNumber);
                    break;
                case "Rejected":
                    RequestService.RejectRequest(requestNumber, User.IDNumber, reason);
                    break;                
                case "WorkRejected":
                    RequestService.RejectWork(requestNumber, User.IDNumber, reason);
                    break;
                case "WorkStarted":
                    RequestService.StartWork(requestNumber, User.IDNumber);
                    break;
            }

            Response.Redirect("Request.aspx?RequestNumber=" + requestNumber);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropdownlists();
                var requestNumber = Request.QueryString["RequestNumber"];
                if (!string.IsNullOrWhiteSpace(requestNumber))
                {
                    var request = RequestService.GetRequest(requestNumber, User.IDNumber);
                    var states = LookupService.GetNextPossibleStates(User.IDNumber, requestNumber);
                    LoadRequest(request);
                    InitializeStateButtons(states);

                    //var shouldShowWorkOrderForm = Request.QueryString["ShowWorkOrder"] == "true";
                    //if (shouldShowWorkOrderForm)
                    //{
                        
                    //}
                }
            }
        }

        private void LoadCategoriesInDropdownlist()
        {
            var categories = LookupService.GetMainCategories();
            ddlCategory.DataSource = categories;
            ddlCategory.DataValueField = "ID";
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataBind();

            LoadSubCategoriesInDropdownlist(categories.First().ID);
        }

        private void LoadDropdownlists()
        {
            LoadSeveritiesInDropdownlist();
            LoadCategoriesInDropdownlist();
        }
             
        private void LoadRequest(RequestDetailsDto request)
        {
            Session["request"] = request;

            htmlLblRequestNum.Visible = true;
            htmlLblState.Visible = true;
            litRequestNumber.Text = request.RequestNumber;
            litState.Text = request.State;
            txtTitle.Text = request.Title;
            txtRequestDescription.Text = request.Description;
            ddlSeverity.SelectedValue = request.SeverityID;
            ddlCategory.SelectedValue = request.MainCategoryID;
            ddlSubcategory.SelectedValue = request.SubCategoryID;
            txtLocationAutoCompleteBox.Text = request.LocationName;
            hfLocation.Value = request.LocationID;

            ddlSeverity.Enabled = false;
            ddlCategory.Enabled = false;
            ddlSubcategory.Enabled = false;
            txtLocationAutoCompleteBox.Enabled = false;

            if (request.WorderOrderData == null)
            {
                lnkShowWorkOrderForm.Visible = false;
            }
        }

        private void LoadSeveritiesInDropdownlist()
        {
            var severities = LookupService.GetSeverities();
            ddlSeverity.DataSource = severities;
            ddlSeverity.DataValueField = "ID";
            ddlSeverity.DataTextField = "Name";
            ddlSeverity.DataBind();
        }

        private void LoadSubCategoriesInDropdownlist(string mainCategoryId)
        {
            var subCategories = LookupService.GetSubCategories(mainCategoryId);
            ddlSubcategory.DataSource = subCategories;
            ddlSubcategory.DataValueField = "ID";
            ddlSubcategory.DataTextField = "Name";
            ddlSubcategory.DataBind();
        }
    }
}