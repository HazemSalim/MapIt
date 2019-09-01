<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="true" CodeBehind="UserMessages.aspx.cs" Inherits="MapIt.Web.Admin.UserMessages" %>

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
                    <li><a href="Users">Users
                    </a></li>
                    <li class="active">Messages</li>
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
                    <h4 class="page-header">
                        <asp:Literal ID="litTitle" runat="server" /></h4>
                </div>
            </div>
            <!--/.row-->

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Message Details</div>
                        <div class="panel-body">
                            <div class="col-md-6">

                                <div class="form-group">
                                    <label>
                                        Message
                                    </label>
                                    <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ControlToValidate="txtMessage" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>

                                <asp:Button ID="btnSave" runat="server" Text="Send" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                    ValidationGroup="S" CausesValidation="true" />
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
                        <div class="panel-heading">Messages List</div>
                        <div class="panel-body">
                            <asp:GridView runat="server" ID="gvMessages" CssClass="table"
                                ShowFooter="false" AutoGenerateColumns="false"
                                GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                DataKeyField="Id" EmptyDataText="No Data" ShowHeaderWhenEmpty="true"
                                AllowSorting="true" OnSorting="gvMessages_Sorting">
                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <RowStyle VerticalAlign="Middle" />
                                <AlternatingRowStyle CssClass="alt-table-data" />
                                <Columns>
                                    <asp:BoundField DataField="Id" Visible="false" />
                                    <asp:BoundField DataField="Sender" HeaderText="Sender" SortExpression="Sender" />
                                    <asp:BoundField DataField="TextMessage" HeaderText="Text Message" SortExpression="TextMessage" />
                                    <asp:BoundField DataField="AddedOn" HeaderText="Added On" SortExpression="AddedOn" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
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
