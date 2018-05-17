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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropdownlists();   
                
                if (IsExistingRequest(out string requestNumber))
                {   
                    LoadRequest(requestNumber);
                    lnkShowWorkOrderForm.NavigateUrl = 
                        lnkCreateWorkOrder.NavigateUrl = 
                        "~/WorkOrder.aspx?RequestNumber=" + requestNumber;
                    
                    InitializeStateButtons(requestNumber);
                }
                else
                {
                    HideWorkOrderActionButtons();
                }
            }
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
                        requestNumber: requestNumberGuid,
                        title: txtTitle.Text,
                        description: txtRequestDescription.Text,
                        locationID: hfLocation.Value,
                        subCategoryID: ddlSubcategory.SelectedValue,
                        severityID: ddlSeverity.SelectedValue,
                        requesterIDNumber: User.IDNumber
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

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var mainCategoryId = ddlCategory.SelectedValue;
            LoadSubCategoriesInDropdownlist(mainCategoryId);
        }


        protected void lBtnState_Click(object sender, EventArgs e)
        {
            var stateId = ((LinkButton)sender).Attributes["data-stateid"];
            var reason = hfRejectReason.Value;
            var requestNumber = litRequestNumber.Text;

            switch (stateId)
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

        private void InitializeStateButtons(string requestNumber)
        {
            var states = LookupService.GetNextPossibleStates(User.IDNumber, requestNumber);

            pnlStateButtons.Controls
                .OfType<WebControl>()
                .Where(x => states.Any(s => x.Attributes["data-stateid"] == s.ID))
                .ToList()
                .ForEach(ctrl => ctrl.Visible = true);
        }

        private bool IsExistingRequest(out string requestNumber)
        {
            requestNumber = Request.QueryString["RequestNumber"];

            return !string.IsNullOrWhiteSpace(requestNumber);
        }


        private void HideWorkOrderActionButtons()
        {
            lBtnEmailWorker.Visible = false;
            lnkShowWorkOrderForm.Visible = false;
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
             
        private void LoadRequest(string requestNumber)
        {
            var request = RequestService.GetRequest(requestNumber, User.IDNumber);

            Session[$"request_{request.RequestNumber}"] = request;

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
                HideWorkOrderActionButtons();
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