<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="ChangePassword.aspx.cs" Inherits="MapIt.Web.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title><%= GetGlobalResourceObject("Resource","web_title") %> | <%= GetGlobalResourceObject("Resource","change_password") %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
        <div class="uk-container ">
            <div class="uk-position-relative">

                <!-- Start Site Content -->
                <div class="BodyContainer">
                    <h1 class="uk-heading-line uk-text-left pageTitle">
                        <span><%= GetGlobalResourceObject("Resource","change_password") %>
                        </span></h1>
                    <asp:UpdatePanel ID="upChangePassword" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <fieldset class="uk-fieldset uk-width-1-2@m uk-width-1-1@s">
                                <div class="uk-margin">
                                    <label for="old_password">
                                        <%= GetGlobalResourceObject("Resource","old_password") %></label>
                                    <asp:TextBox ID="txtOldPassword" runat="server" Placeholder="<%$ Resources:Resource,old_password %>" TextMode="Password"
                                        CssClass="uk-input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="txtOldPassword"
                                        Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                                        ValidationGroup="ChangePassword" Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                                <div class="uk-margin">
                                    <label for="new_password">
                                        <%= GetGlobalResourceObject("Resource","new_password") %></label>
                                    <asp:TextBox ID="txtNewPassword" runat="server" Placeholder="<%$ Resources:Resource,new_password %>" TextMode="Password"
                                        CssClass="uk-input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                                        Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                                        ValidationGroup="ChangePassword" Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revNewPassword" runat="server" ControlToValidate="txtNewPassword"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="ChangePassword" CssClass="alert-text" SetFocusOnError="true"
                                        ValidationExpression="^.*(?=.{6,}).*$">
                                <%= GetGlobalResourceObject("Resource","msg_validate_password") %>
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="uk-margin">
                                    <label for="confirm_password">
                                        <%= GetGlobalResourceObject("Resource","confirm_password") %></label>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" Placeholder="<%$ Resources:Resource,confirm_password %>" TextMode="Password"
                                        CssClass="uk-input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                                        Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                                        ValidationGroup="ChangePassword" Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvConfirmPassword" runat="server" EnableClientScript="true"
                                        Display="Dynamic" ValidationGroup="Signup" Text='<%$ Resources:Resource,password_not_match %>' ControlToValidate="txtConfirmPassword"
                                        ControlToCompare="txtNewPassword" CssClass="alert-text" SetFocusOnError="true"></asp:CompareValidator>
                                </div>

                                <asp:Button ID="btnSave" runat="server" CssClass="uk-button uk-button-primary uk-width-1-1 uk-margin-small-bottom"
                                    Text='<%$ Resources:Resource,save %>' OnClick="btnSave_Click" ValidationGroup="ChangePassword" CausesValidation="true" />

                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
