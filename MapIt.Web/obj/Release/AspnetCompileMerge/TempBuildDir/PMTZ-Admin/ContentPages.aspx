<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="True" CodeBehind="ContentPages.aspx.cs" Inherits="MapIt.Web.Admin.ContentPages" %>

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
                    <li class="active">Content Pages</li>
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
                    <h1 class="page-header">Content Pages</h1>
                </div>
            </div>
            <!--/.row-->

            <asp:Panel ID="pnlAllRecords" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-xs-12 text-right">
                                <div class="form-group">
                                    <asp:Button ID="btnAddNew" runat="server" Text="Add New Page" CssClass="btn btn-primary"
                                        OnClick="btnAddNew_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">Pages List</div>
                            <div class="panel-body">
                                <asp:GridView runat="server" ID="gvPages" CssClass="table"
                                    ShowFooter="false" AutoGenerateColumns="false"
                                    GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                    DataKeyField="ID" EmptyDataText="No Data" ShowHeaderWhenEmpty="true"
                                    OnRowCommand="gvPages_RowCommand" AllowSorting="true" OnSorting="gvPages_Sorting">
                                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <RowStyle VerticalAlign="Middle" />
                                    <AlternatingRowStyle CssClass="alt-table-data" />
                                    <Columns>
                                        <asp:BoundField DataField="Id" Visible="false" />
                                        <asp:BoundField DataField="TitleEN" HeaderText="Title (EN)" SortExpression="TitleEN" />
                                        <asp:BoundField DataField="TitleAR" HeaderText="Title (AR)" SortExpression="TitleAR" />
                                        <asp:BoundField DataField="Ordering" HeaderText="Ordering" SortExpression="Ordering" />
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
                            <div class="panel-heading">Page Details</div>
                            <div class="panel-body">
                                <div class="col-md-10">
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
                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="chkHasLink" runat="server" onchange="hideShow(this,'div_link')" />
                                                Has Link
                                           
                                            </label>
                                        </div>
                                    </div>

                                    <div id="div_link" class="form-group">
                                        <label>
                                            Link
                                        </label>
                                        <asp:TextBox ID="txtLink" runat="server" CssClass="form-control" />
                                    </div>

                                    <div class="form-group">
                                        <label>
                                            Content (EN)
                                        </label>
                                        <CuteEditor:Editor ID="txtContentEN" runat="server" Width="100%" Height="500">
                                        </CuteEditor:Editor>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Content (AR)
                                        </label>
                                        <CuteEditor:Editor ID="txtContentAR" runat="server" Width="100%" Height="500">
                                        </CuteEditor:Editor>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Ordering
                                        </label>
                                        <asp:TextBox ID="txtOrdering" runat="server" CssClass="form-control" TextMode="Number" min="0" step="1" />
                                        <asp:RequiredFieldValidator ID="rfvOrdering" runat="server" ControlToValidate="txtOrdering" SetFocusOnError="true"
                                            EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
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
                                <div class="col-md-2">

                                    <div class="form-group">
                                        <label>Places To Show</label>
                                        <div>
                                            <label>
                                                <input type="checkbox" onchange="checkAll(this,'show')" name="chk[]" />
                                                Select All
                                            </label>
                                        </div>
                                        <div id="show">
                                            <asp:Repeater ID="rShows" runat="server">
                                                <ItemTemplate>
                                                    <div class="checkbox">
                                                        <label>
                                                            <asp:HiddenField ID="hfPagePlaceId" runat="server" Value='<%# Eval("PagePlaceId") %>' />
                                                            <asp:CheckBox ID="chkShow" runat="server" CssClass="chkitem" Checked='<%# ((bool)Eval("Show")) %>' />
                                                            <%# Eval("PagePlace") %>
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
