<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginBox.ascx.cs" Inherits="MapIt.Web.Controls.LoginBox" %>

<div class="uk-modal-dialog uk-modal-body">
    <button class="uk-modal-close-default" type="button" uk-close></button>
    <fieldset class="uk-fieldset">

        <legend class="uk-legend"><%= GetGlobalResourceObject("Resource","login") %></legend>

        <div class="uk-margin">
            <asp:DropDownList ID="ddlCode" runat="server" CssClass="uk-input l_tel">
            </asp:DropDownList>
            <asp:TextBox ID="txtPhone" runat="server" Placeholder="<%$ Resources:Resource,phone %>" TextMode="Phone" ClientIDMode="Static" CssClass="uk-input r_tel"></asp:TextBox>
            <div style="clear:both"></div>
        </div>

        <div class="uk-margin">
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
        <asp:Button ID="btnLogin" runat="server" CssClass="uk-button uk-button-primary uk-width-1-1" Text='<%$ Resources:Resource,login %>'
            OnClick="btnLogin_Click" ValidationGroup="Login" CausesValidation="true" />
    </fieldset>
</div>
