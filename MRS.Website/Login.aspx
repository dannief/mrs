<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MRS.Website.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="Content/bootswatch/cosmo/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/font-awesome.css" rel="stylesheet" />
    <link href="Content/site/main.min.css" rel="stylesheet" />

    <script src="Scripts/bootstrap.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row-fluid">
                <div class="span4 offset4">

                    
                    <div id="login-box-wrapper">

                        <h3>Maintenance Request System</h3>

                        <!-- BEGIN LOGIN BOX-->
                        <div id="login-box" class="well">
                            <div class="input-prepend">
                                <span class="add-on"><i class="icon-user"></i></span>
                                <asp:TextBox ID="txtIdNumber" runat="server" placeholder="ID Number" CssClass="input-xlarge"></asp:TextBox>
                            </div>
                            <div class="input-prepend">
                                <span class="add-on"><i class="icon-lock"></i></span>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="input-xlarge"></asp:TextBox>
                            </div>
                            <br />
                            <br />
                            <asp:LinkButton ID="lBtnLogin" runat="server" CssClass="btn btn-primary pull-right" OnClick="lBtnLogin_Click">
                            <i class="icon-signin"></i>Login
                            </asp:LinkButton>
                        </div>
                        <!-- BEGIN LOGIN BOX-->
                    </div>

                </div>

            </div>
        </div>
    </form>
</body>
</html>
