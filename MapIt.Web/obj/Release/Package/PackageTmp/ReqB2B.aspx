<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="ReqB2B.aspx.cs" Inherits="MapIt.Web.ReqB2B" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title><%= GetGlobalResourceObject("Resource","web_title") %> | <%= GetGlobalResourceObject("Resource","b2b") %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
        <div class="uk-container ">
            <div class="uk-position-relative">

                <!-- Start Site Content -->
                <div class="BodyContainer">
                    <h1 class="uk-heading-line uk-text-left pageTitle"><span><%= GetGlobalResourceObject("Resource","b2b") %></span></h1>
                    <asp:UpdatePanel ID="upRequest" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <fieldset class="uk-fieldset uk-width-1-2@m uk-width-1-1@s">
                                <div class="uk-margin">
                                    <label for="full_name">
                                        <%= GetGlobalResourceObject("Resource","full_name") %></label>
                                    <asp:TextBox ID="txtFullName" runat="server" Placeholder="<%$ Resources:Resource,full_name %>" CssClass="uk-input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName"
                                        Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                                        ValidationGroup="Request" Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>

                                <div class="uk-margin">
                                    <label for="country">
                                        <%= GetGlobalResourceObject("Resource","country") %>
                                    </label>
                                    <asp:TextBox ID="txtCountry" runat="server" Placeholder="<%$ Resources:Resource,country %>" CssClass="uk-input"></asp:TextBox>
                                </div>

                                <div class="uk-margin">
                                    <label for="city">
                                        <%= GetGlobalResourceObject("Resource","city") %>
                                    </label>
                                    <asp:TextBox ID="txtCity" runat="server" Placeholder="<%$ Resources:Resource,city %>" CssClass="uk-input"></asp:TextBox>
                                </div>

                                <div class="uk-margin">
                                    <label for="phone" style="display: block;">
                                        <%= GetGlobalResourceObject("Resource","phone") %></label>
                                    <asp:TextBox ID="txtCode" runat="server" Placeholder="+965" TextMode="Phone" CssClass="uk-input l_tel">
                                    </asp:TextBox>
                                    <asp:TextBox ID="txtPhone" runat="server" Placeholder="<%$ Resources:Resource,phone %>" TextMode="Phone" ClientIDMode="Static" CssClass="uk-input r_tel"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone"
                                        Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                                        ValidationGroup="Request" Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                    <div style="clear: both"></div>
                                </div>

                                <div class="uk-margin">
                                    <label for="email">
                                        <%= GetGlobalResourceObject("Resource","email") %></label>
                                    <asp:TextBox ID="txtEmail" runat="server" Placeholder="<%$ Resources:Resource,email %>" CssClass="uk-input"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ID="revEmail" ControlToValidate="txtEmail"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="Request"
                                        CssClass="alert-text" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"><%= GetGlobalResourceObject("Resource","invalid_email2") %>
                                    </asp:RegularExpressionValidator>
                                </div>

                                <div class="uk-margin">
                                    <label for="com_name">
                                        <%= GetGlobalResourceObject("Resource","com_name") %></label>
                                    <asp:TextBox ID="txtComName" runat="server" Placeholder="<%$ Resources:Resource,com_name %>" CssClass="uk-input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvComName" runat="server" ControlToValidate="txtComName"
                                        Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                                        ValidationGroup="Request" Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>

                                <div class="uk-margin">
                                    <label for="details">
                                        <%= GetGlobalResourceObject("Resource","details") %></label>
                                    <asp:TextBox ID="txtDetails" runat="server" TextMode="MultiLine" Height="50" Placeholder="<%$ Resources:Resource,details %>" CssClass="uk-input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDetails" runat="server" ControlToValidate="txtDetails"
                                        Display="Dynamic" EnableClientScript="true" SetFocusOnError="true"
                                        ValidationGroup="Request" Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>

                                <asp:Button ID="btnSend" runat="server" CssClass="uk-button uk-button-primary uk-width-1-1 uk-margin-small-bottom"
                                    Text='<%$ Resources:Resource,send %>' OnClick="btnSend_Click" ValidationGroup="Request" CausesValidation="true" />

                            </fieldset>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSend" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
