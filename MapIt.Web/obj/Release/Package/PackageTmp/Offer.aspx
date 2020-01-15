<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Offer.aspx.cs" Inherits="MapIt.Web.Offer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upBroker" runat="server">
        <ContentTemplate>
            <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
                <div class="uk-container ">
                    <div class="uk-position-relative">
                        <!-- start body container -->
                        <div class="BodyContainer">
                            <h1 class="uk-heading-line uk-text-left pageTitle">
                                <span>
                                    <asp:Literal ID="litTitle" runat="server" /></span>
                            </h1>
                            <div class="uk-container ">
                                <img id="imgOffer" runat="server" style="height: 450px;" src="#" alt="">
                                <hr />
                                <div class="detailsIconsContainer">

                                    <span class="uk-margin-left">
                                        <i class="fa fa-eye"></i>
                                        <span>
                                            <asp:Literal ID="litViewers" runat="server" /></span>
                                    </span>
                                    <span class="uk-margin-left">
                                        <i class="fa fa-link"></i>
                                        <span>
                                            <a id="aLink" runat="server" href="#"></a></span>
                                    </span>
                                    <span class="uk-margin-left">
                                        <i class="fa fa-whatsapp"></i>
                                        <span style="user-select:none;">
                                            <a id="aWhatsapp" runat="server" ></a></span>
                                    </span>
                                    <span class="uk-margin-left">
                                        <i class="fa fa-phone"></i>
                                        <span>
                                            <a id="aPhone" runat="server" href="#"></a></span>
                                    </span>
                                </div>
                                <h5>
                                    <b><%= GetGlobalResourceObject("Resource","details") %></b>
                                    <br />
                                    <div>
                                        <asp:Literal ID="litDesc" runat="server" />
                                    </div>
                                </h5>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
