<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="True" CodeBehind="TechMessages.aspx.cs" Inherits="MapIt.Web.Admin.TechMessages" %>

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
                    <li class="active">Tech Messages</li>
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
                    <h1 class="page-header">Tech Messages</h1>
                </div>
            </div>
            <!--/.row-->

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
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    User
                                </label>
                                <asp:TextBox runat="server" ID="txtSearchUser" CssClass="form-control" placeholder="Enter phone to search..."></asp:TextBox>
                                <asp:HiddenField ID="hfSUserId" runat="server" OnValueChanged="hfUserId_ValueChanged" />
                            </div>

                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading">Users List</div>
                        <div class="panel-body">
                            <asp:GridView runat="server" ID="gvUsers" CssClass="table"
                                ShowFooter="false" AutoGenerateColumns="false"
                                GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                DataKeyField="Id" EmptyDataText="No Data" ShowHeaderWhenEmpty="true"
                                AllowSorting="true" OnSorting="gvUsers_Sorting">
                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <RowStyle VerticalAlign="Middle" />
                                <AlternatingRowStyle CssClass="alt-table-data" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="16px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <img src='<%# (bool)Eval("HasPendingMessages") ? "/images/red_circle.png" : "" %>' alt="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Id" HeaderText="Id" />
                                    <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName" />
                                    <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Messages
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <a href="UserMessages?id=<%# Eval("Id") %>" title="Messages" class="grid_button"><i class="fa fa-comment" style="font-size: 25px;"></i></a>
                                            <img src='<%# (bool)Eval("HasPendingMessages") ? "/images/red_circle.png" : "" %>' alt="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
