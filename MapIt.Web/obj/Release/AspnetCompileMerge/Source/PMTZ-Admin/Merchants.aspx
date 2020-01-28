<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="True" CodeBehind="Merchants.aspx.cs" Inherits="MapIt.Web.Admin.Merchants" %>

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
                    <li class="active">Merchants</li>
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
                    <h1 class="page-header">Merchants</h1>
                </div>
            </div>
            <!--/.row-->

            <asp:Panel ID="pnlAllRecords" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-xs-12 text-right">
                                <div class="form-group">
                                    <asp:Button ID="btnAddNew" runat="server" Text="Add new Merchant" CssClass="btn btn-primary"
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
                            </div>

                            <div class="col-md-6">
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

                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-default" CausesValidation="false" Text="Print"
                                    OnClientClick="printContent('div_merchants');" />
                                <asp:Button ID="btnExportExcel" runat="server" CssClass="btn btn-info" CausesValidation="false" Text="Export To Excel"
                                    OnClick="btnExportExcel_Click" />
                            </div>
                        </div>

                        <div class="panel panel-default">
                            <div class="panel-heading">Merchants List</div>
                            <div class="panel-body">
                                <div id="div_merchants" style="display: none;">
                                    <div class="row" style="margin-bottom: 10px;">
                                        <div class="col-xs-6">
                                            <img src="/images/logo.svg" alt="MapIt" />
                                        </div>
                                    </div>
                                    <fieldset>
                                        <legend>Merchants List</legend>
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

                                    <asp:GridView runat="server" ID="gvMerchantsExcel" ShowFooter="false" AutoGenerateColumns="false"
                                        GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                        DataKeyField="Id" CssClass="table" EmptyDataText="No Data" ShowHeaderWhenEmpty="true" Visible="true">
                                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                        <RowStyle VerticalAlign="Middle" />
                                        <AlternatingRowStyle CssClass="alt-table-data" />
                                        <Columns>
                                            <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                                            <asp:BoundField DataField="Country" HeaderText="Country" />
                                            <asp:BoundField DataField="City" HeaderText="City" />
                                            <asp:BoundField DataField="Phone" HeaderText="Phone" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                            <asp:BoundField DataField="AddedOn" HeaderText="Added On" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
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

                                <asp:GridView runat="server" ID="gvMerchants" CssClass="table"
                                    ShowFooter="false" AutoGenerateColumns="false"
                                    GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                    DataKeyField="Id" EmptyDataText="No Data" ShowHeaderWhenEmpty="true"
                                    OnRowCommand="gvMerchants_RowCommand" AllowSorting="true" OnSorting="gvMerchants_Sorting">
                                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <RowStyle VerticalAlign="Middle" />
                                    <AlternatingRowStyle CssClass="alt-table-data" />
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="Id" />
                                        <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName" />
                                        <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                                        <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                                        <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" SortExpression="CompanyName" />
                                        <asp:BoundField DataField="AddedOn" HeaderText="Added On" SortExpression="AddedOn" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                        <asp:TemplateField ItemStyle-Width="70px" HeaderText="Active" SortExpression="IsActive">
                                            <ItemTemplate>
                                                <div style="text-align: center;">
                                                    <%# (bool)Eval("IsActive") ? "<i class='fa fa-check' style='font-size:25px;'></i>" : "<i class='fa fa-times' style='font-size:25px;'></i>" %>
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
                            <div class="panel-heading">Merchant Details</div>
                            <div class="panel-body">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Full Name
                                        </label>
                                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName" SetFocusOnError="true"
                                            EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"
                                            InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Country
                                        </label>
                                        <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            City
                                        </label>
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label style="display: block;">Phone</label>
                                        <asp:TextBox ID="txtCode" runat="server" Placeholder="+965" TextMode="Phone" CssClass="form-control l_tel">
                                        </asp:TextBox>
                                        <asp:TextBox ID="txtPhone" runat="server" MaxLength="8" TextMode="Phone" CssClass="form-control r_tel"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone"
                                            Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                                            ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Email
                                        </label>
                                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Company Name
                                        </label>
                                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName"
                                            Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                                            ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Details
                                        </label>
                                        <asp:TextBox ID="txtDetails" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100px" />
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
