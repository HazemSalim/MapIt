<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proprties.aspx.cs" Inherits="MapIt.Web.Proprties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        html, body, #map {
            height: 100%;
            width: 100%;
            margin: 0;
            padding: 0;
        }

        .dojoxColorPicker {
            position: absolute;
            top: 15px;
            right: 15px;
            -moz-box-shadow: 0 0 7px #888;
            -webkit-box-shadow: 0 0 7px #888;
            box-shadow: 0 0 7px #888;
        }
    </style>
    <link rel="stylesheet" href="https://js.arcgis.com/3.20/esri/css/esri.css" />
    <link rel="stylesheet" href="https://js.arcgis.com/3.24/dijit/themes/claro/claro.css" />
    <link rel="stylesheet" href="https://js.arcgis.com/3.24/dojox/widget/ColorPicker/ColorPicker.css" />
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <script src="https://js.arcgis.com/3.20/"></script>
    <script src="https://js.arcgis.com/3.24/"></script>
    <script>
        var dojoConfig = { isDebug: true };
        $(document).ready(function () {
            var count =<%= LoadCount %>;
            if(count==1){
                showMap();}
        });
        function showMap() {
            require(["esri/map", "esri/layers/ArcGISTiledMapServiceLayer", "esri/geometry/Point", "esri/SpatialReference",
                "esri/symbols/SimpleMarkerSymbol", "esri/graphic",
                "dojo/_base/array", "dojo/dom-style", "dojox/widget/ColorPicker", "esri/symbols/PictureMarkerSymbol", "dojo/domReady!"], function (Map, ArcGISTiledMapServiceLayer, Point, SpatialReference,
                    SimpleMarkerSymbol, Graphic,
                    arrayUtils, domStyle, ColorPicker, PictureMarkerSymbol) {

                    var initialExtent = new esri.geometry.Extent
                        ({ "xmin": 5190124.0769009115, "ymin": 3335306.073392077, "xmax": 5437155.058767752, "ymax": 3495933.660671074, "spatialReference": { "wkid": 102100 } });
                    var paciMap = new Map("ui-map", {
                        extent: initialExtent,
                        center: [47.30765259374954, 29.69781119600917],
                        zoom: 10,
                        minZoom: 9,
                        maxZoom: 17
                    });

                    var BaseMap = new esri.layers.ArcGISTiledMapServiceLayer("https://kuwaitportal.paci.gov.kw/arcgisportal/rest/services/Hosted/ArabicMap/MapServer/")
                    paciMap.addLayer(BaseMap);

                    var symbol = new PictureMarkerSymbol({
                        "url": "Images/marker.png",
                        "height": 20,
                        "width": 20,
                        "type": "esriPMS",
                        "angle": -30,
                    });

                    var cordnites = document.getElementById("ContentPlaceHolder1_hdLocation");
                    paciMap.on("load", function () {
                        if (cordnites.value != "") {
                            var cordinatesArray = cordnites.value.split("|");
                            for (var i = 0; i < cordinatesArray.length; i++) {
                                if (cordinatesArray[i] != "") {
                                    var longitude = cordinatesArray[i].split(",")[0];
                                    var latitude = cordinatesArray[i].split(",")[1];
                                    var points = new Point(longitude, latitude);
                                    paciMap.graphics.add(new Graphic(points, symbol));
                                }
                            }
                        }
                    });
                });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upTypes" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdLocation" runat="server" />
            <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
                <div class="navBG">
                    <div class="uk-container  filterSearchNav">
                        <div class="uk-position-relative">
                            <nav class="uk-navbar-container uk-margin">
                                <div class="nav-overlay uk-navbar-right">
                                    <nav class="uk-navbar-container fiterNavMargin" uk-navbar>
                                        <div class="uk-navbar-left">
                                            <ul class="uk-navbar-nav navFilter navbar-nav-padding">
                                                <li>
                                                    <a href="">
                                                        <asp:Literal ID="litPurpose" runat="server" Text="<%$ Resources:Resource,all_purposes %>" />
                                                    </a>
                                                    <div class="uk-navbar-dropdown">
                                                        <ul class="uk-nav uk-navbar-dropdown-nav" style="min-width: 150px;">
                                                            <asp:Repeater ID="rPurpose" runat="server" OnItemCommand="rPurpose_ItemCommand">
                                                                <HeaderTemplate>
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkPurpose" CommandArgument="" CommandName="Purpose" runat="server"><%= GetGlobalResourceObject("Resource","all_purposes") %></asp:LinkButton>
                                                                    </li>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkPurpose" CommandArgument='<%# Eval("Id") %>' CommandName="Purpose" runat="server"><%# Eval(Resources.Resource.db_title_col) %></asp:LinkButton>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ul>
                                                    </div>
                                                </li>
                                                <li>
                                                    <a href="">
                                                        <asp:Literal ID="litType" runat="server" Text="<%$ Resources:Resource,all_types %>" />
                                                    </a>
                                                    <div class="uk-navbar-dropdown">
                                                        <ul class="uk-nav uk-navbar-dropdown-nav">
                                                            <asp:Repeater ID="rTypes" runat="server" OnItemCommand="rTypes_ItemCommand">
                                                                <HeaderTemplate>
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkTypes" CommandArgument="" CommandName="Types" runat="server"><%= GetGlobalResourceObject("Resource","all_types") %></asp:LinkButton>
                                                                    </li>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkTypes" CommandArgument='<%# Eval("Id") %>' CommandName="Types" runat="server"><%# Eval(Resources.Resource.db_title_col) %></asp:LinkButton>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ul>
                                                    </div>
                                                </li>
                                                <li>
                                                    <a href="#"><%= GetGlobalResourceObject("Resource","filter_by_price").ToString() %>
                                                    </a>
                                                    <div class="uk-navbar-dropdown">
                                                        <ul class="uk-nav uk-navbar-dropdown-nav">
                                                            <div class="uk-margin">
                                                                <div class="uk-form-controls">
                                                                    <label class="uk-form-label" for="form-horizontal-select"><%= GetGlobalResourceObject("Resource","purpose").ToString() %></label>
                                                                    <asp:DropDownList ID="ddlPurpose" runat="server" CssClass="uk-select"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="uk-margin">
                                                                <div class="uk-grid-small" uk-grid>
                                                                    <div class="uk-width-1-2@s">
                                                                        <asp:TextBox ID="txtMin" runat="server" class="uk-input" placeholder='<%$Resources:Resource, lowest_price%>'></asp:TextBox>
                                                                        <ul>
                                                                        </ul>
                                                                    </div>
                                                                    <div class="uk-width-1-2@s">
                                                                        <asp:TextBox ID="txtMax" runat="server" class="uk-input" placeholder='<%$Resources:Resource, highest_price%>'></asp:TextBox>
                                                                        <ul>
                                                                    </div>
                                                                    <div>
                                                                        <asp:Button ID="btnSearchByPrice" CssClass="uk-button buttonStyle" runat="server" Text='<%$ Resources:Resource, search %>' OnClick="btnSearchByPrice_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ul>
                                                    </div>
                                                </li>
                                                <li>
                                                    <div class="uk-navbar-right">
                                                        <div>
                                                            <a class="uk-navbar-toggle" href="#" uk-search-icon>
                                                                <span><%= GetGlobalResourceObject("Resource","search").ToString() %>
                                                                </span>
                                                            </a>
                                                            <div class="uk-navbar-dropdown searhNavFilter" uk-drop="mode: click; cls-drop: uk-navbar-dropdown; boundary: !nav">

                                                                <div class="uk-grid-small uk-flex-middle" uk-grid>
                                                                    <div class="uk-width-expand">
                                                                        <asp:Panel ID="pnlSearchKeyword" runat="server" class="uk-search uk-search-navbar uk-width-1-1" DefaultButton="btnSearchKeyword">
                                                                            <asp:TextBox ID="txtSearchKeyword" class="uk-search-input" runat="server" placeholder="<%$ Resources:Resource,address %>"></asp:TextBox>
                                                                            <asp:Button ID="btnSearchKeyword" runat="server" Text="Price" OnClick="btnSearchKeyword_Click" Style="display: none; visibility: hidden" />
                                                                        </asp:Panel>
                                                                    </div>
                                                                    <div class="uk-width-auto">
                                                                        <a class="uk-navbar-dropdown-close" href="#" uk-close></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </nav>
                                </div>
                            </nav>
                        </div>
                    </div>
                </div>
                <div class="uk-container ">
                    <div class="uk-position-relative">
                        <!-- start body container -->
                        <div class="BodyContainer">
                            <h1 class="uk-heading-line uk-text-left pageTitle">
                                <span>
                                    <asp:Literal ID="litTitle" runat="server" />
                                </span>
                            </h1>
                            <div class="uk-child-width-1-2@m uk-grid">
                                <%--    <div data-uk-sticky="" class="uk-sticky uk-active uk-sticky-fixed" style="position: fixed; top: 0px; width: 611px;">--%>
                                <div id="ui-map"></div>
                                <%--     </div>--%>
                                <div class="uk-child-width-1-2@m uk-grid" uk-sortable="handle: .uk-sortable-handle" uk-grid>
                                    <asp:Repeater ID="rPros" runat="server" OnItemCommand="rPros_ItemCommand">
                                        <ItemTemplate>
                                            <!-- ***************** start card **************** -->
                                            <div class=" uk-sortable-handle " uk-icon="icon: table">
                                                <div class="uk-card uk-card-default">
                                                    <div class="uk-card-media-top">
                                                        <div class="uk-position-relative uk-visible-toggle uk-light" uk-slideshow="animation: fade">
                                                            <ul class="uk-slideshow-items">
                                                                <li>
                                                                    <img src="<%# Eval("PropertyPhoto") %>" style="height: 180px;" alt='<%# Eval(Resources.Resource.db_title_col) %>' uk-cover>
                                                                </li>
                                                            </ul>
                                                            <a class="uk-position-center-left uk-position-small uk-hidden-hover" href="#" uk-slidenav-previous uk-slideshow-item="previous"></a>
                                                            <a class="uk-position-center-right uk-position-small uk-hidden-hover" href="#" uk-slidenav-next uk-slideshow-item="next"></a>
                                                        </div>
                                                    </div>
                                                    <div class="">
                                                        <div class="uk-position-top-right  sizeSpan" style="color: #fff;">
                                                            <%# Eval("Area") %> <%= GetGlobalResourceObject("Resource","m2") %>
                                                        </div>
                                                        <div class="uk-position-bottom-left  sizeSpan" style="color: #fff;">
                                                            <a style="color: #fff;" href="<%# Eval("PageName", "../Pro/{0}") %>" uk-toggle><%# Eval(Resources.Resource.db_title_col) %>
                                                            </a>

                                                            <p style="margin: 0;" style="color: #fff;">
                                                                <%# Eval(Resources.Resource.db_address_col) %>
                                                            </p>
                                                            <span class="icon-calendar2"><%# Culture.ToLower() == "ar-kw" ? MapIt.Helpers.PresentHelper.GetDurationAr((DateTime)Eval("AddedOn"))
                                                                    : MapIt.Helpers.PresentHelper.GetDurationEn((DateTime)Eval("AddedOn")) %></span>
                                                            <p>
                                                            </p>
                                                            <div>
                                                                <asp:Repeater ID="rComponents" runat="server">
                                                                    <ItemTemplate>
                                                                        <span>
                                                                            <img src="<%# Eval("Component.FinalPhoto") %>" style="width: 32px;" alt="" title="<%# Eval("Component."+Resources.Resource.db_title_col) %>" />
                                                                            <%# Eval("Count") %>
                                                                        </span>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                        <div class="uk-position-bottom-right  sizeSpan" style="padding: 5px;">
                                                            <asp:HiddenField ID="hfProId" Value='<%# Eval("Id") %>' runat="server" />
                                                            <asp:HiddenField ID="hdCoordinates" Value='<%# Eval("Cordinates") %>' runat="server" />
                                                            <asp:LinkButton ID="lnkFav" CommandName="Fav" CommandArgument='<%# Eval("Id") %>' runat="server">
                                                                <%# new MapIt.Repository.PropertiesRepository()
                                                                        .IsFavourite(MapIt.Helpers.ParseHelper.GetInt64(Eval("Id")).Value,  this.UserId)? 
                                                                        "<i class='fa fa-heart fa-2x'></i>":"<i class='fa fa-heart-o fa-2x'></i>"%>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- ***************** end card **************** -->
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <div class="uk-text-left sizeSpan">
                                        <%--  <asp:Button ID="btnLoadMore" runat="server" CssClass="uk-button uk-button-primary" Text='<%$ Resources:Resource,load_more %>'
                                            OnClick="btnLoadMore_Click" />--%>
                                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" OnPageChanged="AspNetPager1_PageChanged"
                                            PageSize="15" CssClass="pages" CurrentPageButtonClass="cpb" NextPageText="التالي"
                                            PrevPageText="السابق" Wrap="true" Direction="RightToLeft" ShowFirstLast="False">
                                        </webdiyer:AspNetPager>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearchKeyword" />
            <asp:PostBackTrigger ControlID="btnSearchByPrice" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
