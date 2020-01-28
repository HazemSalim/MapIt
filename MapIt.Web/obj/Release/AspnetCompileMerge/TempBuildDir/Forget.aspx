<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="Forget.aspx.cs" Inherits="MapIt.Web.Forget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function validate() {
            if (document.getElementById("#txtPhone").value == "" && document.getElementById("#txtEmail").value == "") {
                tempAlert(<%= GetGlobalResourceObject("Resource","enter_phone_email") %>);
                document.getElementById("#txtPhone").focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
        <div class="uk-container ">
            <div class="uk-position-relative">

                <!-- Start Site Content -->
                <div class="BodyContainer">
                    <h1 class="uk-heading-line uk-text-left pageTitle">
                        <span>
                            <asp:Literal ID="litTitle" runat="server" />
                        </span></h1>
                    <asp:UpdatePanel ID="upForget" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <fieldset class="uk-fieldset uk-width-1-2@m uk-width-1-1@s">
                                <div class="uk-margin">
                                    <label for="phone" style="display: block;">
                                        <%= GetGlobalResourceObject("Resource","phone") %></label>
                                    <asp:DropDownList ID="ddlCode" runat="server" CssClass="uk-input l_tel">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtPhone" runat="server" Placeholder="<%$ Resources:Resource,phone %>" MaxLength="8" TextMode="Phone" ClientIDMode="Static" CssClass="uk-input r_tel"></asp:TextBox>
                                    <div style="clear: both"></div>
                                </div>
                                <div class="uk-margin">
                                    <label for="email">
                                        <%= GetGlobalResourceObject("Resource","email") %></label>
                                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" ClientIDMode="Static" Placeholder="<%$ Resources:Resource,email %>" CssClass="uk-input"></asp:TextBox>
                                    <div style="clear: both"></div>
                                </div>

                                <asp:Button ID="btnSubmit" runat="server" CssClass="uk-button uk-button-primary uk-width-1-1 uk-margin-small-bottom"
                                    Text='<%$ Resources:Resource,submit %>' OnClientClick="validate();" OnClick="btnSubmit_Click" />

                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
