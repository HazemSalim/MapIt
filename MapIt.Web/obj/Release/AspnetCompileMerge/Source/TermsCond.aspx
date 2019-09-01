<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TermsCond.aspx.cs" Inherits="MapIt.Web.TermsCond" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%= GetGlobalResourceObject("Resource","web_title") %> | <%= GetGlobalResourceObject("Resource","terms_cond") %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
        <div class="uk-container ">
            <div class="uk-position-relative">
                <!-- Start Site Content -->
                <div class="BodyContainer">
                    <h1 class="uk-heading-line uk-text-left pageTitle">
                        <span><%= GetGlobalResourceObject("Resource","terms_cond") %>
                        </span>
                    </h1>
                    <div class="uk-container ">
                        <div class="uk-panel">
                            <p>
                                <asp:Literal ID="litContent" runat="server" />
                            </p>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
