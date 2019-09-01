<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="BuyCredit.aspx.cs" Inherits="MapIt.Web.BuyCredit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title><%= GetGlobalResourceObject("Resource","web_title") %> | <%= GetGlobalResourceObject("Resource","buy_credit") %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
        <div class="uk-container ">
            <div class="uk-position-relative">
                <!-- start body container -->
                <div class="BodyContainer">
                    <h1 class="uk-heading-line uk-text-left pageTitle">
                        <span><%= GetGlobalResourceObject("Resource","buy_credit") %></span>
                    </h1>
                    <div class="uk-container ">

                        <asp:UpdatePanel ID="upPackages" runat="server">
                            <ContentTemplate>
                                <div class="uk-child-width-1-2 uk-child-width-1-3@s uk-grid-match uk-grid-small" uk-grid>
                                    <asp:Repeater ID="rPackages" runat="server" OnItemCommand="rPackages_ItemCommand">
                                        <ItemTemplate>
                                            <div class="uk-card uk-card-default uk-width-1-3@m">
                                                <div class="uk-card-header">
                                                    <div class="uk-grid-small uk-flex-middle" uk-grid>
                                                        <div class="uk-width-auto">
                                                            <img class="uk-border-circle" style="width: 32px; height: 32px;" src="/images/paid.png">
                                                        </div>
                                                        <div class="uk-width-expand">
                                                            <h5 class="uk-card-title uk-margin-remove-bottom"><%# Eval(Resources.Resource.db_title_col) %></h5>
                                                            <p class="uk-text-meta uk-margin-remove-top">
                                                                <%# Eval("Price") %> <%# Culture.ToLower() == "ar-kw" ? GeneralSetting.DefaultCurrency.SymbolAR : GeneralSetting.DefaultCurrency.SymbolEN %>
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="uk-card-body">
                                                    <p><%# Eval(Resources.Resource.db_description_col) %></p>
                                                </div>
                                                <div class="uk-card-footer">
                                                    <asp:LinkButton ID="btnSubs" runat="server" CssClass="uk-button uk-button-text" CommandName="BuyItem" CommandArgument='<%# Eval("Id") %>'>
                                                <span class="icon-money"></span>
                                                <span><%= GetGlobalResourceObject("Resource","buy") %>
                                                </span>
                                                    </asp:LinkButton>


                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
