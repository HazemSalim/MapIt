<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="True" CodeBehind="Offers.aspx.cs" Inherits="MapIt.Web.Admin.Offers" %>

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
                    <li class="active">Offers</li>
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
                    <h1 class="page-header">Offers</h1>
                </div>
            </div>
            <!--/.row-->

            <asp:Panel ID="pnlAllRecords" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-xs-12 text-right">
                                <div class="form-group">
                                    <asp:Button ID="btnAddNew" runat="server" Text="Add new Offer" CssClass="btn btn-primary"
                                        OnClick="btnAddNew_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        Country
                                    </label>
                                    <asp:DropDownList ID="ddlSearchCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Text="All Countries" Value=""></asp:ListItem>
                                    </asp:DropDownList>
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
                            </div>
                        </div>

                        <div class="panel panel-default">
                            <div class="panel-heading">Offers List</div>
                            <div class="panel-body">
                                <div id="div_offers" style="display: none;">
                                    <fieldset>
                                        <legend>Offers List</legend>
                                        <div class="row">
                                            <div class="col-xs-6">
                                                <div class="form-group">
                                                    <label>
                                                        Country
                                                    </label>
                                                    <asp:Label ID="lblSearchCountry" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        Search Keyword
                                                    </label>
                                                    <asp:Label ID="lblSearchKeyword" runat="server" CssClass="form-control"></asp:Label>
                                                </div>

                                            </div>
                                            <div class="col-xs-6">
                                            </div>
                                        </div>
                                    </fieldset>

                                    <asp:GridView runat="server" ID="gvOffersExcel" ShowFooter="false" AutoGenerateColumns="false"
                                        GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                        DataKeyField="Id" CssClass="table" EmptyDataText="No Data" ShowHeaderWhenEmpty="true" Visible="true">
                                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                        <RowStyle VerticalAlign="Middle" />
                                        <AlternatingRowStyle CssClass="alt-table-data" />
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="Id" />
                                            <asp:BoundField DataField="TitleEN" HeaderText="Offer" />
                                            <asp:BoundField DataField="Country.TitleEN" HeaderText="Country" />
                                            <asp:BoundField DataField="Phone" HeaderText="Phone" />
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

                                <asp:GridView runat="server" ID="gvOffers" ShowFooter="false" AutoGenerateColumns="false"
                                    GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                    DataKeyField="Id" CssClass="table" EmptyDataText="No Data"
                                    ShowHeaderWhenEmpty="true" OnRowCommand="gvOffers_RowCommand"
                                    AllowSorting="true" OnSorting="gvOffers_Sorting">
                                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <RowStyle VerticalAlign="Middle" />
                                    <AlternatingRowStyle CssClass="alt-table-data" />
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="Id" />
                                        <asp:BoundField DataField="TitleEN" HeaderText="Offer" SortExpression="TitleEN" />
                                        <asp:BoundField DataField="Country.TitleEN" HeaderText="Country" SortExpression="Country.TitleEN" />
                                        <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                                        <asp:BoundField DataField="AddedOn" HeaderText="Added On" SortExpression="AddedOn" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                        <asp:BoundField DataField="Ordering" HeaderText="Ordering" SortExpression="Ordering" />
                                        <asp:TemplateField ItemStyle-Width="110px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <img src='<%# Eval("FinalPhoto") %>' alt="" style="width: 64px;" />
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
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditItem" CommandArgument='<%# Eval("Id") %>'
                                                    ToolTip="Edit" CssClass="grid_button">
                                                        <i class="fa fa-pencil" style="font-size:25px;"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteItem" CommandArgument='<%# Eval("Id") %>'
                                                    ToolTip="Delete" Style="float: left; display: block; padding: 0px 8px;" OnClientClick="return confirm('Are you sure to delete?');">
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
                            <div class="panel-heading">Offer Details</div>
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
                                            Description (EN)
                                        </label>
                                        <asp:TextBox ID="txtDescriptionEN" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100px" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Description (AR)
                                        </label>
                                        <asp:TextBox ID="txtDescriptionAR" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100px" />
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
                                            Link
                                        </label>
                                        <asp:TextBox ID="txtLink" runat="server" TextMode="Url" CssClass="form-control"></asp:TextBox>
                                    </div>
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
                                            Viewers Count
                                        </label>
                                        <asp:TextBox ID="txtViewersCount" runat="server" TextMode="Number" Text="0" step="1" min="0" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvViewersCount" runat="server" ControlToValidate="txtViewersCount"
                                            EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="S"
                                            Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
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

