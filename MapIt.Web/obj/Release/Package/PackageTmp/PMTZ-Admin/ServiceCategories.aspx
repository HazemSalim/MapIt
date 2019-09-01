<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="true" Inherits="MapIt.Web.Admin.ServiceCategories" CodeBehind="ServiceCategories.aspx.cs" %>

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
                    <li class="active">Service Categories</li>
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
                    <h1 class="page-header">Service Categories</h1>
                </div>
            </div>
            <!--/.row-->

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Category Details</div>
                        <div class="panel-body">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        Main Category
                                    </label>
                                    <asp:DropDownList ID="ddlMainCategory" runat="server" AppendDataBoundItems="true" CssClass="form-control" Style="width: 100%;">
                                        <asp:ListItem Text="No Main Category" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
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

                                <asp:Button ID="btnSave" runat="server" Text="Add new Category" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                    ValidationGroup="S" CausesValidation="true" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false"
                                    OnClick="btnCancel_Click" Visible="false" />
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        Photo
                                    </label>
                                    <asp:FileUpload ID="fuPhoto" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvPhoto" runat="server" ControlToValidate="fuPhoto" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
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
                                        Ordering
                                    </label>
                                    <asp:TextBox ID="txtOrdering" runat="server" CssClass="form-control" TextMode="Number" Text="0" step="1" min="0" />
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
                            </div>

                        </div>
                    </div>
                </div>
                <!-- /.col-->
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Service Categories List</div>
                        <div class="panel-body">
                            <asp:GridView runat="server" ID="gvCategories" CssClass="table"
                                ShowFooter="false" AutoGenerateColumns="false"
                                GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                DataKeyField="Id" EmptyDataText="No Data" ShowHeaderWhenEmpty="true"
                                OnRowCommand="gvCategories_RowCommand" AllowSorting="true" OnSorting="gvCategories_Sorting">
                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <RowStyle VerticalAlign="Middle" />
                                <AlternatingRowStyle CssClass="AlternativeTableData" />
                                <HeaderStyle CssClass="HeaderTableData" />
                                <Columns>
                                    <asp:BoundField DataField="Id" Visible="false" />
                                    <asp:TemplateField HeaderText="Main Category" SortExpression="MainCategory.TitleEN">
                                        <ItemTemplate>
                                            <%# Eval("ParentId") != null ? Eval("MainServicesCategory.TitleEN") : string.Empty %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TitleEN" HeaderText="Title (EN)" SortExpression="TitleEN" />
                                    <asp:BoundField DataField="TitleAR" HeaderText="Title (AR)" SortExpression="TitleAR" />
                                    <asp:BoundField DataField="Ordering" HeaderText="Ordering" SortExpression="Ordering" />
                                    <asp:TemplateField ItemStyle-Width="110px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <img src='<%# Eval("FinalPhoto") %>' alt="" style="width: 100px;" />
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
                                            <div class="text-right">
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditItem" CommandArgument='<%# Eval("Id")+","+ ((GridViewRow) Container).RowIndex %>' ToolTip="Edit">
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
