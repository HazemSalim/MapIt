<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="EditAccount.aspx.cs" Inherits="MapIt.Web.EditAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title><%= GetGlobalResourceObject("Resource","web_title") %> | <%= GetGlobalResourceObject("Resource","edit_account") %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
        <div class="uk-container ">
            <div class="uk-position-relative">

                <!-- Start Site Content -->
                <div class="BodyContainer">
                    <h1 class="uk-heading-line uk-text-left pageTitle"><span><%= GetGlobalResourceObject("Resource","edit_account") %></span></h1>
                    <asp:UpdatePanel ID="upEditAccount" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <fieldset class="uk-fieldset uk-width-1-2@m uk-width-1-1@s">
                                <div class="uk-margin">
                                    <label for="email">
                                        <%= GetGlobalResourceObject("Resource","email") %></label>
                                    <asp:TextBox ID="txtEmail" runat="server" Placeholder="<%$ Resources:Resource,email %>" CssClass="uk-input"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ID="revEmail" ControlToValidate="txtEmail"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="EditAccount"
                                        CssClass="alert-text" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"><%= GetGlobalResourceObject("Resource","invalid_email2") %>
                                    </asp:RegularExpressionValidator>
                                </div>

                                <div class="uk-margin">
                                    <label for="phone" style="display: block;">
                                        <%= GetGlobalResourceObject("Resource","phone") %></label>
                                    <asp:DropDownList ID="ddlCode" runat="server" CssClass="uk-input l_tel">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone" Enabled="false" ClientIDMode="Static" CssClass="uk-input r_tel"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone"
                                        Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                                        ValidationGroup="EditProfile" Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                    <div style="clear: both"></div>
                                </div>

                                <div class="uk-margin">
                                    <label for="birthdate">
                                        <%= GetGlobalResourceObject("Resource","birth_date") %></label>
                                    <asp:TextBox ID="txtBDate" runat="server" TextMode="Date" CssClass="uk-input"></asp:TextBox>
                                </div>

                                <div class="uk-margin">
                                    <label for="sex">
                                        <%= GetGlobalResourceObject("Resource","sex") %></label>
                                    <asp:DropDownList ID="ddlSex" runat="server" CssClass="uk-input" Style="width: 100%;">
                                        <asp:ListItem Text='<%$ Resources:Resource,male %>' Value="1" />
                                        <asp:ListItem Text='<%$ Resources:Resource,female %>' Value="2" />
                                    </asp:DropDownList>
                                </div>

                                <div class="uk-margin">
                                    <label for="password">
                                        <%= GetGlobalResourceObject("Resource","password") %></label>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="uk-input" Placeholder="<%$ Resources:Resource,password %>" />
                                </div>

                                <asp:Button ID="btnSave" runat="server" CssClass="uk-button uk-button-primary uk-width-1-1 uk-margin-small-bottom"
                                    Text='<%$ Resources:Resource,save %>' OnClick="btnSave_Click" ValidationGroup="EditAccount" CausesValidation="true" />

                            </fieldset>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSave" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
