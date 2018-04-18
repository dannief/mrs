<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkOrder.aspx.cs" Inherits="MRS.Website.WorkOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageHeading" runat="server">
    Work Order
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">    
    <label runat="server" id="htmlLblWorkOrderNum" visible="false">Work Order Number</label>
    <p class="text-info">
        <asp:Literal ID="litRequestNumber" runat="server" Visible="false"/>
        <asp:Literal ID="litWorkOrderNumber" runat="server" Visible="false"></asp:Literal>
    </p>
    <label>Assigned Worker</label>
    <asp:DropDownList ID="ddlAssignedWorker" runat="server" CssClass="input-xxlarge"></asp:DropDownList>            
    <label>Priority</label>
    <asp:DropDownList ID="ddlPriority" runat="server" CssClass="input-xxlarge"></asp:DropDownList>
    <label>Description</label>
    <asp:TextBox ID="txtWorkOrderDescription" runat="server" CssClass="input-xxlarge" TextMode="MultiLine" Rows="10"></asp:TextBox>   
    <br />
    <br />
    <asp:LinkButton ID="lBtnSaveWorkOrderForm" runat="server" CssClass="btn btn-primary" OnClick="lBtnSaveWorkOrderForm_Click">
        <i class="icon-ok"></i> 
        <div>Save Work Order</div>
    </asp:LinkButton>             
</asp:Content>
