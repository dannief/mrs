using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MRS.Application;
using MRS.DataTransferObjects;
using MRS.Website.Code;

namespace MRS.Website
{
    public partial class WorkOrder : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var requestDto = Session["request"] as RequestDetailsDto;
                LoadWorkOrder(requestDto);
            }
        }

        protected void lBtnSaveWorkOrderForm_Click(object sender, EventArgs e)
        {
            var workOrderData = new WorkOrderDto
            {
                WorkOrderNumber = litWorkOrderNumber.Text,
                WorkerIDNumber = ddlAssignedWorker.SelectedValue,
                Description = txtWorkOrderDescription.Text,
                PriorityID = ddlPriority.SelectedValue
            };

            var requestNumber = litRequestNumber.Text;
            if (string.IsNullOrEmpty(workOrderData.WorkOrderNumber))
            {
                RequestService.CreateWorkOrder(User.IDNumber, requestNumber, workOrderData);
            }
            else
            {
                RequestService.UpdateWorkOrder(requestNumber, workOrderData);
            }

            Response.Redirect("Request.aspx?RequestNumber=" + requestNumber);
        }
                
        private void LoadWorkOrder(RequestDetailsDto requestDto)
        {
            LoadWorkOrderFormDropdownLists(requestDto);

            var workOrderData = requestDto.WorderOrderData;
            if (workOrderData != null)
            {
                htmlLblWorkOrderNum.Visible = true;                
                litRequestNumber.Visible = true;
                //litWorkOrderNumber.Visible = true;
                litRequestNumber.Text = requestDto.RequestNumber;
                litWorkOrderNumber.Text = workOrderData.WorkOrderNumber;
                ddlAssignedWorker.SelectedValue = workOrderData.WorkerIDNumber;
                txtWorkOrderDescription.Text = workOrderData.Description;
                ddlPriority.SelectedValue = workOrderData.PriorityID;
            }
        }

        private void LoadWorkOrderFormDropdownLists(RequestDetailsDto requestDto)
        {
            LoadWorkersInDropdownlist(requestDto);
            LoadPrioritiesInDropdownlist(requestDto);
        }

        private void LoadPrioritiesInDropdownlist(RequestDetailsDto requestDto)
        {
            var priorities = LookupService.GetPriorities();
            ddlPriority.DataSource = priorities;
            ddlPriority.DataValueField = "ID";
            ddlPriority.DataTextField = "Name";
            ddlPriority.DataBind();
        }

        private void LoadWorkersInDropdownlist(RequestDetailsDto requestDto)
        {
            var workers = LookupService.GetWorkers(requestDto.LocationID);
            ddlAssignedWorker.DataSource = workers;
            ddlAssignedWorker.DataValueField = "ID";
            ddlAssignedWorker.DataTextField = "Name";
            ddlAssignedWorker.DataBind();
        }
    }
}