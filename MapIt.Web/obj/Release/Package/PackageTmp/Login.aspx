<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="Login.aspx.cs" Inherits="MapIt.Web.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title><%= GetGlobalResourceObject("Resource","web_title") %> | <%= GetGlobalResourceObject("Resource","login") %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
        <div class="uk-container ">
            <div class="uk-position-relative">

                <!-- Start Site Content -->
                <div class="BodyContainer">
                    <h1 class="uk-heading-line uk-text-left pageTitle">
                        <span><%= GetGlobalResourceObject("Resource","login") %>
                        </span></h1>
                    <asp:UpdatePanel ID="upLogin" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <fieldset class="uk-fieldset uk-width-1-2@m uk-width-1-1@s">
                                <div class="uk-margin">
                                    <label for="phone" style="display: block;">
                                        <%= GetGlobalResourceObject("Resource","phone") %></label>
                                    <asp:DropDownList ID="ddlCode" runat="server" CssClass="uk-input l_tel">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtPhone" runat="server" Placeholder="<%$ Resources:Resource,phone %>" 
                                        MaxLength="8" TextMode="Phone" ClientIDMode="Static" CssClass="uk-input r_tel"></asp:TextBox>
                                    <div style="clear: both"></div>
                                </div>

                                <div class="uk-margin">
                                    <label for="password">
                                        <%= GetGlobalResourceObject("Resource","password") %></label>
                                    <asp:TextBox ID="txtPassword" runat="server" Placeholder="<%$ Resources:Resource,password %>" TextMode="Password" CssClass="uk-input"></asp:TextBox>
                                </div>

                                <div class="uk-margin uk-grid-small uk-child-width-auto uk-grid">
                                    <label>
                                        <input class="uk-checkbox" type="checkbox" checked><%= GetGlobalResourceObject("Resource","remember_me") %></label>
                                </div>

                                <div class="uk-margin uk-grid-small uk-child-width-auto uk-grid">
                                    <a href="/Register" style="display: block;"><%= GetGlobalResourceObject("Resource","ask_register") %></a>
                                </div>
                                <div class="uk-margin uk-grid-small uk-child-width-auto uk-grid" style="display: none; visibility: hidden;">
                                    <a href="/Forget?op=username" style="display: block;"><%= GetGlobalResourceObject("Resource","forget_username") %></a>
                                </div>
                                <div class="uk-margin uk-grid-small uk-child-width-auto uk-grid">
                                    <a href="/Forget?op=password" style="display: block;"><%= GetGlobalResourceObject("Resource","forget_password") %></a>
                                </div>

                                <asp:Button ID="btnLogin" runat="server" CssClass="uk-button uk-button-primary uk-width-1-1 uk-margin-small-bottom"
                                    Text='<%$ Resources:Resource,login %>' OnClick="btnLogin_Click" ValidationGroup="Login" CausesValidation="true" />

                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
