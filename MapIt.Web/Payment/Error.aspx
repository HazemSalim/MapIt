<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Error.aspx.cs" Inherits="MapIt.Web.Payment.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MapIt | Error Page</title>

    <!-- Icons -->
    <link rel="shortcut icon" href="/images/fav.png" type="image/x-icon" />
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="/images/ico/apple-touch-icon-144-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="/images/ico/apple-touch-icon-114-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="/images/ico/apple-touch-icon-72-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" href="/images/ico/apple-touch-icon-57-precomposed.png" />

    <link href="../Payment/Content/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

        <div id="divcontent" runat="server" style="width: 100%; float: left; padding-top: 0px;">
            <div style="background-color: #f2f2f2; padding-left: 35px; padding-right: 35px; padding-top: 35px; padding-bottom: 50px; width: 770px; margin: 30px auto; border-radius: 15px; -moz-border-radius: 15px; -webkit-border-radius: 15px;">
                <div style="float: left; padding-top: 5px;">
                    <img src="<%= ConfigurationManager.AppSettings["WebsiteURL"] %>/Payment/Images/logo.svg" alt="" />
                </div>
                <div style="float: left; text-align: left;">

                    <p style="margin-left: 58px; color: #ff0000;">
                        <b>There is an error while payment process, please try again.</b>
                    </p>

                    <p style="margin-left: 58px;">
                        <b>Date/Time :</b>
                        <asp:Label ID="lbl_DateTime" runat="server"></asp:Label>
                    </p>
                    <p style="margin-left: 58px;">
                        <b>Result :</b>
                        <asp:Label ID="lbl_SuccessOrNot" runat="server"></asp:Label>
                    </p>
                    <p style="margin-left: 58px;">
                        <b>Amount :</b>
                        <asp:Label ID="lbl_Amount" runat="server"></asp:Label>
                    </p>
                    <p style="margin-left: 58px;">
                        <b>Reference ID :</b>
                        <asp:Label ID="lbl_ReferenceId" runat="server"></asp:Label>
                    </p>
                    <p style="margin-left: 58px;">
                        <b>Payment ID :</b>
                        <asp:Label ID="lbl_PaymentId" runat="server"></asp:Label>
                    </p>
                    <p style="margin-left: 58px;">
                        <b>Order No. :</b>
                        <asp:Label ID="lbl_MerchantTrackId" runat="server"></asp:Label>
                    </p>
                </div>

                <div style="clear: both"></div>
            </div>
        </div>
    </form>
</body>
</html>
