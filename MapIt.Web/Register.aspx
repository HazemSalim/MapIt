<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="Register.aspx.cs" Inherits="MapIt.Web.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title><%= GetGlobalResourceObject("Resource","web_title") %> | <%= GetGlobalResourceObject("Resource","register") %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
        <div class="uk-container ">
            <div class="uk-position-relative">

                <!-- Start Site Content -->
                <div class="BodyContainer">
                    <h1 class="uk-heading-line uk-text-left pageTitle"><span><%= GetGlobalResourceObject("Resource","register") %></span></h1>
                    <asp:UpdatePanel ID="upRegister" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <fieldset class="uk-fieldset uk-width-1-2@m uk-width-1-1@s">
                                <div class="uk-margin">
                                    <label for="country">
                                        <%= GetGlobalResourceObject("Resource","country") %></label>
                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="uk-input" Style="width: 100%;">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="Register" Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"
                                        InitialValue=""></asp:RequiredFieldValidator>
                                </div>

                                <div class="uk-margin">
                                    <label for="phone" style="display: block;">
                                        <%= GetGlobalResourceObject("Resource","phone") %></label>
                                    <asp:DropDownList ID="ddlCode" runat="server" CssClass="uk-input l_tel">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtPhone" runat="server" Placeholder="<%$ Resources:Resource,phone %>" TextMode="Phone" ClientIDMode="Static" CssClass="uk-input r_tel"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone"
                                        Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                                        ValidationGroup="Register" Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                    <div style="clear: both"></div>
                                </div>

                                <div class="uk-margin">
                                    <label for="email">
                                        <%= GetGlobalResourceObject("Resource","email") %></label>
                                    <asp:TextBox ID="txtEmail" runat="server" Placeholder="<%$ Resources:Resource,email %>" CssClass="uk-input"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ID="revEmail" ControlToValidate="txtEmail"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="Register"
                                        CssClass="alert-text" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"><%= GetGlobalResourceObject("Resource","invalid_email2") %>
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="uk-margin">
                                    <label for="password">
                                        <%= GetGlobalResourceObject("Resource","password") %></label>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Placeholder="<%$ Resources:Resource,password %>" CssClass="uk-input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                        Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                                        ValidationGroup="Register" Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="Register" CssClass="alert-text" SetFocusOnError="true"
                                        ValidationExpression="^.*(?=.{6,}).*$">
                                <%= GetGlobalResourceObject("Resource","msg_validate_password") %>
                                    </asp:RegularExpressionValidator>
                                </div>

                                <div class="uk-margin">
                                    <label for="confirm_password">
                                        <%= GetGlobalResourceObject("Resource","confirm_password") %></label>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Placeholder="<%$ Resources:Resource,confirm_password %>"
                                        CssClass="uk-input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                                        Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                                        ValidationGroup="Register" Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                                        ControlToCompare="txtPassword" Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                                        ValidationGroup="Register" CssClass="alert-text"><%= GetGlobalResourceObject("Resource","password_not_match") %></asp:CompareValidator>
                                </div>
                                <div class="uk-margin uk-grid-small uk-child-width-auto uk-grid">
                                    <label>
                                        <input runat="server" id="chekTerms" class="uk-checkbox" type="checkbox">
                                        <%= GetGlobalResourceObject("Resource","i_agree_to") %>
                                        <a href="/TermsCond" target="_blank"><%= GetGlobalResourceObject("Resource","terms_use") %></a></label>

                                </div>

                                <asp:Button ID="btnRegister" runat="server" CssClass="uk-button uk-button-primary uk-width-1-1 uk-margin-small-bottom"
                                    Text='<%$ Resources:Resource,register %>' OnClick="btnRegister_Click" ValidationGroup="Register" CausesValidation="true" />

                            </fieldset>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnRegister" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
