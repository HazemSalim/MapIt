<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="True" CodeBehind="Services.aspx.cs" Inherits="MapIt.Web.Admin.Services" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .hide {
            display: none;
        }

        .w-auto {
            width: auto;
            display: inherit;
        }
    </style>
    <script src="../Scripts/jquery-1.7.1.js"></script>
    <link href="../Content/colorbox.css" rel="stylesheet" />
    <script src="../Scripts/jquery.colorbox.js"></script>

    <%--<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>--%>
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

                $("[id$=txtUser]").autocomplete({
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
                        $("[id$=hfUserId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });

            $(document).ready(function () {
                $(".iframe").colorbox({ iframe: true, transition: "none", width: "770", height: "570" });
                $(".callbacks").colorbox({ onCleanup: function () { updateLocation(); } });
            });
        }

        function updateLocation() {
            try {
                debugger;
                var longitude = document.getElementById("hfLongitude");
                var latitude = document.getElementById("hfLatitude");
                var txtLocation = document.getElementById("txtLocation");
                if (longitude.value != '' && latitude.value != '')
                    txtLocation.value = latitude.value + ', ' + longitude.value;
                else
                    txtLocation.value = '';
            } catch (e) {

            }
        }

        function clearLocation() {
            try {
                var longitude = document.getElementById("hfLongitude");
                var latitude = document.getElementById("hfLatitude");
                var txtLocation = document.getElementById("txtLocation");

                longitude.value = '';
                latitude.value != '';
                txtLocation.value = '';
            } catch (e) {

            }
        }
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
                    <li class="active">Services</li>
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
                    <h1 class="page-header">Services</h1>
                </div>
            </div>
            <!--/.row-->

            <asp:Panel ID="pnlAllRecords" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-xs-12 text-right">
                                <div class="form-group">
                                    <asp:Button ID="btnAddNew" runat="server" Text="Add new Service" CssClass="btn btn-primary"
                                        OnClick="btnAddNew_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
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
                                        Category
                                    </label>
                                    <asp:DropDownList ID="ddlSearchCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Text="All Categories" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group" style="padding-top: 25px;">
                                    <asp:TextBox runat="server" ID="txtSearchKeyWord" placeholder="Search ... " CssClass="form-control"></asp:TextBox>
                                </div>

                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-default" CausesValidation="false" Text="Print"
                                    OnClientClick="printContent('div_services');" />
                                <asp:Button ID="btnExportExcel" runat="server" CssClass="btn btn-info" CausesValidation="false" Text="Export To Excel"
                                    OnClick="btnExportExcel_Click" />
                            </div>
                        </div>

                        <div class="panel panel-default">
                            <div class="panel-heading">Services List</div>
                            <div class="panel-heading">
                                <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click"
                                    ToolTip="Delete All" CssClass="grid_button" OnClientClick="return confirm('Are you sure to delete?');">
                                                        <i class="fa fa-trash" style="font-size:25px;"></i>
                                </asp:LinkButton>
                            </div>
                            <div class="panel-body">
                                <div id="div_services" style="display: none;">
                                    <div class="row" style="margin-bottom: 10px;">
                                        <div class="col-xs-6">
                                            <img src="/images/svg.png" alt="Ellipse" />
                                        </div>
                                    </div>
                                    <fieldset>
                                        <legend>Services List</legend>
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
                                                        Category
                                                    </label>
                                                    <asp:Label ID="lblSearchCategory" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-6">
                                                <div class="form-group">
                                                    <label>
                                                        Search Keyword
                                                    </label>
                                                    <asp:Label ID="lblSearchKeyword" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>

                                    <asp:GridView runat="server" ID="gvServicesExcel" ShowFooter="false" AutoGenerateColumns="false"
                                        GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                        DataKeyField="Id" CssClass="table" EmptyDataText="No Data" ShowHeaderWhenEmpty="true" Visible="true">
                                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                        <RowStyle VerticalAlign="Middle" />
                                        <AlternatingRowStyle CssClass="alt-table-data" />
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="Id" />
                                            <asp:BoundField DataField="Title" HeaderText="Service" />
                                            <asp:BoundField DataField="ServicesCategory.TitleEN" HeaderText="Category" />
                                            <asp:BoundField DataField="User.FullName" HeaderText="User" />
                                            <asp:BoundField DataField="User.Phone" HeaderText="Phone" />
                                            <asp:TemplateField ItemStyle-Width="70px" HeaderText="Active">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <%# (bool)Eval("IsActive") ? "Active" : "Inactive" %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AddedOn" HeaderText="Added On" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <asp:GridView runat="server" ID="gvServices" ShowFooter="false" AutoGenerateColumns="false"
                                    GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                    DataKeyField="Id" CssClass="table" EmptyDataText="No Data"
                                    ShowHeaderWhenEmpty="true" OnRowCommand="gvServices_RowCommand"
                                    AllowSorting="true" OnSorting="gvServices_Sorting">
                                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <RowStyle VerticalAlign="Middle" />
                                    <AlternatingRowStyle CssClass="alt-table-data" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="70px">
                                            <HeaderTemplate>
                                                <label>
                                                    <input type="checkbox" onchange="checkAll2(this,'csAccessible')" name="chk[]" />
                                                </label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="csAccessible">
                                                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="chkitem" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Id" HeaderText="Id" />
                                        <asp:BoundField DataField="Title" HeaderText="Service" SortExpression="Title" />
                                        <asp:BoundField DataField="ServicesCategory.TitleEN" HeaderText="Category" SortExpression="ServicesCategory.TitleEN" />
                                        <asp:BoundField DataField="User.FullName" HeaderText="User" SortExpression="User.FullName" />
                                        <asp:BoundField DataField="User.Phone" HeaderText="Phone" SortExpression="User.Phone" />
                                        <asp:BoundField DataField="AddedOn" HeaderText="Added On" SortExpression="AddedOn" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                        <asp:BoundField DataField="Ordering" HeaderText="Ordering" SortExpression="Ordering" />
                                        <asp:TemplateField ItemStyle-Width="70px" HeaderText="Reported" SortExpression="ServiceReports.Count">
                                            <ItemTemplate>
                                                <div style="text-align: center;">
                                                    <a href="ServiceReports?id=<%# Eval("Id") %>">
                                                        <span style="font-size: 24px;"><%# (int)Eval("ReportsCount") == 0 ? "" : Eval("ReportsCount") %></span>
                                                        <%# (bool)Eval("IsReported") ? "<i class='fa fa-flag' style='font-size:25px;color:red;'></i>" : "" %>
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="110px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <img src='<%# Eval("ServicePhoto") %>' alt="" style="width: 100px;" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="70px" HeaderText="Admin Added" SortExpression="AdminAdded">
                                            <ItemTemplate>
                                                <div style="text-align: center;">
                                                    <%# (bool)Eval("AdminAdded") ? "<i class='fa fa-check' style='font-size:25px;'></i>" : "<i class='fa fa-times' style='font-size:25px;'></i>" %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="70px" HeaderText="Active" SortExpression="IsActive">
                                            <ItemTemplate>
                                                <div style="text-align: center;">
                                                    <%# (bool)Eval("IsActive") ? "<i class='fa fa-check' style='font-size:25px;'></i>" : "<i class='fa fa-times' style='font-size:25px;'></i>" %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <a href="ServiceMessages?id=<%# Eval("Id") %>" style="float: left; display: block; padding: 0px 8px;"><i class="fa fa-comments-o" style="font-size: 25px;"></i></a>

                                                 <a href="GenNotifs?sid=<%# Eval("Id") %>" style="float: left; display: block; padding: 0px 8px;">
                                                    <i class="fa fa-envelope-o" style="font-size: 25px;"></i></a>

                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditItem" CommandArgument='<%# Eval("Id") %>'
                                                    CssClass="grid_button" ToolTip="Edit">
                                                        <i class="fa fa-pencil" style="font-size:25px;"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteItem" CommandArgument='<%# Eval("Id") %>'
                                                    CssClass="grid_button" ToolTip="Delete" OnClientClick="return confirm('Are you sure to delete?');">
                                                        <i class="fa fa-trash" style="font-size:25px;"></i>
                                                </asp:LinkButton>
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
            </asp:Panel>
            <asp:Panel ID="pnlRecordDetails" runat="server" Visible="false">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">Service Details</div>
                            <div class="panel-body">
                                <div class="col-md-6">

                                    <div class="form-group">
                                        <label>
                                            User
                                        </label>
                                        <asp:TextBox runat="server" ID="txtUser" CssClass="form-control" placeholder="Enter phone to search..."></asp:TextBox>
                                        <asp:HiddenField ID="hfUserId" runat="server" OnValueChanged="hfUserId_ValueChanged" />
                                        <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="txtUser" SetFocusOnError="true"
                                            EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"
                                            InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                    <%--<div class="form-group">
                                        <label>
                                            Type
                                        </label>
                                        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select Type" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Company" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Individual" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType" SetFocusOnError="true"
                                            EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"
                                            InitialValue=""></asp:RequiredFieldValidator>
                                    </div>--%>
                                    <div class="form-group">
                                        <label>
                                            Category
                                        </label>
                                        <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                            <asp:ListItem Text="Select Category" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Country
                                        </label>
                                        <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Text="Select Country" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry" SetFocusOnError="true"
                                            EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"
                                            InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            City
                                        </label>
                                        <asp:DropDownList ID="ddlCity" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                            <asp:ListItem Text="Select City" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity" SetFocusOnError="true"
                                            EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"
                                            InitialValue=""></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group">
                                        <label>
                                            Title
                                        </label>
                                        <td class="col_value">
                                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                                                EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                        </label>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Experience Years
                                        </label>
                                        <asp:TextBox ID="txtExYears" runat="server" TextMode="Number" CssClass="form-control" min="0"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvExYears" runat="server" ControlToValidate="txtExYears"
                                            EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="S"
                                            Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Description
                                        </label>
                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100px" />
                                        <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription"
                                            EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="S"
                                            Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group" style="position: relative;">
                                        <label for="location">
                                            Location</label>
                                        <asp:TextBox ID="txtLocation" runat="server" Enabled="false" ClientIDMode="Static" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="txtLocation"
                                            EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="S"
                                            Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                        <a class="callbacks iframe location_icon" href="LocationByEsri?marker=1">
                                            <img src="/images/location_icon.png" alt="Location" />
                                        </a>
                                        <a class="delete_location_icon" style="cursor: pointer;" onclick="clearLocation();">
                                            <img src="/images/delete.png" alt="Location" />
                                        </a>
                                        <input id="hfLatitude" runat="server" clientidmode="Static" type="hidden" />
                                        <input id="hfLongitude" runat="server" clientidmode="Static" type="hidden" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Other Phones
                                        </label>
                                        <asp:Repeater ID="rOtherPhones" runat="server" OnItemCommand="rOtherPhones_ItemCommand" OnItemDataBound="rOtherPhones_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="option_row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <asp:HiddenField ID="hfCode1" runat="server" Value='<%# Eval("Code") %>' />
                                                            <asp:DropDownList ID="ddlCode1" runat="server" CssClass="form-control l_tel">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtPhone1" runat="server" TextMode="Phone" CssClass="form-control r_tel" Text='<%# Eval("Phone") %>'></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteItem" ToolTip="Delete">
                                                            <img src="/Images/delete.png" alt="Delete"  class="option_row_btn_image" /></asp:LinkButton>
                                                    <div class="clearfix"></div>

                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="option_row">

                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <asp:HiddenField ID="hfCode1" runat="server" Value='<%# Eval("Code") %>' />
                                                            <asp:DropDownList ID="ddlCode1" runat="server" CssClass="form-control l_tel">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtPhone1" runat="server" TextMode="Phone" CssClass="form-control r_tel" Text='<%# Eval("Phone") %>'></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <asp:LinkButton ID="lnkAdd" runat="server" CommandName="AddItem" ToolTip="Add">
                                                                <img src="/Images/plus.png" alt="Add" class="option_row_btn_image" /></asp:LinkButton>
                                                    <div class="clearfix"></div>
                                                </div>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Viewers Count
                                        </label>
                                        <asp:TextBox ID="txtViewersCount" runat="server" TextMode="Number" CssClass="form-control" min="0"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvViewersCount" runat="server" ControlToValidate="txtViewersCount"
                                            EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="S"
                                            Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="chkCompany" runat="server" />
                                                Is Company
                                           
                                            </label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="chkActive" runat="server" />
                                                Is Active
                                           
                                            </label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Ordering
                                        </label>
                                        <asp:TextBox ID="txtOrdering" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="chkSendPush" runat="server" />
                                                Send Push Notification Message
                                            </label>
                                        </div>

                                        <asp:TextBox ID="txtNotMsg" runat="server" TextMode="MultiLine" CssClass="form-control" Height="50px" />
                                    </div>
                                </div>
                                <div class="col-md-6">

                                    <div id="div_areas" runat="server" class="form-group">
                                        <label>Areas to cover</label>
                                        <div>
                                            <label>
                                                <input id="allAreas" runat="server" type="checkbox" onchange="checkAll(this,'areas')" name="chk[]" />
                                                Select All
                                            </label>
                                        </div>
                                        <div id="areas" style="width: 220px; height: 300px; overflow-y: auto;">
                                            <asp:Repeater ID="rAreas" runat="server">
                                                <ItemTemplate>
                                                    <div class="checkbox">
                                                        <label>
                                                            <asp:HiddenField ID="hfAreaId" runat="server" Value='<%# Eval("AreaId") %>' />
                                                            <asp:CheckBox ID="chkCovered" runat="server" CssClass="chkitem" Checked='<%# ((bool)Eval("IsCovered")) %>' />
                                                            <%# Eval("Area") %>
                                                        </label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <hr />
                                    <label>
                                        Photos -  Image size (628 * 408)
                                    </label>
                                    <div class="form-group">
                                        <asp:Repeater ID="rImages" runat="server" OnItemCommand="rImages_ItemCommand">
                                            <ItemTemplate>
                                                <div class="pro_photo_item" style="position: relative;">
                                                    <div>
                                                        <asp:FileUpload ID="fuPhoto" runat="server" CssClass="input" />
                                                        <asp:HiddenField ID="hfPhoto" runat="server" Value='<%# Eval("Photo") %>' />
                                                    </div>
                                                    <div>
                                                        <div class="pull-left" style="position: absolute;">
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteItem" CommandArgument='<%# Eval("Id") %>' ToolTip="Delete"
                                                                Visible='<%# MapIt.Helpers.ParseHelper.GetInt(Eval("Id").ToString()) == 0? false : true %>'>
                                                                <img src="/Images/delete.png" alt="Delete"  style="width:25px; height:25px; border:none;" />
                                                            </asp:LinkButton>
                                                        </div>
                                                        <img src='<%# MapIt.Lib.AppSettings.ServiceWMPhotos + Eval("Photo") %>' alt="" />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <hr />
                                    <div class="text-center">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                            ValidationGroup="S" CausesValidation="true" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false"
                                            OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.col-->
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

