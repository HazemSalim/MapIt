<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="true" CodeBehind="Admins.aspx.cs" Inherits="MapIt.Web.Admin.Admins" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <ol class="breadcrumb">
                    <li><a href=".">
                        <i class="fa fa-home"></i>
                    </a></li>
                    <li class="active">Admin Users</li>
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
                    <h1 class="page-header">Admin Users</h1>
                </div>
            </div>
            <!--/.row-->

            <asp:Panel ID="pnlAllRecords" runat="server">

                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-xs-12 text-right">
                                <div class="form-group">
                                    <asp:Button ID="btnAddNew" runat="server" Text="Add new User" CssClass="btn btn-primary"
                                        OnClick="btnAddNew_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">Admin Users List</div>
                            <div class="panel-body">
                                <asp:GridView runat="server" ID="gvUsers" CssClass="table"
                                    ShowFooter="false" AutoGenerateColumns="false"
                                    GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                    DataKeyField="Id" EmptyDataText="No Data" ShowHeaderWhenEmpty="true"
                                    OnRowCommand="gvUsers_RowCommand" AllowSorting="true" OnSorting="gvUsers_Sorting">
                                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <RowStyle VerticalAlign="Middle" />
                                    <AlternatingRowStyle CssClass="alt-table-data" />
                                    <Columns>
                                        <asp:BoundField DataField="Id" Visible="false" />
                                        <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName" />
                                        <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                        <asp:BoundField DataField="AddedOn" HeaderText="Added On" SortExpression="AddedOn" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                        <asp:BoundField DataField="LastLoginOn" HeaderText="Last Login On" SortExpression="LastLoginOn" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                        <asp:TemplateField ItemStyle-Width="70px" HeaderText="Active" SortExpression="IsActive">
                                            <ItemTemplate>
                                                <div style="text-align: center;">
                                                    <%# (bool)Eval("IsActive") ? "<i class='fa fa-check' style='font-size:25px;'></i>" : "<i class='fa fa-times' style='font-size:25px;'></i>" %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditItem" CommandArgument='<%# Eval("Id") %>'
                                                    CssClass="grid_button" ToolTip="Edit">
                                                        <i class="fa fa-pencil" style="font-size:25px;"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteItem" CommandArgument='<%# Eval("Id") %>'
                                                    CssClass="grid_button" ToolTip="Delete" OnClientClick="return confirm('Are you sure to delete?');"
                                                    Visible='<%# MapIt.Helpers.ParseHelper.GetInt(Eval("Id").ToString()).Value > 1 ? true : false %>'>
                                                        <i class="fa fa-trash" style="font-size:25px;"></i>
                                                </asp:LinkButton>
                                                </div>
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
                <!--/.row-->
            </asp:Panel>
            <asp:Panel ID="pnlRecordDetails" runat="server" Visible="false">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">Admin User Details</div>
                            <div class="panel-body">
                                <div class="col-md-6">

                                    <div class="form-group">
                                        <label>
                                            Full Name
                                        </label>
                                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName"
                                            EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="S"
                                            Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group">
                                        <label style="display: block;">Phone</label>
                                        <asp:DropDownList ID="ddlCode" runat="server" CssClass="form-control l_tel">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone" CssClass="form-control r_tel"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Email
                                        </label>
                                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" SetFocusOnError="true"
                                            EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"
                                            InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            UserName
                                        </label>
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvtxtUserName" runat="server" ControlToValidate="txtUserName"
                                            EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Password
                                        </label>
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                            EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword"
                                            EnableClientScript="true" Display="Dynamic" ValidationGroup="S" CssClass="alert-text" SetFocusOnError="true"
                                            ValidationExpression="^.*(?=.{6,}).*$">
                                                Password must be at least 6 character
                                        </asp:RegularExpressionValidator>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Confirm Password
                                        </label>
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                                            EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field"
                                            CssClass="alert-text" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvNewPassConfirm" runat="server" EnableClientScript="true"
                                            Display="Dynamic" ValidationGroup="S" Text="Password not matched" ControlToValidate="txtConfirmPassword"
                                            ControlToCompare="txtPassword" CssClass="alert-text" SetFocusOnError="true"></asp:CompareValidator>
                                    </div>
                                    <div class="form-group">
                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="chkActive" runat="server" />
                                                Is Active
                                           
                                            </label>
                                        </div>
                                    </div>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                        ValidationGroup="S" CausesValidation="true" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false"
                                        OnClick="btnCancel_Click" />
                                </div>
                                <div class="col-md-6">

                                    <div id="div_perm" runat="server" class="form-group">
                                        <label>Permissions - Pages to access</label>
                                        <div>
                                            <label>
                                                <input type="checkbox" onchange="checkAll(this,'perm')" name="chk[]" />
                                                Select All
                                            </label>
                                        </div>
                                        <div id="perm">
                                            <asp:Repeater ID="rPermissions" runat="server">
                                                <ItemTemplate>
                                                    <div class="checkbox">
                                                        <label>
                                                            <asp:HiddenField ID="hfAdminPageId" runat="server" Value='<%# Eval("AdminPageId") %>' />
                                                            <asp:CheckBox ID="chkAccessible" runat="server" CssClass="chkitem" Checked='<%# ((bool)Eval("IsAccessible")) %>' />
                                                            <%# Eval("AdminPage") %>
                                                        </label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <!-- /.col-->
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
