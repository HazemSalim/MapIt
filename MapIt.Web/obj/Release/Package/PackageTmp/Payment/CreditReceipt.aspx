<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="CreditReceipt.aspx.cs" Inherits="MapIt.Web.Payment.CreditReceipt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Map It | Credit Receipt</title>

    <!-- Icons -->
    <link rel="shortcut icon" href="/images/fav.png" type="image/x-icon" />
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="/images/ico/apple-touch-icon-144-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="/images/ico/apple-touch-icon-114-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="/images/ico/apple-touch-icon-72-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" href="/images/ico/apple-touch-icon-57-precomposed.png" />

    <link href="../Payment/Content/receipt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            window.print();
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page-wrap">
            <%--<a href="/Account" class="back_home">Back to your account page</a>--%>
            <div id="header">Map It | Credit Receipt</div>
            <div id="identity">
                <strong>Address:</strong>
                <asp:Literal ID="litComAddress" runat="server"></asp:Literal>
                <br />
                <strong>Phone:</strong>
                <asp:Label ID="lblComPhone" runat="server"></asp:Label>
                <div id="logo">
                    <img id="image" src="images/logo.svg" alt="logo" width="50px" height="50px" />
                </div>
            </div>
            <div style="clear: both"></div>
            <div id="customer">
                <strong>Customer Id:</strong>
                <asp:Label ID="lblId" runat="server"></asp:Label>
                <br />
                <strong>Customer:</strong>
                <asp:Label ID="lblUser" runat="server"></asp:Label>
                <br />
                <strong>Phone:</strong>
                <asp:Label ID="lblPhone" runat="server"></asp:Label>
                <br />
                <table id="meta">
                    <tr>
                        <td class="meta-head">Receipt #</td>
                        <td>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="meta-head">Date</td>
                        <td>
                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="meta-head">Package</td>
                        <td>
                            <asp:Label ID="lblPackage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="meta-head">Start Date</td>
                        <td>
                            <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="meta-head">End Date</td>
                        <td>
                            <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="meta-head">Payment Method</td>
                        <td>
                            <asp:Label ID="lblPaymentMethod" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="meta-head">Amount</td>
                        <td>
                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="terms" style="margin-bottom: 20px;">
                <h5>Terms</h5>
                <asp:Literal ID="litTerms" runat="server"></asp:Literal>
            </div>
        </div>

    </form>
</body>
</html>
