<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Service.aspx.cs" Inherits="MapIt.Web.Service" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://js.arcgis.com/3.20/esri/css/esri.css" />
    <link rel="stylesheet" href="https://js.arcgis.com/3.24/dijit/themes/claro/claro.css" />
    <link rel="stylesheet" href="https://js.arcgis.com/3.24/dojox/widget/ColorPicker/ColorPicker.css" />
    <script src="https://js.arcgis.com/3.20/"></script>
    <script src="https://js.arcgis.com/3.24/"></script>
    <script src="/Scripts/jquery-1.7.1.min.js"></script>
    <script>
        $(document).ready(function () {
            require(["esri/map", "esri/layers/ArcGISTiledMapServiceLayer", "esri/geometry/Point", "esri/SpatialReference",
                "esri/symbols/SimpleMarkerSymbol", "esri/graphic",
                "dojo/_base/array", "dojo/dom-style", "dojox/widget/ColorPicker", "esri/symbols/PictureMarkerSymbol", "dojo/domReady!"], function (Map, ArcGISTiledMapServiceLayer, Point, SpatialReference,
                    SimpleMarkerSymbol, Graphic,
                    arrayUtils, domStyle, ColorPicker, PictureMarkerSymbol) {
                    var cordnites = document.getElementById("ContentPlaceHolder1_hdLocation").value.split(",");
                    var longitude = cordnites[0];
                    var latitude = cordnites[1];

                    var initialExtent = new esri.geometry.Extent
                        ({ "xmin": 5190124.0769009115, "ymin": 3335306.073392077, "xmax": 5437155.058767752, "ymax": 3495933.660671074, "spatialReference": { "wkid": 102100 } });
                    var paciMap = new Map("ui-map", {
                        extent: initialExtent,
                        center: [longitude, latitude],
                        zoom: 15,
                        minZoom: 9,
                        maxZoom: 17
                    });

                    var BaseMap = new esri.layers.ArcGISTiledMapServiceLayer("https://kuwaitportal.paci.gov.kw/arcgisportal/rest/services/Hosted/ArabicMap/MapServer/")
                    paciMap.addLayer(BaseMap);

                    var symbol = new PictureMarkerSymbol({
                        "url": "/Images/marker.png",
                        "height": 20,
                        "width": 20,
                        "type": "esriPMS",
                        "angle": -30,
                    });

                    paciMap.on("load", function () {
                        if (longitude != "" && latitude != "") {
                            var points = new Point(longitude, latitude);
                            paciMap.graphics.add(new Graphic(points, symbol));
                        }
                    });
                });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upService" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdLocation" runat="server" />
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
                                <div class="uk-card-media-top">
                                    <div class="uk-position-relative uk-visible-toggle uk-light" uk-slideshow="animation: fade">
                                        <ul class="uk-slideshow-items">
                                            <asp:Repeater ID="rPhotos" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <img src='<%# MapIt.Lib.AppSettings.ServiceWMPhotos + Eval("Photo") %>' style="height: 450px;" alt='<%#Eval("Service.Title") %>' uk-cover>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>

                                        <a class="uk-position-center-left uk-position-small uk-hidden-hover" href="#" uk-slidenav-previous uk-slideshow-item="previous"></a>
                                        <a class="uk-position-center-right uk-position-small uk-hidden-hover" href="#" uk-slidenav-next uk-slideshow-item="next"></a>
                                    </div>

                                </div>
                                <hr />
                                <div class="detailsIconsContainer">

                                    <span class="uk-margin-left">
                                        <span class="icon-phone"></span>
                                        <span>
                                            <a id="aPhone" runat="server" href="#">
                                                <asp:Literal ID="litPhone" runat="server" /></a></span>
                                    </span>

                                    <asp:LinkButton ID="lnkAddToFavorite" runat="server" OnClick="lnkAddToFavorite_Click">
                                        <span class="uk-margin-left">
                                            <span class="icon-favorite-heart-button"></span>
                                            <span>
                                                <asp:Literal ID="litFav" runat="server" /></span>
                                        </span>
                                    </asp:LinkButton>
                                    <span>|</span>
                                    <a href="">
                                        <span class="uk-margin-left inlinBlock">
                                            <span>
                                                <asp:Literal ID="litYears" runat="server" />
                                            </span>
                                            <span><%= GetGlobalResourceObject("Resource","years_ex") %></span>
                                        </span>
                                    </a>
                                    <a href="">
                                        <span class="uk-margin-left">
                                            <span class="icon-dark-eye"></span>
                                            <span>
                                                <asp:Literal ID="litViewers" runat="server" /></span>
                                        </span>
                                    </a>
                                </div>
                                <div class="detailsIconsContainer uk-margin-top">
                                    <asp:LinkButton ID="lnkBtnReport" runat="server" CssClass="uk-button buttonStyle">

                                        <span class="uk-margin-left">
                                            <span class="icon-report-symbol"></span>
                                            <span><%= GetGlobalResourceObject("Resource","report_abuse") %></span>
                                            (
                                            <asp:Literal ID="litReports" runat="server" />
                                            )
                                        </span>
                                    </asp:LinkButton>
                                    <a id="aOther" href="#" class="uk-button buttonStyle uk-margin-right">
                                        <span class="uk-margin-left">
                                            <span class="icon-megaphone"></span>
                                            <span><%= GetGlobalResourceObject("Resource","other_srv_advr") %></span>
                                            (
                                            <asp:Literal ID="litOthers" runat="server" />
                                            )
                                        </span>
                                    </a>
                                </div>
                                <h5>
                                    <b><%= GetGlobalResourceObject("Resource","details") %></b>
                                    <br />
                                    <div>
                                        <asp:Literal ID="litDesc" runat="server" />
                                    </div>
                                </h5>

                            </div>
                            <div class="uk-margin-small">
                                <div id="ui-map"></div>
                            </div>

                            <div id="div_related" runat="server">
                                <hr class="uk-divider-icon">
                                <div class="uk-child-width-1-4@m uk-grid uk-margin-top" uk-sortable="handle: .uk-sortable-handle" uk-grid>

                                    <div class="searchResultTitle uk-margin-top">
                                        <h3>
                                            <span class="icon-megaphone"></span>
                                            <%= GetGlobalResourceObject("Resource","related_services") %>
                                        </h3>
                                    </div>

                                    <asp:Repeater ID="rRelatedSrvs" runat="server" OnItemCommand="rRelatedSrvs_ItemCommand">
                                        <ItemTemplate>
                                            <div class="uk-text-center">
                                                <div class="uk-inline-clip uk-transition-toggle">
                                                    <div class=" uk-position-bottom-left ">
                                                        <asp:HiddenField ID="proId" runat="server" Value='<%# Eval("Id") %>' />
                                                        <asp:LinkButton ID="lnkDeleteFav" CommandName="DeleteFav" CommandArgument='<%# Eval("Id") %>' runat="server">
                                                                <span class="icon-favorite-heart-button"></span>
                                                        </asp:LinkButton>
                                                    </div>

                                                    <a href='<%# Eval("PageName", "../Srv/{0}") %>' title='<%# Eval("Title") %>'>
                                                        <img class="uk-transition-scale-up uk-transition-opaque" src='<%# Eval("ServicePhoto") %>' alt='<%# Eval("Title") %>'>
                                                        <div class="uk-transition-fade uk-position-cover uk-position-small uk-overlay uk-overlay-default uk-flex uk-flex-center uk-flex-middle">
                                                            <div class="uk-h4 uk-margin-remove">
                                                                <div>
                                                                    <span class="icon-star-full"></span>
                                                                    <span class="icon-star-full"></span>
                                                                    <span class="icon-star-full blackStar"></span>
                                                                    <span class="icon-star-full blackStar"></span>
                                                                </div>
                                                                <div>
                                                                    <%# Eval("ExYears") %> <%= GetGlobalResourceObject("Resource","years_ex") %>
                                                                </div>
                                                                <h5>
                                                                    <%# MapIt.Helpers.PresentHelper.StringLimit(Eval("Description"), 25) %>
                                                                </h5>
                                                            </div>
                                                        </div>
                                                    </a>
                                                </div>
                                                <p class="uk-margin-small-top"><%# Eval("Title") %></p>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
