<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MapIt.Web.Admin.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Map it Administration Panel</title>

    <link rel="shortcut icon" type="image/png" href="/Images/fav.png" />
    <link rel="shortcut icon" type="image/x-icon" href="/Images/fav.png" />
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <%--<webopt:BundleReference runat="server" Path="~/Content/admin" />--%>
    <link href="/Content/admin/bootstrap.min.css" rel="stylesheet" />
    <link href="/Content/admin/datepicker3.css" rel="stylesheet" />
    <link href="/Content/admin/bootstrap-table.css" rel="stylesheet" />
    <link href="/Content/admin/admin-styles.css" rel="stylesheet" />
    <link href="/Content/snackbar.css" rel="stylesheet" />

    <script>
        function tempAlert(msg, location, duration) {
            var el = document.createElement("div");
            el.setAttribute("id", "snackbar");
            el.innerHTML = msg;
            el.className = "show";
            setTimeout(function () {
                el.parentNode.removeChild(el);
            }, duration);
            document.body.appendChild(el);

            if (location.length != 0) {
                setTimeout(function () {
                    window.location = location;
                }, duration);
            }
        }
    </script>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <div id="snackbar"></div>

        <div class="row">
            <div class="col-xs-10 col-xs-offset-1 col-sm-8 col-sm-offset-2 col-md-4 col-md-offset-4">
                <div class="text-center">
                    <h2>Map it Admin Panel</h2>
                </div>
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">Log in</div>
                    <div class="panel-body">
                        <fieldset>
                            <div class="form-group">
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="UserName" autofocus=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                                    Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S" CssClass="has-error"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                    Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S" CssClass="has-error"></asp:RequiredFieldValidator>
                            </div>
                            <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="chkRememberMe" runat="server" />
                                    Remember Me
                                </label>
                            </div>
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CausesValidation="true" ValidationGroup="S"
                                OnClick="btnLogin_Click" CssClass="btn btn-primary" />
                        </fieldset>
                    </div>
                </div>
            </div>
            <!-- /.col-->
            <div class="col-xs-10 col-xs-offset-1 col-sm-8 col-sm-offset-2 col-md-4 col-md-offset-4">
                <div class="text-center">
                    &copy; <%:DateTime.Now.Year %> Map it. All Rights Reserved.
                </div>
                <div class="text-center">
                    Developed by Hard Task
                    <a href="http://hardtask.com" target="_blank">
                        <img src="../images/hardtask.png" alt="">
                    </a>
                </div>
            </div>
        </div>
        <!-- /.row -->
    </form>
</body>
</html>
