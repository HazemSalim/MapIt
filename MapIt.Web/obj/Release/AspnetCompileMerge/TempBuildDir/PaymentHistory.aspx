<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentHistory.aspx.cs" Inherits="MapIt.Web.PaymentHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%= GetGlobalResourceObject("Resource","web_title") %> | <%= GetGlobalResourceObject("Resource","payment_history") %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upSrvs" runat="server">
        <ContentTemplate>
            <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
                <div class="uk-container ">
                    <div class="uk-position-relative">
                        <!-- start body container -->
                        <div class="BodyContainer">
                            <h1 class="uk-heading-line uk-text-left pageTitle">
                                <span>
                                    <%= GetGlobalResourceObject("Resource","payment_history") %>
                                </span>
                            </h1>
                            <div class="uk-container ">
                                <div class="uk-child-width-1-1 uk-child-width-1-1@s uk-grid-match uk-grid-small" uk-grid>
                                    <asp:GridView runat="server" ID="gvLogs" ShowFooter="false" AutoGenerateColumns="false"
                                        GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                        DataKeyField="Id" CssClass="tabledata" EmptyDataText="<%$ Resources:Resource,no_data %>"
                                        ShowHeaderWhenEmpty="true">
                                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                        <RowStyle VerticalAlign="Middle" />
                                        <AlternatingRowStyle CssClass="alternativetabledata" />
                                        <HeaderStyle CssClass="headertabledata" />
                                        <Columns>
                                            <asp:BoundField DataField="Id" Visible="false" />
                                            <asp:BoundField DataField="LogNo" HeaderText="<%$ Resources:Resource,log_no %>" SortExpression="LogNo" />
                                            <asp:BoundField DataField="TransOn" HeaderText="<%$ Resources:Resource,trans_on %>" SortExpression="TransOn" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                            <asp:TemplateField ItemStyle-Width="70px" HeaderText="<%$ Resources:Resource,amount %>" SortExpression="Amount">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <%# Eval("Amount") %> KWD
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TransType" HeaderText="<%$ Resources:Resource,trans_type %>" SortExpression="TransType" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="uk-text-center sizeSpan">
                                <asp:Button ID="btnLoadMore" runat="server" CssClass="uk-button uk-button-primary" Text='<%$ Resources:Resource,load_more %>'
                                    OnClick="btnLoadMore_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

