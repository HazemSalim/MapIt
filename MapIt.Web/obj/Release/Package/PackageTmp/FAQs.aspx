<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FAQs.aspx.cs" Inherits="MapIt.Web.FAQs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%= GetGlobalResourceObject("Resource","web_title") %> | <%= GetGlobalResourceObject("Resource","faqs") %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
        <div class="uk-container ">
            <div class="uk-position-relative">
                <!-- Start Site Content -->
                <div class="BodyContainer">
                    <h1 class="uk-heading-line uk-text-left pageTitle">
                        <span><%= GetGlobalResourceObject("Resource","faqs") %>
                            <span class="icon-mapit mapitLogoPageTitle"></span>

                        </span>
                    </h1>
                    <div class="uk-container ">
                        <div class="uk-panel">
                            <ul uk-accordion>
                                <asp:Repeater ID="rFAQs" runat="server">
                                    <ItemTemplate>
                                        <li class="<%# Container.ItemIndex == 0? "uk-open" : string.Empty %>">
                                            <h3 class="uk-accordion-title"><%# Eval(Resources.Resource.db_title_col) %></h3>
                                            <div class="uk-accordion-content">
                                                <span>
                                                    <%# Eval(Resources.Resource.db_content_col) %>
                                                </span>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
