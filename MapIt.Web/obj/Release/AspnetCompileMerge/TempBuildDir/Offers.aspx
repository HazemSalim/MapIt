<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Offers.aspx.cs" Inherits="MapIt.Web.Offers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%= GetGlobalResourceObject("Resource","web_title") %> | <%= GetGlobalResourceObject("Resource","offers") %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upOffers" runat="server">
        <ContentTemplate>
            <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
                <div class="uk-container ">
                    <div class="uk-position-relative">
                        <!-- start body container -->
                        <div class="BodyContainer">
                            <h1 class="uk-heading-line uk-text-left pageTitle">
                                <span><%= GetGlobalResourceObject("Resource","offers") %></span>
                            </h1>
                            <div class="uk-container ">
                                <div class="uk-child-width-1-2 uk-child-width-1-3@s uk-grid-match uk-grid-small" uk-grid>
                                    <asp:Repeater ID="rOffers" runat="server">
                                        <ItemTemplate>
                                            <div class="uk-text-center">
                                                <div class="uk-inline-clip uk-transition-toggle">
                                                    <a href='<%# Eval("PageName", "../Ofr/{0}") %>' title='<%# Eval(Resources.Resource.db_title_col) %>'>
                                                        <img class="uk-transition-scale-up uk-transition-opaque" src='<%# Eval("FinalPhoto") %>' style="height: 180px;" alt='<%# Eval(Resources.Resource.db_title_col) %>'>
                                                    </a>
                                                </div>
                                                <p class="uk-margin-small-top"><%# Eval(Resources.Resource.db_title_col) %></p>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
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

