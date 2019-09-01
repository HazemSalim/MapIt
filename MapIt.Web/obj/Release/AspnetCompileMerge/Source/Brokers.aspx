<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Brokers.aspx.cs" Inherits="MapIt.Web.Brokers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%= GetGlobalResourceObject("Resource","web_title") %> | <%= GetGlobalResourceObject("Resource","brokers") %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upBrokers" runat="server">
        <ContentTemplate>
            <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
                <div class="navBG">
                    <div class="uk-container  filterSearchNav">
                        <div class="uk-position-relative">
                            <nav class="uk-navbar-container uk-margin">
                                <div class="nav-overlay uk-navbar-left">
                                    <nav class="uk-navbar-container fiterNavMargin" uk-navbar>
                                        <div class="uk-navbar-left">
                                            <ul class="uk-navbar-nav navFilter navbar-nav-padding">
                                                <li>
                                                    <a href="">
                                                        <asp:Literal ID="litCity" runat="server" Text="<%$ Resources:Resource,all_cities %>" />
                                                    </a>
                                                    <div class="uk-navbar-dropdown">
                                                        <ul class="uk-nav uk-navbar-dropdown-nav" style="min-width: 150px;">
                                                            <asp:Repeater ID="rCities" runat="server" OnItemCommand="rCities_ItemCommand">
                                                                <HeaderTemplate>
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkCity" CommandArgument="" CommandName="City" runat="server"><%= GetGlobalResourceObject("Resource","all_cities") %></asp:LinkButton>
                                                                    </li>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkCity" CommandArgument='<%# Eval("Id") %>' CommandName="City" runat="server"><%# Eval(Resources.Resource.db_title_col) %></asp:LinkButton>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ul>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </nav>
                                </div>
                            </nav>
                        </div>
                    </div>
                </div>
                <div class="uk-container ">
                    <div class="uk-position-relative">
                        <!-- start body container -->
                        <div class="BodyContainer">
                            <h1 class="uk-heading-line uk-text-left pageTitle">
                                <span>
                                    <%= GetGlobalResourceObject("Resource","brokers") %></span>
                            </h1>
                            <div class="uk-container ">
                                <div class="uk-child-width-1-2 uk-child-width-1-3@s uk-grid-match uk-grid-small" uk-grid>
                                    <asp:Repeater ID="rBrokers" runat="server">
                                        <ItemTemplate>
                                            <div class="uk-text-center">
                                                <div class="uk-inline-clip uk-transition-toggle">
                                                    <a href='<%# Eval("PageName", "../Bro/{0}") %>' title='<%# Eval("FullName") %>'>
                                                        <img class="uk-transition-scale-up uk-transition-opaque" src='<%# Eval("FinalPhoto") %>' style="height: 180px;" alt='<%# Eval("FullName") %>'>
                                                        <div class="uk-transition-fade uk-position-cover uk-position-small uk-overlay uk-overlay-default uk-flex uk-flex-center uk-flex-middle">
                                                            <div class="uk-h4 uk-margin-remove">
                                                                <h5>
                                                                    <%# MapIt.Helpers.PresentHelper.StringLimit(Eval(Resources.Resource.db_details_col), 25) %>
                                                                </h5>
                                                            </div>
                                                        </div>
                                                    </a>
                                                </div>
                                                <p class="uk-margin-small-top"><%# Eval("FullName") %></p>
                                                <div class="detailsIconsContainer">

                                                    <span class="uk-margin-left">
                                                        <i class="fa fa-phone"></i>
                                                        <span>
                                                            <a href="tel:<%# Eval("Phone") %>">
                                                                <%# Eval("Phone") %></a></span>
                                                    </span>
                                                    <span class="uk-margin-left">
                                                        <i class="fa fa-envelope"></i>
                                                        <span>
                                                            <a href="mailto:<%# Eval("Email") %>">
                                                                <%# Eval("Email") %></a></span>
                                                    </span>
                                                </div>
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

