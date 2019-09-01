<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="true" Inherits="MapIt.Web.Admin.Sliders" CodeBehind="Sliders.aspx.cs" %>

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
                    <li class="active">Sliders</li>
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
                    <h1 class="page-header">Sliders</h1>
                </div>
            </div>
            <!--/.row-->

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Slider Details</div>
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
                                <div class="form-group" style="display: none;">
                                    <label>
                                        Country
                                    </label>
                                    <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true" CssClass="form-control" Style="width: 100%;">
                                        <asp:ListItem Text="All Countries" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <asp:Button ID="btnSave" runat="server" Text="Add new Slider" OnClick="btnSave_Click" CssClass="btn btn-primary"
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
                                        Link
                                    </label>
                                    <asp:TextBox ID="txtLink" runat="server" TextMode="Url" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvLink" runat="server" ControlToValidate="txtLink" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="chkOpenBlank" runat="server" />
                                            Open in new webpage
                                           
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Ordering
                                    </label>
                                    <asp:TextBox ID="txtOrdering" runat="server" TextMode="Number" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvOrdering" runat="server" ControlToValidate="txtOrdering"
                                        EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
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
                        <div class="panel-heading">Sliders List</div>
                        <div class="panel-body">
                            <asp:GridView runat="server" ID="gvSliders" CssClass="table"
                                ShowFooter="false" AutoGenerateColumns="false"
                                GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                DataKeyField="Id" EmptyDataText="No Data" ShowHeaderWhenEmpty="true"
                                OnRowCommand="gvSliders_RowCommand" AllowSorting="true" OnSorting="gvSliders_Sorting">
                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <RowStyle VerticalAlign="Middle" />
                                <AlternatingRowStyle CssClass="AlternativeTableData" />
                                <HeaderStyle CssClass="HeaderTableData" />
                                <Columns>
                                    <asp:BoundField DataField="Id" Visible="false" />
                                    <asp:BoundField DataField="TitleEN" HeaderText="Title (EN)" SortExpression="TitleEN" />
                                    <asp:BoundField DataField="TitleAR" HeaderText="Title (AR)" SortExpression="TitleAR" />
                                    <asp:BoundField DataField="Ordering" HeaderText="Ordering" SortExpression="Ordering" />
                                    <asp:TemplateField ItemStyle-Width="110px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <img src='<%# Eval("FinalPhoto") %>' alt="" style="width: 100px;" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="70px" HeaderText="New Webpage" SortExpression="OpenBlank">
                                        <ItemTemplate>
                                            <div style="text-align: center;">
                                                <%# (bool)Eval("OpenBlank") ? "<i class='fa fa-check' style='font-size:25px;'></i>" : "<i class='fa fa-times' style='font-size:25px;'></i>" %>
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
