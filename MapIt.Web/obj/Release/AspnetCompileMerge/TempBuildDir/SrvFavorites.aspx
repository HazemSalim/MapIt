<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SrvFavorites.aspx.cs" Inherits="MapIt.Web.SrvFavorites" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%= GetGlobalResourceObject("Resource","web_title") %> | <%= GetGlobalResourceObject("Resource","my_favorites_srv") %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upFavoritesSrv" runat="server">
        <ContentTemplate>
            <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
                <div class="uk-container ">
                    <div class="uk-position-relative">
                        <!-- start body container -->
                        <div class="BodyContainer">
                            <h1 class="uk-heading-line uk-text-left pageTitle">
                                <span>
                                    <%= GetGlobalResourceObject("Resource","my_favorites_srv") %>
                                </span>
                            </h1>
                            <div class="uk-container ">
                                <div class="uk-child-width-1-3 uk-child-width-1-3@s uk-grid-match uk-grid-small" uk-grid>
                                    <asp:Repeater ID="rFavSrvs" runat="server" OnItemCommand="rFavSrvs_ItemCommand">
                                        <ItemTemplate>
                                            <div class="uk-text-center">
                                                <div class="uk-inline-clip uk-transition-toggle">
                                                    <div class="uk-position-bottom-right" style="padding: 5px">
                                                        <asp:HiddenField ID="proId" runat="server" Value='<%# Eval("Id") %>' />
                                                        <asp:LinkButton ID="lnkDeleteFav" CommandName="DeleteFav" CommandArgument='<%# Eval("Id") %>' runat="server">
                                                                <i class="fa fa-heart fa-lg"></i>
                                                        </asp:LinkButton>
                                                    </div>

                                                    <a href='<%# Eval("PageName", "../Srv/{0}") %>' title='<%# Eval("Title") %>'>
                                                        <img class="uk-transition-scale-up uk-transition-opaque" src='<%# Eval("ServicePhoto") %>' style="height: 180px;" alt='<%# Eval("Title") %>'>
                                                        <div class="uk-transition-fade uk-position-cover uk-position-small uk-overlay uk-overlay-default uk-flex uk-flex-center uk-flex-middle">
                                                            <div class="uk-h4 uk-margin-remove">
                                                                <div>
                                                                    <%# Eval("ExYears") %> <%= GetGlobalResourceObject("Resource","years_ex") %>
                                                                </div>
                                                                <h5>
                                                                    <%# MapIt.Helpers.PresentHelper.StringLimit(Eval("Description"), 25) %>
                                                                </h5>
                                                            </div>
                                                        </div>
                                                    </a>
                                                </div>
                                                <p class="uk-margin-small-top"><%# Eval("Title") %></p>
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

