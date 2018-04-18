<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="MRS.Website.Dashboard" %>

<%@ Register Src="~/UserControls/RequestList.ascx" TagPrefix="uc1" TagName="RequestList" %>
<%@ Register Src="~/UserControls/WorkOrderList.ascx" TagPrefix="uc1" TagName="WorkOrderList" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

    <!-- BEGIN ACTION BUTTONS -->
    <div class="row-fluid">
        <asp:HyperLink NavigateUrl="~/Request.aspx" runat="server" CssClass="btn btn-large span3 offset3">
            <i class="icon-gears"></i>
            <div> Create Request</div>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Issue.aspx" runat="server" CssClass="btn btn-large span3">
            <i class="icon-warning-sign"></i>
            <div> Report Issue</div>
        </asp:HyperLink>
    </div>
    <!-- END ACTION BUTTONS -->

    <br /><br />

    <!-- BEGIN TABS -->
    <div class="row-fluid">        
        <ul class="nav nav-tabs pull-right span12">
            <li class="active"><a href="#requests" data-toggle="tab">Requests</a></li>
            <li><a href="#issues" data-toggle="tab">Issues</a></li>
            <li><a href="#workorders" data-toggle="tab">Work Orders</a></li>
        </ul>       
    </div>
    <div class="row-fluid">
        <div class="tab-content span12">
          <div class="tab-pane active" id="requests">
              <uc1:RequestList runat="server" ID="ucRequestList" />
          </div>
          <div class="tab-pane" id="issues">issues...</div>
          <div class="tab-pane" id="workorders">
              <uc1:WorkOrderList runat="server" ID="ucWorkOrderList" />
          </div>          
        </div>
    </div>
    <!-- BEGIN TABS -->

</asp:Content>
