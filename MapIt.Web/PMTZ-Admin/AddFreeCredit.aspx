<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="True" CodeBehind="AddFreeCredit.aspx.cs" Inherits="MapIt.Web.Admin.AddFreeCredit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).keypress(function (event) {
            if (event.keyCode == 13) {
                $("#<%= btnSearch.ClientID %>").click();
            }
        });
        function CheckAll(aspCheckBoxID, checkVal) {
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox' && elm.name.indexOf(aspCheckBoxID) != -1) {
                    elm.checked = checkVal
                }
            }
        }

        function isItemChecked(aspCheckBoxID) {
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox' && elm.name.indexOf(aspCheckBoxID) != -1 && elm.checked) {
                    return true;
                }
            }

            return false;
        }
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
                    <li class="active">Add Free Credit</li>
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
                    <h1 class="page-header">Add Free Credit</h1>
                </div>
            </div>
            <!--/.row-->

            <div class="row">
                <div class="col-lg-12">
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
                    <hr />
                    <div class="row">
                        <div class="col-xs-6">
                            <div class="form-group">
                                <asp:TextBox runat="server" ID="txtAmount" placeholder="Enter the amount ... " CssClass="form-control" TextMode="Number" min="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount"
                                    EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="A"
                                    Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-xs-6">
                            <div class="form-group">
                                <asp:Button ID="btnAddFree" runat="server" Text="Add Free Credit" CssClass="btn btn-primary"
                                    OnClick="btnAddFree_Click" ValidationGroup="A" CausesValidation="true" />
                            </div>
                        </div>
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading">Users List</div>
                        <div class="panel-body">

                            <asp:GridView runat="server" ID="gvUsers" CssClass="table"
                                ShowFooter="false" AutoGenerateColumns="false"
                                GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                DataKeyField="Id" EmptyDataText="No Data" ShowHeaderWhenEmpty="true"
                                AllowSorting="true" OnSorting="gvUsers_Sorting">
                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <RowStyle VerticalAlign="Middle" />
                                <AlternatingRowStyle CssClass="alt-table-data" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="20px">
                                        <HeaderTemplate>
                                            <input id="chkAllItems" style="border-style: none;" type="checkbox" onclick="CheckAll('chkItem', this.checked)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkItem" runat="server" />
                                            <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Id" HeaderText="Id" />
                                    <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName" />
                                    <asp:BoundField DataField="Country.TitleEN" HeaderText="Country" SortExpression="Country.TitleEN" />
                                    <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                                    <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                                    <asp:BoundField DataField="AddedOn" HeaderText="Added On" SortExpression="AddedOn" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                    <asp:TemplateField ItemStyle-Width="70px" HeaderText="Active" SortExpression="IsActive">
                                        <ItemTemplate>
                                            <div style="text-align: center;">
                                                <%# (bool)Eval("IsActive") ? "<i class='fa fa-check' style='font-size:25px;'></i>" : "<i class='fa fa-times' style='font-size:25px;'></i>" %>
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
    </asp:UpdatePanel>
</asp:Content>
