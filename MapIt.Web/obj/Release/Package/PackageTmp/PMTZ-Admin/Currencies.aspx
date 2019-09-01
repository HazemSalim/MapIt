<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="True" CodeBehind="Currencies.aspx.cs" Inherits="MapIt.Web.Admin.Currencies" %>

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
                    <li class="active">Currencies</li>
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
                    <h1 class="page-header">Currencies</h1>
                </div>
            </div>
            <!--/.row-->

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Currency Details</div>
                        <div class="panel-body">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Title (EN)
                                    </label>
                                    <asp:TextBox ID="txtTitleEN" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvTitleEN" runat="server" ControlToValidate="txtTitleEN" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Title (AR)
                                    </label>
                                    <asp:TextBox ID="txtTitleAR" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvTitleAR" runat="server" ControlToValidate="txtTitleAR" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Symbol (EN)
                                    </label>
                                    <asp:TextBox ID="txtSymbolEN" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvSymbolEN" runat="server" ControlToValidate="txtSymbolEN" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>

                                <asp:Button ID="btnSave" runat="server" Text="Add new Currency" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                    ValidationGroup="S" CausesValidation="true" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false"
                                    OnClick="btnCancel_Click" Visible="false" />

                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Symbol (AR)
                                    </label>
                                    <asp:TextBox ID="txtSymbolAR" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvSymbolAR" runat="server" ControlToValidate="txtSymbolAR" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Digits
                                    </label>
                                    <asp:DropDownList ID="ddlDigits" runat="server" AppendDataBoundItems="true" CssClass="form-control" Style="width: 100%;">
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Format
                                    </label>
                                    <asp:DropDownList ID="ddlFormat" runat="server" AppendDataBoundItems="true" CssClass="form-control" Style="width: 100%;">
                                        <asp:ListItem Text="0.00" Value="0.00"></asp:ListItem>
                                        <asp:ListItem Text="0.000" Value="0.000"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Code
                                    </label>
                                    <asp:TextBox ID="txtCode" runat="server" TextMode="Number" step="1" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvCode" runat="server" ControlToValidate="txtCode" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Exchange Rate
                                    </label>
                                    <asp:TextBox ID="txtExchangeRate" runat="server" TextMode="Number" step="0.01" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvExchangeRate" runat="server" ControlToValidate="txtExchangeRate"
                                        EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group" style="display: none;">
                                    <label>
                                        Notes
                                    </label>
                                    <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50px" />
                                </div>
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="chkActive" runat="server" />
                                            Is Active
                                           
                                        </label>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- /.col-->
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Currencies List</div>
                        <div class="panel-body">
                            <asp:GridView runat="server" ID="gvCurrencies" CssClass="table"
                                ShowFooter="false" AutoGenerateColumns="false"
                                GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                DataKeyField="Id" EmptyDataText="No Data" ShowHeaderWhenEmpty="true"
                                OnRowCommand="gvCurrencies_RowCommand" AllowSorting="true" OnSorting="gvCurrencies_Sorting">
                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <RowStyle VerticalAlign="Middle" />
                                <AlternatingRowStyle CssClass="alt-table-data" />
                                <Columns>
                                    <asp:BoundField DataField="Id" Visible="false" />
                                    <asp:BoundField DataField="TitleEN" HeaderText="Title (EN)" SortExpression="TitleEN" />
                                    <asp:BoundField DataField="TitleAR" HeaderText="Title (AR)" SortExpression="TitleAR" />
                                    <asp:BoundField DataField="SymbolEN" HeaderText="Symbol (EN)" SortExpression="SymbolEN" />
                                    <asp:BoundField DataField="SymbolAR" HeaderText="Symbol (AR)" SortExpression="SymbolAR" />
                                    <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                                    <asp:BoundField DataField="ExchangeRate" HeaderText="Exchange Rate" SortExpression="ExchangeRate" />
                                    <asp:TemplateField ItemStyle-Width="70px" HeaderText="Active" SortExpression="IsActive">
                                        <ItemTemplate>
                                            <div style="text-align: center;">
                                                <%# (bool)Eval("IsActive") ? "<i class='fa fa-check' style='font-size:25px;'></i>" : "<i class='fa fa-times' style='font-size:25px;'></i>" %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditItem" CommandArgument='<%# Eval("Id")+","+ ((GridViewRow) Container).RowIndex %>'
                                                CssClass="grid_button" ToolTip="Edit">
                                                        <i class="fa fa-pencil" style="font-size:25px;"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteItem" CommandArgument='<%# Eval("Id") %>'
                                                CssClass="grid_button" ToolTip="Delete" OnClientClick="return confirm('Are you sure to delete?');"
                                                Visible='<%# MapIt.Helpers.ParseHelper.GetInt(Eval("Id").ToString()).Value > 1 ? true : false %>'>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
