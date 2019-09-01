<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MapIt.Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%= GetGlobalResourceObject("Resource","web_title") %> | <%= GetGlobalResourceObject("Resource","home") %></title>
    <script>
        function setPurpose(value) {
            if (value == 1) {
                var purpose = document.getElementById("hfPurpose");
                purpose.value = value;
                document.getElementById("aRent").classList.remove("active");
                document.getElementById("aRent").classList.add("white");
                document.getElementById("aSale").classList.add("active");
                document.getElementById("aSale").classList.remove("white");
            }
            else {
                var purpose = document.getElementById("hfPurpose");
                purpose.value = value;
                document.getElementById("aSale").classList.remove("active");
                document.getElementById("aSale").classList.add("white");
                document.getElementById("aRent").classList.add("active");
                document.getElementById("aRent").classList.remove("white");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upDefault" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="uk-background-fixed uk-background-fixed uk-background-center-center  BG-Kuwite">
                <div class="searchSection textCenter" uk-parallax="bgy: -200">
                    <h1><%= GetGlobalResourceObject("Resource","your_dream_house") %></h1>
                    <div class="searchLinks">
                        <a id="aSale" class="active" onclick="setPurpose(1)"><%= GetGlobalResourceObject("Resource","sale") %></a>
                        <a id="aRent" class="white" onclick="setPurpose(2)"><%= GetGlobalResourceObject("Resource","rent") %></a>
                        <input id="hfPurpose" runat="server" clientidmode="Static" type="hidden" />
                    </div>

                    <div class="uk-form-horizontal uk-margin-large">
                        <div class="uk-column-1-2@s  uk-column-1-4@l">
                            <div class="uk-margin">
                                <div class="uk-form-controls">
                                    <asp:DropDownList ID="ddlSearchType" runat="server" CssClass="uk-select" AppendDataBoundItems="true">
                                        <asp:ListItem Text="<%$  Resources:Resource,all_types %>" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="uk-margin">
                                <div class="uk-form-controls">
                                    <asp:DropDownList ID="ddlSearchCity" runat="server" CssClass="uk-select" AppendDataBoundItems="true"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSearchCity_SelectedIndexChanged">
                                        <asp:ListItem Text="<%$  Resources:Resource,all_cities %>" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="uk-margin">
                                <div class="uk-form-controls">
                                    <asp:DropDownList ID="ddlSearchArea" runat="server" CssClass="uk-select" AppendDataBoundItems="true"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSearchArea_SelectedIndexChanged">
                                        <asp:ListItem Text="<%$  Resources:Resource,all_areas %>" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="uk-margin">
                                <div class="uk-form-controls">
                                    <asp:DropDownList ID="ddlSearchBlock" runat="server" CssClass="uk-select" AppendDataBoundItems="true">
                                        <asp:ListItem Text="<%$  Resources:Resource,all_blocks %>" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>


                    </div>
                    <div class="uk-search uk-search-default">
                        <asp:LinkButton ID="lnkSearch" runat="server" CssClass="uk-search-icon uk-icon" uk-search-icon="" OnClick="lnkSearch_Click">
                        <img src="/images/searcher.svg" class="SearchIcon">
                        </asp:LinkButton>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="uk-search-input" placeholder="<%$ Resources:Resource,search_dots %>" />
                    </div>
                </div>

            </div>

            <div class="searchAgent">
                <div class="uk-container ">
                    <%--<span class="icon-users"></span>--%>
                    <span><%= GetGlobalResourceObject("Resource","search_agants") %>
                    </span>
                    <a href="/Brokers" class=" uk-button  buttonStyle searchNow"><%= GetGlobalResourceObject("Resource","search_now") %></a>
                </div>
            </div>

            <div class="uk-container ">
                <div class="uk-column-1@s uk-column-1-3@m bodyLinksContainer">
                    <ul class="uk-list bodyLinks" <%--uk-parallax="opacity: 0,7"--%>>
                        <li><%= GetGlobalResourceObject("Resource","properties_types") %></li>
                        <asp:Repeater ID="rTypes" runat="server">
                            <ItemTemplate>
                                <li>
                                    <a href='<%# Eval("PageName", "../Typs/{0}") %>' class=" uk-button-text"><%# Eval(Resources.Resource.db_title_col) %>
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <li>
                            <a href="/Proprties" class=" uk-button  buttonStyle searchNow"><%= GetGlobalResourceObject("Resource","more") %></a>
                        </li>
                    </ul>
                    <ul class="uk-list bodyLinks" <%--uk-parallax="opacity: 0,5"--%>>
                        <li><%= GetGlobalResourceObject("Resource","cons_services") %></li>
                        <asp:Repeater ID="rCats" runat="server">
                            <ItemTemplate>
                                <li>
                                    <a href='<%# Eval("SubLink") %>' class=" uk-button-text"><%# Eval(Resources.Resource.db_title_col) %>
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <li>
                            <a href="/ServCat" class=" uk-button  buttonStyle searchNow"><%= GetGlobalResourceObject("Resource","more") %></a>
                        </li>
                    </ul>
                    <ul class="uk-list bodyLinks" <%-- uk-parallax="opacity: 0,3"--%>>
                        <li><%= GetGlobalResourceObject("Resource","other_links") %></li>
                        <li>
                            <a href="/AboutUs" class="uk-button-text">
                                <%= GetGlobalResourceObject("Resource","about_us") %></a>
                        </li>
                        <li>
                            <a href="/TermsCond" class=" uk-button-text">
                                <%= GetGlobalResourceObject("Resource","terms_cond") %></a>
                        </li>
                        <li>
                            <a href="/FAQs" class=" uk-button-text">
                                <%= GetGlobalResourceObject("Resource","faqs") %></a>
                        </li>
                        <li>
                            <a href="/TechSupport" class=" uk-button-text">
                                <%= GetGlobalResourceObject("Resource","tech_support") %></a>
                        </li>
                        <li>
                            <a href="/Offers" class="uk-button-text">
                                <%= GetGlobalResourceObject("Resource","offers") %></a>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="uk-background-fixed uk-background-fixed uk-background-center-center  BG-KuwiteNight">
                <div class="uk-column-1@s uk-column-1-2@m">
                    <a href="/ManageProperty">
                        <img src="<%= GetGlobalResourceObject("Resource","add_house_image") %>" class="uk-align-center" <%--uk-parallax="opacity: 0,1,1; y: -100,0,0; x: 100,100,0; scale: 2,1,1; viewport: 0.5;"--%>>
                    </a>
                    <a href="/ManageService">
                        <img src="<%= GetGlobalResourceObject("Resource","add_service_image") %>" class="uk-align-center" <%--uk-parallax="opacity: 0,1,1; y: -100,0,0; x: 100,100,0; scale: 2,1,1; viewport: 0.5;"--%>>
                    </a>
                </div>
                <div class="downloadApp uk-align-center">
                    <h5><%= GetGlobalResourceObject("Resource","our_app") %></h5>
                    <h6><%= GetGlobalResourceObject("Resource","google_and_app") %></h6>
                    <a id="aGooglePlay" runat="server" href="#" target="_blank">
                        <img src="/images/AndroidApp.svg" <%--uk-parallax="opacity: 0,1,1; y: 100,0,0; x: 100,100,0; scale: 2,1,1; viewport: 0.5;"--%>>
                    </a>
                    <a id="aAppStore" runat="server" href="#" target="_blank">
                        <img src="/images/Available_on_the_App_Store_(black)_SVG.svg" <%--uk-parallax="opacity: 0,1,1; y: -100,0,0; x: 100,100,0; scale: 2,1,1; viewport: 0.5;"--%>>
                    </a>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

