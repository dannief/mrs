<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkOrderList.ascx.cs" Inherits="MRS.Website.UserControls.WorkOrderList" %>

<asp:GridView ID="gvWorkOrderList" runat="server" CssClass="table table-bordered table-hover" ClientIDMode="Static" DataKeyNames="RequestNumber" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField DataField="WorkOrderNumber" HeaderText="Work Order Number"/>
        <asp:BoundField DataField="Worker" HeaderText="Worker"/>
        <asp:BoundField DataField="Description" HeaderText="Description" />
        <asp:BoundField DataField="Priority" HeaderText="Priority" />
        <asp:BoundField DataField="RequestNumber"/>
    </Columns>
    <EmptyDataTemplate>There are no work orders to display</EmptyDataTemplate>
</asp:GridView>

<style type="text/css">
    #gvWorkOrderList tr
    {
        cursor: pointer;
    }

    #gvWorkOrderList th:last-child,
    #gvWorkOrderList td:last-child
    {
       display: none;
    }
</style>

<script type="text/javascript">
    $(function () {
        $("#gvWorkOrderList").on("click", "tr", function () {
            requestNumber = $(this).children("td:last-child").html();
            location.href = '<%= ResolveClientUrl("~/WorkOrder.aspx?RequestNumber=")%>' + requestNumber;
        });
    });
</script>