<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="True" CodeBehind="UserCredits.aspx.cs" Inherits="MapIt.Web.Admin.UserCredits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function pageLoad(sender, args) {

            $(function () {
                $("[id$=txtSearchUser]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/App/App.asmx/BindUsers") %>',
                            data: "{ 'prefix': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item.split('-')[0],
                                        val: item.split('-')[1]
                                    }
                                }))
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hfSUserId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }

        $(document).keypress(function (event) {
            if (event.keyCode == 13) {
                $("#<%= btnSearch.ClientID %>").click();
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <ol class="breadcrumb">
                    <li><a href=".">
                        <i class="fa fa-home"></i>
                    </a></li>
                    <li class="active">User Credits</li>
                </ol>
            </div>
            <!--/.row-->

            <div class="row" style="margin-top: 10px;">
                <div class="col-xs-6">
                    <input type="button" value="<" class="btn btn-info" onclick="goBack();">
                    <input type="button" value=">" class="btn btn-info" onclick="goForward();">
                </div>
                <div class="col-xs-6">
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">User Credits</h1>
                </div>
            </div>
            <!--/.row-->

            <asp:Panel ID="pnlAllRecords" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        Date From 
                                    </label>
                                    <asp:TextBox ID="txtSearchDateFrom" runat="server" TextMode="Date" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Date To 
                                    </label>
                                    <asp:TextBox ID="txtSearchDateTo" runat="server" TextMode="Date" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                </div>

                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-default" CausesValidation="false" Text="Print"
                                    OnClientClick="printContent('div_usercredits');" />
                                <asp:Button ID="btnExportExcel" runat="server" CssClass="btn btn-info" CausesValidation="false" Text="Export To Excel"
                                    OnClick="btnExportExcel_Click" />
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        User
                                    </label>
                                    <asp:TextBox runat="server" ID="txtSearchUser" CssClass="form-control" placeholder="Enter phone to search..."></asp:TextBox>
                                    <asp:HiddenField ID="hfSUserId" runat="server" OnValueChanged="hfUserId_ValueChanged" />
                                </div>
                                <div class="form-group">
                                    <label>
                                        Payment Method
                                    </label>
                                    <asp:DropDownList ID="ddlSearchPaymentMethod" runat="server" CssClass="form-control" Width="50%" AppendDataBoundItems="true">
                                        <asp:ListItem Text="All Payment Method" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Payment Status
                                    </label>
                                    <asp:DropDownList ID="ddlSearchPaymentStatus" runat="server" CssClass="form-control" Width="50%" AppendDataBoundItems="true">
                                        <asp:ListItem Text="All Payment Status" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-default">
                            <div class="panel-heading">Credits List</div>
                            <div class="panel-body">
                                <div id="div_usercredits" style="display: none;">
                                    <div class="row" style="margin-bottom: 10px;">
                                        <div class="col-xs-6">
                                            <img src="/images/logo.svg" alt="MapIt" />
                                        </div>
                                    </div>
                                    <fieldset>
                                        <legend>Credits List</legend>
                                        <div class="row">
                                            <div class="col-xs-6">
                                                <div class="form-group">
                                                    <label>
                                                        User
                                                    </label>
                                                    <asp:Label ID="lblSearchUser" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        Payment Method
                                                    </label>
                                                    <asp:Label ID="lblSearchPaymentMethod" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        Payment Status
                                                    </label>
                                                    <asp:Label ID="lblSearchPaymentStatus" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-6">
                                                <div class="form-group">
                                                    <label>
                                                        Date From
                                                    </label>
                                                    <asp:Label ID="lblSearchDateFrom" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        Date To
                                                    </label>
                                                    <asp:Label ID="lblSearchDateTo" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>

                                    <asp:GridView runat="server" ID="gvCreditsExcel" ShowFooter="false" AutoGenerateColumns="false"
                                        GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                        DataKeyField="Id" CssClass="table" EmptyDataText="No Data" ShowHeaderWhenEmpty="true" Visible="true">
                                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                        <RowStyle VerticalAlign="Middle" />
                                        <AlternatingRowStyle CssClass="alt-table-data" />
                                        <Columns>
                                            <asp:BoundField DataField="TransNo" HeaderText="Trans No" />
                                            <asp:BoundField DataField="User.Phone" HeaderText="User Phone" />
                                            <asp:BoundField DataField="Package.TitleEN" HeaderText="Package" />
                                            <asp:TemplateField ItemStyle-Width="70px" HeaderText="Amount">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <%# Eval("Amount") %> <%# Eval("Currency.SymbolEN") %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PaymentMethod.TitleEN" HeaderText="Payment Method" />
                                            <asp:TemplateField HeaderText="Payment Status">
                                                <ItemTemplate>
                                                    <%# GetPaymentStatusName(Eval("PaymentStatus")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TransOn" HeaderText="Trans On" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:GridView runat="server" ID="gvCredits" ShowFooter="false" AutoGenerateColumns="false"
                                    GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                    DataKeyField="Id" CssClass="table" EmptyDataText="No Data"
                                    ShowHeaderWhenEmpty="true" OnRowCommand="gvCredits_RowCommand"
                                    AllowSorting="true" OnSorting="gvCredits_Sorting">
                                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <RowStyle VerticalAlign="Middle" />
                                    <AlternatingRowStyle CssClass="alt-table-data" />
                                    <Columns>
                                        <asp:BoundField DataField="Id" Visible="false" />
                                        <asp:BoundField DataField="TransNo" HeaderText="Trans No" SortExpression="TransNo" />
                                        <asp:BoundField DataField="User.Phone" HeaderText="User Phone" SortExpression="User.Phone" />
                                        <asp:BoundField DataField="Package.TitleEN" HeaderText="Package" SortExpression="Package.TitleEN" />
                                        <asp:TemplateField ItemStyle-Width="70px" HeaderText="Amount" SortExpression="Amount">
                                            <ItemTemplate>
                                                <div style="text-align: center;">
                                                    <%# Eval("Amount") %> <%# Eval("Currency.SymbolEN") %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PaymentMethod.TitleEN" HeaderText="Payment Method" SortExpression="PaymentMethod.TitleEN" />
                                        <asp:TemplateField HeaderText="Payment Status" SortExpression="PaymentStatus">
                                            <ItemTemplate>
                                                <%# GetPaymentStatusName(Eval("PaymentStatus")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TransOn" HeaderText="Trans On" SortExpression="TransOn" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                    </Columns>
                                </asp:GridView>
                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" OnPageChanged="AspNetPager1_PageChanged"
                                    PageSize="15" CssClass="pages" CurrentPageButtonClass="cpb" NextPageText="Next"
                                    PrevPageText="Previous" Wrap="true" Direction="RightToLeft" ShowFirstLast="False">
                                </webdiyer:AspNetPager>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

