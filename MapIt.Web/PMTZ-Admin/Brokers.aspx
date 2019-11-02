<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="True" CodeBehind="Brokers.aspx.cs" Inherits="MapIt.Web.Admin.Brokers" %>

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
                    <li class="active">Brokers</li>
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
                    <h1 class="page-header">Brokers</h1>
                </div>
            </div>
            <!--/.row-->

            <asp:Panel ID="pnlAllRecords" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-xs-12 text-right">
                                <div class="form-group">
                                    <asp:Button ID="btnAddNew" runat="server" Text="Add new Broker" CssClass="btn btn-primary"
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
                                    <label>
                                        City
                                    </label>
                                    <asp:DropDownList ID="ddlSearchCity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Text="All Cities" Value=""></asp:ListItem>
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
                            <div class="panel-heading">Brokers List</div>
                            <div class="panel-body">
                                <div id="div_brokers" style="display: none;">
                                    <fieldset>
                                        <legend>Brokers List</legend>
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
                                                        City
                                                    </label>
                                                    <asp:Label ID="lblSearchCity" runat="server" CssClass="form-control"></asp:Label>
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

                                    <asp:GridView runat="server" ID="gvBrokersExcel" ShowFooter="false" AutoGenerateColumns="false"
                                        GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                        DataKeyField="Id" CssClass="table" EmptyDataText="No Data" ShowHeaderWhenEmpty="true" Visible="true">
                                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                        <RowStyle VerticalAlign="Middle" />
                                        <AlternatingRowStyle CssClass="alt-table-data" />
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="Id" />
                                            <asp:BoundField DataField="FullName" HeaderText="Broker" />
                                            <asp:BoundField DataField="City.Country.TitleEN" HeaderText="Country" />
                                            <asp:BoundField DataField="City.TitleEN" HeaderText="City" />
                                            <asp:BoundField DataField="Phone" HeaderText="Phone" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" />
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

                                <asp:GridView runat="server" ID="gvBrokers" ShowFooter="false" AutoGenerateColumns="false"
                                    GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                    DataKeyField="Id" CssClass="table" EmptyDataText="No Data"
                                    ShowHeaderWhenEmpty="true" OnRowCommand="gvBrokers_RowCommand"
                                    AllowSorting="true" OnSorting="gvBrokers_Sorting">
                                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <RowStyle VerticalAlign="Middle" />
                                    <AlternatingRowStyle CssClass="alt-table-data" />
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="Id" />
                                        <asp:BoundField DataField="FullName" HeaderText="Broker" SortExpression="FullName" />
                                        <asp:BoundField DataField="City.Country.TitleEN" HeaderText="Country" SortExpression="City.Country.TitleEN" />
                                        <asp:BoundField DataField="City.TitleEN" HeaderText="City" SortExpression="City.TitleEN" />
                                        <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                        <asp:BoundField DataField="AddedOn" HeaderText="Added On" SortExpression="AddedOn" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
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
                            <div class="panel-heading">Broker Details</div>
                            <div class="panel-body">
                                <div class="col-md-4">
                                     <div class="form-group">
                                        <label>
                                            Broker User
                                        </label>
                                         <asp:DropDownList ID="ddlUsers" runat="server" AppendDataBoundItems="true" AutoPostBack="true" 
                                             DataTextField="Email" DataValueField="Id"
                                            OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Text="Select Broker" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUsers" SetFocusOnError="true"
                                            EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"
                                            InitialValue=""></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group">
                                        <label>
                                            Full Name
                                        </label>
                                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName" SetFocusOnError="true"
                                            EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Country
                                        </label>
                                        <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Text="Select Country" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry" SetFocusOnError="true"
                                            EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"
                                            InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            City
                                        </label>
                                        <asp:DropDownList ID="ddlCity" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                            <asp:ListItem Text="Select City" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity" SetFocusOnError="true"
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
                                            Email
                                        </label>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Link
                                        </label>
                                        <asp:TextBox ID="txtLink" runat="server" TextMode="Url" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Details (EN)
                                        </label>
                                        <asp:TextBox ID="txtDetailsEN" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50px" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Details (AR)
                                        </label>
                                        <asp:TextBox ID="txtDetailsAR" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50px" />
                                    </div>
                                </div>
                                <div class="col-md-4">

                                    <div id="div_areas" runat="server" class="form-group">
                                        <label>Areas to cover</label>
                                        <div>
                                            <label>
                                                <input id="allAreas" runat="server" type="checkbox" onchange="checkAll(this,'areas')" name="chk[]" />
                                                Select All
                                            </label>
                                        </div>
                                        <div id="areas" style="width: 200px; height: 300px; overflow-y: auto;">
                                            <asp:Repeater ID="rAreas" runat="server">
                                                <ItemTemplate>
                                                    <div class="checkbox">
                                                        <label>
                                                            <asp:HiddenField ID="hfAreaId" runat="server" Value='<%# Eval("AreaId") %>' />
                                                            <asp:CheckBox ID="chkCovered" runat="server" CssClass="chkitem" Checked='<%# ((bool)Eval("IsCovered")) %>' />
                                                            <%# Eval("Area") %>
                                                        </label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
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

