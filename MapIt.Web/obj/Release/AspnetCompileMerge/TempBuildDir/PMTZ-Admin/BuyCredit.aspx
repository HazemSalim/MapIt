<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="True" CodeBehind="BuyCredit.aspx.cs" Inherits="MapIt.Web.Admin.BuyCredit" %>

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
                    <li class="active">Buy Credit</li>
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
                        <asp:Literal ID="litTitle" runat="server" />'s
                        Buy Credit</h4>
                </div>
            </div>
            <!--/.row-->

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Packages</div>
                        <div class="panel-body">
                            <div class="col-sm-12">
                                <asp:Repeater ID="rPackages" runat="server" OnItemCommand="rPackages_ItemCommand">
                                    <ItemTemplate>
                                        <div class="col-xs-12 col-sm-6 col-md-3">
                                            <ul class="pricing p-blue">
                                                <li>
                                                    <div>
                                                        <i class="fa fa-gear"></i>
                                                    </div>
                                                    <big><%# Eval(Resources.Resource.db_title_col) %></big>
                                                </li>
                                                <%# Eval(Resources.Resource.db_description_col) %>
                                                <%--<li>Responsive Design</li>
                                                <li>Color Customization</li>
                                                <li>HTML5 & CSS3</li>
                                                <li>Styled elements</li>--%>
                                                <li>
                                                    <h3><%# Math.Round(MapIt.Helpers.ParseHelper.GetDouble(Eval("Price")).Value * Currency.ExchangeRate, Currency.Digits, MidpointRounding.ToEven) %> <%# Culture.ToLower() == "ar-kw" ? Currency.SymbolAR : Currency.SymbolEN %></h3>
                                                </li>
                                                <li>
                                                    <asp:Button ID="btnBuy" runat="server" CommandName="BuyItem" CommandArgument='<%# Eval("Id") %>' Text='<%$ Resources:Resource,join_now %>' />
                                                </li>
                                            </ul>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.col-->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
