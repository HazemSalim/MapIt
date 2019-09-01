<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="True" CodeBehind="UsersPackages.aspx.cs" Inherits="MapIt.Web.Admin.UsersPackages" %>

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
                    <li class="active">Users Packages</li>
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
                    <h1 class="page-header">Users Packages</h1>
                </div>
            </div>
            <!--/.row-->
            <asp:Panel ID="pnlAllRecords" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-xs-12 text-right">
                                <div class="form-group">
                                    <asp:Button ID="btnAddNew" runat="server" Text="Add new Package" CssClass="btn btn-primary"
                                        OnClick="btnAddNew_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">Packages List</div>
                            <div class="panel-body">
                                <asp:GridView runat="server" ID="gvPackages" CssClass="table"
                                    ShowFooter="false" AutoGenerateColumns="false"
                                    GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                    DataKeyField="Id" EmptyDataText="No Data" ShowHeaderWhenEmpty="true"
                                    OnRowCommand="gvPackages_RowCommand" AllowSorting="true" OnSorting="gvPackages_Sorting">
                                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <RowStyle VerticalAlign="Middle" />
                                    <AlternatingRowStyle CssClass="alt-table-data" />
                                    <Columns>
                                        <asp:BoundField DataField="Id" Visible="false" />
                                        <asp:BoundField DataField="TitleEN" HeaderText="Title (EN)" SortExpression="TitleEN" />
                                        <asp:BoundField DataField="TitleAR" HeaderText="Title (AR)" SortExpression="TitleAR" />
                                        <asp:TemplateField ItemStyle-Width="70px" HeaderText="Price" SortExpression="Price">
                                            <ItemTemplate>
                                                <div style="text-align: center;">
                                                    <%# Eval("Price") %> KWD
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                                <div class="text-right">
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditItem" CommandArgument='<%# Eval("Id") %>' ToolTip="Edit">
                                                        <i class="fa fa-pencil" style="font-size:25px;"></i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteItem" CommandArgument='<%# Eval("Id") %>' ToolTip="Delete"
                                                        OnClientClick="return confirm('Are you sure to delete?');">
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
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Package Details</div>
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

                                <div class="form-group" style="display: none;">
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
                                    <label>
                                        Price (KWD)
                                    </label>
                                    <asp:TextBox ID="txtPrice" runat="server" TextMode="Number" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ControlToValidate="txtPrice" SetFocusOnError="true"
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
                            <div class="col-md-8">

                                <div class="form-group">
                                    <label>
                                        Description (EN)
                                    </label>
                                    <cuteeditor:editor id="txtDescriptionEN" runat="server" width="100%" height="500">
                                        </cuteeditor:editor>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Description (AR)
                                    </label>
                                    <cuteeditor:editor id="txtDescriptionAR" runat="server" width="100%" height="500">
                                        </cuteeditor:editor>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
