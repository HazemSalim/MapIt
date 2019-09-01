<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="True" CodeBehind="Users.aspx.cs" Inherits="MapIt.Web.Admin.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
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
                    <li class="active">Users</li>
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
                    <h1 class="page-header">Users</h1>
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
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        Created Date From 
                                    </label>
                                    <asp:TextBox ID="txtSearchCDateFrom" runat="server" TextMode="Date" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Created Date To 
                                    </label>
                                    <asp:TextBox ID="txtSearchCDateTo" runat="server" TextMode="Date" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtSearchKeyWord" placeholder="Search ... " CssClass="form-control"></asp:TextBox>
                                </div>

                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-default" CausesValidation="false" Text="Print"
                                    OnClientClick="printContent('div_users');" />
                                <asp:Button ID="btnExportExcel" runat="server" CssClass="btn btn-info" CausesValidation="false" Text="Export To Excel"
                                    OnClick="btnExportExcel_Click" />
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        Country
                                    </label>
                                    <asp:DropDownList ID="ddlSearchCountry" runat="server" CssClass="form-control" Width="50%" AppendDataBoundItems="true">
                                        <asp:ListItem Text="All Countries" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group">
                                    <label>
                                        Sex
                                    </label>
                                    <asp:DropDownList ID="ddlSearchSexStatus" runat="server" CssClass="form-control" Width="50%" AppendDataBoundItems="true">
                                        <asp:ListItem Text="All Sex Status" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group">
                                    <label>
                                        Status
                                    </label>
                                    <asp:DropDownList ID="ddlSearchActiveStatus" runat="server" CssClass="form-control" Width="50%" AppendDataBoundItems="true">
                                        <asp:ListItem Text="All Activation Status" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Activate" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Inactivate" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-default">
                            <div class="panel-heading">Users List</div>
                            <div class="panel-body">
                                <div id="div_users" style="display: none;">
                                    <div class="row" style="margin-bottom: 10px;">
                                        <div class="col-xs-6">
                                            <img src="/images/logo.svg" alt="MapIt" />
                                        </div>
                                    </div>
                                    <fieldset>
                                        <legend>Users List</legend>
                                        <div class="row">
                                            <div class="col-xs-6">
                                                <div class="form-group">
                                                    <label>
                                                        Created Date From
                                                    </label>
                                                    <asp:Label ID="lblSearchCDateFrom" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        Created Date To
                                                    </label>
                                                    <asp:Label ID="lblSearchCDateTo" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-6">
                                                <div class="form-group">
                                                    <label>
                                                        Country
                                                    </label>
                                                    <asp:Label ID="lblSearchCountry" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        Sex Status
                                                    </label>
                                                    <asp:Label ID="lblSearchSexStatus" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        Activation Status
                                                    </label>
                                                    <asp:Label ID="lblSearchActiveStatus" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        Search Keyword
                                                    </label>
                                                    <asp:Label ID="lblSearchKeyword" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>

                                    <asp:GridView runat="server" ID="gvUsersExcel" ShowFooter="false" AutoGenerateColumns="false"
                                        GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                        DataKeyField="Id" CssClass="table" EmptyDataText="No Data" ShowHeaderWhenEmpty="true" Visible="true">
                                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                        <RowStyle VerticalAlign="Middle" />
                                        <AlternatingRowStyle CssClass="alt-table-data" />
                                        <Columns>
                                            <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                                            <asp:BoundField DataField="Country.TitleEN" HeaderText="Country" />
                                            <asp:BoundField DataField="Phone" HeaderText="Phone" />
                                            <asp:BoundField DataField="AddedOn" HeaderText="Added On" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                            <asp:BoundField DataField="LastLoginOn" HeaderText="Last Login On" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                            <asp:TemplateField ItemStyle-Width="70px" HeaderText="Active">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <%# (bool)Eval("IsActive") ? "Active" : "Inactive" %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <asp:GridView runat="server" ID="gvUsers" CssClass="table"
                                    ShowFooter="false" AutoGenerateColumns="false"
                                    GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                    DataKeyField="Id" EmptyDataText="No Data" ShowHeaderWhenEmpty="true"
                                    OnRowCommand="gvUsers_RowCommand" AllowSorting="true" OnSorting="gvUsers_Sorting">
                                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <RowStyle VerticalAlign="Middle" />
                                    <AlternatingRowStyle CssClass="alt-table-data" />
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="Id" />
                                        <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName" />
                                        <asp:BoundField DataField="Country.TitleEN" HeaderText="Country" SortExpression="Country.TitleEN" />
                                        <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                                        <asp:BoundField DataField="AddedOn" HeaderText="Added On" SortExpression="AddedOn" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                        <asp:BoundField DataField="LastLoginOn" HeaderText="Last Login On" SortExpression="LastLoginOn" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                        <asp:TemplateField ItemStyle-Width="110px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <img src='<%# Eval("UserPhoto") %>' alt="" style="width: 100px;" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="70px" HeaderText="Active" SortExpression="IsActive">
                                            <ItemTemplate>
                                                <div style="text-align: center;">
                                                    <%# (bool)Eval("IsActive") ? "<i class='fa fa-check' style='font-size:25px;'></i>" : "<i class='fa fa-times' style='font-size:25px;'></i>" %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To Buy Credit">
                                            <ItemTemplate>
                                                <div class="col-sm-12">
                                                    <a href='BuyCredit?id=<%# Eval("Id") %>' title="Buy Credit">
                                                        <i class="fa fa-dot-circle-o" style="font-size: 25px;"></i></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="pull-left">
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditItem" CommandArgument='<%# Eval("Id") %>'
                                                        ToolTip="Edit" Style="float: left; display: block; padding: 0px 8px;">
                                                    <i class="fa fa-pencil" style="font-size:25px;"></i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteItem" CommandArgument='<%# Eval("Id") %>'
                                                        ToolTip="Delete" Style="float: left; display: block; padding: 0px 8px;" OnClientClick="return confirm('Are you sure to delete?');">
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
            </asp:Panel>
            <asp:Panel ID="pnlRecordDetails" runat="server" Visible="false">

                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">User Details</div>
                            <div class="panel-body">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            First Name
                                        </label>
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            LastName
                                        </label>
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label>Sex</label>
                                        <asp:DropDownList ID="ddlSex" runat="server" CssClass="form-control" Style="width: 100%;">
                                            <asp:ListItem Text="" Value="" />
                                            <asp:ListItem Text="Male" Value="1" />
                                            <asp:ListItem Text="Female" Value="2" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Birth Date
                                        </label>
                                        <asp:TextBox ID="txtBDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Country</label>
                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry" SetFocusOnError="true"
                                            EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"
                                            InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label style="display: block;">Phone</label>
                                        <asp:DropDownList ID="ddlCode" runat="server" CssClass="form-control l_tel">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone" CssClass="form-control r_tel"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone"
                                            Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                                            ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
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
                                            EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvConfirmPassword" runat="server" EnableClientScript="true"
                                            Display="Dynamic" ValidationGroup="S" Text="Password not matched" ControlToValidate="txtConfirmPassword"
                                            ControlToCompare="txtPassword" CssClass="alert-text" SetFocusOnError="true"></asp:CompareValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Email
                                        </label>
                                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Other Phones
                                        </label>
                                        <asp:Repeater ID="rOtherPhones" runat="server" OnItemCommand="rOtherPhones_ItemCommand" OnItemDataBound="rOtherPhones_ItemDataBound">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
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
                                            Photo
                                        </label>
                                        <asp:FileUpload ID="fuPhoto" runat="server" />
                                        <br />
                                        <div id="div_old" runat="server" class="row" visible="false">
                                            <span class="datalistitem" style="display: inline-block; width: 150px;">
                                                <div style="text-align: center;">
                                                    <a id="aOld" runat="server" href="" class="highslide " onclick="return hs.expand(this)">
                                                        <img id="imgOld" runat="server" src="" alt="Highslide JS" />
                                                    </a>
                                                </div>
                                            </span>
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

                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                        ValidationGroup="S" CausesValidation="true" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false"
                                        OnClick="btnCancel_Click" />
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
