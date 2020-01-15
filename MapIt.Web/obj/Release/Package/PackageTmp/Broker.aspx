<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Broker.aspx.cs" Inherits="MapIt.Web.Broker" %>

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
                                    <asp:Literal ID="litFullname" runat="server" /></span>
                            </h1>
                            <div class="uk-container ">
                                <img id="imgPhoto" runat="server" style="height: 450px;" src="#">
                                <hr />
                                <div class="detailsIconsContainer">

                                    <span class="uk-margin-left">
                                        <i class="fa fa-phone"></i>
                                        <span style="user-select:none;">
                                            <a id="aPhone" runat="server" ></a></span>
                                    </span>
                                    <span class="uk-margin-left">
                                        <i class="fa fa-envelope"></i>
                                        <span>
                                            <a id="aEmail" runat="server" href="#"></a></span>
                                    </span>
                                </div>
                                <h5>
                                    <b><%= GetGlobalResourceObject("Resource","details") %></b>
                                    <br />
                                    <div>
                                        <asp:Literal ID="litDetails" runat="server" />
                                    </div>
                                </h5>
                                <h5>
                                    <b><%= GetGlobalResourceObject("Resource","op_areas") %></b>
                                    <br />
                                    <div style="margin-top: 15px;">
                                        <asp:Repeater ID="rAreas" runat="server">
                                            <ItemTemplate>
                                                <span style="border-radius: 20px; background: #003366; color: #fff; padding: 10px; margin: 5px;"><%# Eval("Area." + Resources.Resource.db_title_col) %> </span>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <span id="span_allareas" runat="server" visible="false" style="border-radius: 20px; background: #003366; color: #fff; padding: 10px; margin: 5px;"><%= GetGlobalResourceObject("Resource","all_areas") %> </span>
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
