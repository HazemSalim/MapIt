<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="True" CodeBehind="PropertyTypes.aspx.cs" Inherits="MapIt.Web.Admin.PropertyTypes" %>

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
                    <li class="active">Property Types</li>
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
                    <h1 class="page-header">Property Types</h1>
                </div>
            </div>
            <!--/.row-->

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Type Details</div>
                        <div class="panel-body">
                            <div class="col-md-6">
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
                                        Color
                                    </label>
                                    <asp:TextBox ID="txtColor" runat="server" TextMode="Color" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvColor" runat="server" ControlToValidate="txtColor" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Ordering
                                    </label>
                                    <asp:TextBox ID="txtOrdering" runat="server" TextMode="Number" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvOrdering" runat="server" ControlToValidate="txtOrdering"
                                        EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>

                                <asp:Button ID="btnSave" runat="server" Text="Add new Type" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                    ValidationGroup="S" CausesValidation="true" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false"
                                    OnClick="btnCancel_Click" Visible="false" />
                            </div>
                            <div class="col-md-6">
                            </div>

                        </div>
                    </div>
                </div>
                <!-- /.col-->
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Types List</div>
                        <div class="panel-body">
                            <asp:GridView runat="server" ID="gvTypes" CssClass="table"
                                ShowFooter="false" AutoGenerateColumns="false"
                                GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                DataKeyField="Id" EmptyDataText="No Data" ShowHeaderWhenEmpty="true"
                                OnRowCommand="gvTypes_RowCommand" AllowSorting="true" OnSorting="gvTypes_Sorting">
                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <RowStyle VerticalAlign="Middle" />
                                <AlternatingRowStyle CssClass="alt-table-data" />
                                <Columns>
                                    <asp:BoundField DataField="Id" Visible="false" />
                                    <asp:BoundField DataField="TitleEN" HeaderText="Title (EN)" SortExpression="TitleEN" />
                                    <asp:BoundField DataField="TitleAR" HeaderText="Title (AR)" SortExpression="TitleAR" />
                                    <asp:TemplateField HeaderText="Color" SortExpression="Color">
                                        <ItemTemplate>
                                            <div id="preview" style="width: 25px; height: 25px; border: 1px solid #000; margin: 0 3px; background-color: <%# Eval("Color") %>;">
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Ordering" HeaderText="Ordering" SortExpression="Ordering" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditItem" CommandArgument='<%# Eval("Id")+","+ ((GridViewRow) Container).RowIndex %>'
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
