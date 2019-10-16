<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportAbuse.aspx.cs" Inherits="MapIt.Web.ReportAbuse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="uk-background-fixed uk-background-fixed uk-background-center-center">
        <div class="uk-container">
            <div class="uk-position-relative">
                <div class="BodyContainer">
                    <h1 class="uk-heading-line uk-text-left pageTitle">
                        <span>
                            <asp:Literal ID="litTitle" runat="server" />
                        </span></h1>
                    <asp:UpdatePanel ID="upReportAbuse" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <fieldset class="uk-fieldset uk-width-1-2@m uk-width-1-1@s">
                                <div class="uk-margin">
                                    <label for="ddlReasonTypes">
                                        <%= GetGlobalResourceObject("Resource","reason") %></label>
                                    <asp:DropDownList ID="ddlReasonTypes" runat="server" CssClass="uk-input" AppendDataBoundItems="true">
                                             <asp:ListItem Text='<%$ Resources:Resource,select %>' Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="rfvReasonTypes" runat="server" ControlToValidate="ddlReasonTypes" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="MSrv" Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"
                                        InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                                <div class="uk-margin">
                                    <label for="txtNotes">
                                        <%= GetGlobalResourceObject("Resource","notes") %></label>
                                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Columns="5"  ClientIDMode="Static" Placeholder="<%$ Resources:Resource,notes %>" 
                                        CssClass="uk-input"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNotes" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="MSrv" Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"
                                        InitialValue=""></asp:RequiredFieldValidator>

                                    <div style="clear: both"></div>
                                </div>

                                <asp:Button ID="btnSubmit" runat="server" CssClass="uk-button uk-button-primary uk-width-1-1 uk-margin-small-bottom"
                                    Text='<%$ Resources:Resource,submit %>' OnClick="btnSubmit_Click" ValidationGroup="MSrv" CausesValidation="true" />

                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
