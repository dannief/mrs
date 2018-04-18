<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequestList.ascx.cs" Inherits="MRS.Website.UserControls.RequestList" %>

<asp:GridView ID="gvRequestList" runat="server" CssClass="table table-bordered table-hover" ClientIDMode="Static" DataKeyNames="RequestNumber">
    <EmptyDataTemplate>You don't have any pending requests</EmptyDataTemplate>
</asp:GridView>

<style type="text/css">
    #gvRequestList tr
    {
        cursor: pointer;
    }
</style>

<script type="text/javascript">
    $(function () {        
        $("#gvRequestList").on("click", "tr", function () {
            requestNumber = $(this).children("td:first-child").html();
            location.href = '<%= ResolveClientUrl("~/Request.aspx?RequestNumber=")%>' + requestNumber;
        });
    });
</script>