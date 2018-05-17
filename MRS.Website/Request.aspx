<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="MRS.Website.Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="Scripts/app/request.aspx.min.js"></script>--%>
    <script src="Scripts/app/request.aspx.js"></script>

    <script type="text/javascript">

        function promptForRejectReason() {
            var reason = prompt("Enter a reason for rejection");
            $("#hfRejectReason").val(reason);
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageHeading" runat="server">
    Maintenance Request Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row-fluid">        
        <div class="span7">
            <!-- BEGIN REQUEST FIELDS -->
            <div>
                <label runat="server" id="htmlLblRequestNum" visible="false">Request Number</label>
                <p class="text-info"><asp:Literal ID="litRequestNumber" runat="server"></asp:Literal></p>
                <label runat="server" id="htmlLblState" visible="false">State</label>
                <p class="text-info"><asp:Literal ID="litState" runat="server"></asp:Literal></p>
                <label>Title</label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="input-xxlarge"></asp:TextBox>
                <label>Description</label>
                <asp:TextBox ID="txtRequestDescription" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="10"></asp:TextBox>
                <label>Severity</label>
                <asp:DropDownList ID="ddlSeverity" runat="server" CssClass="input-xxlarge"></asp:DropDownList>
                <asp:UpdatePanel ID="upCategories" runat="server">
                    <ContentTemplate> 
                        <label>Category</label>
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="input-xxlarge" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                        <label>Subcategory</label>
                        <asp:DropDownList ID="ddlSubcategory" runat="server" CssClass="input-xxlarge"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <label>Location</label>                
                <asp:TextBox ID="txtLocationAutoCompleteBox" CssClass="input-xxlarge" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfLocation" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfRejectReason" runat="server" ClientIDMode="Static" />
                <br />
                <br />
                <asp:LinkButton ID="lBtnSaveRequest" runat="server" CssClass="btn btn-primary" OnClick="lBtnSaveRequest_Click">
                    <i class="icon-ok"></i> 
                    <div>Save Request</div>
                </asp:LinkButton>
            </div>
            <!-- END REQUEST FIELDS -->
        </div>
        <div class="span5">        
            <!-- START WORKFLOW BUTTONS -->
            <div class="row-fluid">                
                <asp:Panel ID="pnlStateButtons" CssClass="span12" runat="server">
                        <asp:LinkButton ID="lBtnApproveRequest" runat="server" CssClass="btn btn-info action-button"
                            data-stateid="Approved" Visible="false" OnClick="lBtnState_Click">
                        <i class="icon-wrench"></i>
                        <div>Approve Request</div>
                        </asp:LinkButton>

                        <asp:LinkButton ID="lBtnRejected" runat="server" CssClass="btn btn-infoaction-button " 
                            data-stateid="Rejected" Visible="false" OnClick="lBtnState_Click">
                        <i class="icon-wrench"></i>
                        <div>Reject Request</div>
                        </asp:LinkButton>

                        <asp:HyperLink ID="lnkCreateWorkOrder" runat="server" CssClass="btn btn-info action-button" 
                            data-stateid="WorkAssigned" Visible="false" ClientIDMode="Static">
                        <i class="icon-wrench"></i>
                        <div>Create Worker Order</div>
                        </asp:HyperLink>

                        <asp:LinkButton ID="lBtnStartWork" runat="server" CssClass="btn btn-info action-button" 
                            data-stateid="WorkStarted" Visible="false" OnClick="lBtnState_Click">
                        <i class="icon-wrench"></i>
                        <div>Start Work</div>
                        </asp:LinkButton>

                        <asp:LinkButton ID="lBtnWorkRejected" runat="server" CssClass="btn btn-info action-button" 
                            data-stateid="WorkRejected" Visible="false" OnClick="lBtnState_Click">
                        <i class="icon-wrench"></i>
                        <div>Reject Work</div>
                        </asp:LinkButton>

                        <asp:LinkButton ID="lBtnCompleted" runat="server" CssClass="btn btn-info action-button" 
                            data-stateid="Completed" Visible="false" OnClick="lBtnState_Click">
                        <i class="icon-wrench"></i>
                        <div>Complete Request</div>
                        </asp:LinkButton>
                    </asp:Panel>                
            </div>
            <!-- END WORKFLOW BUTTONS --> 

            <!-- START ACTION BUTTONS -->
            <div class="row-fluid">
                <div class="span12">
                    <asp:LinkButton ID="lBtnEmailWorker" runat="server" CssClass="btn action-button">
                        <i class="icon-envelope"></i>
                        <div>Email Worker</div>
                    </asp:LinkButton>   
                    <asp:Hyperlink ID="lnkShowWorkOrderForm" runat="server" CssClass="btn action-button" ClientIDMode="Static">
                        <i class="icon-wrench"></i>
                        <div>View Work Order</div>
                    </asp:Hyperlink>                             
                </div>
            </div>   
            <!-- END ACTION BUTTONS -->                   
        </div>
    </div>   
   
</asp:Content>
