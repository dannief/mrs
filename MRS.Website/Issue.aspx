<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Issue.aspx.cs" Inherits="MRS.Website.Issue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageHeading" runat="server">
    Issue Details
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <div class="row-fluid">        
        <div class="span8">
            <!-- BEGIN REQUEST FIELDS -->
            <div>                
                <label>Title</label>
                <input id="inputTitle" class="input-xxlarge" />
                <label>Description</label>
                <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="10"></asp:TextBox>
                <label>Subcategory</label>
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="input-xxlarge"></asp:DropDownList>
                <label>Location</label>
                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="input-xxlarge"></asp:DropDownList>
                <br />
                <br />
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary">
                <i class="icon-ok"></i> 
                <div>Report Issue</div>
                </asp:LinkButton>
            </div>
            <!-- END REQUEST FIELDS -->
        </div>

        <div class="span4">        
            <!--  -->
            <div>
                
            </div>   
             <!--  -->       
        </div>
    </div>
</asp:Content>
