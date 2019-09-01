<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Property.aspx.cs" Inherits="MapIt.Web.Property" %>

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
    <asp:UpdatePanel ID="upProperty" runat="server">
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
                            <div class="uk-container">
                                <div class="detailsIconsContainer">
                                    <a onclick="return flase;">
                                        <span class="uk-margin-left">
                                            <i class="fa fa-eye fa-2x" title=""></i>
                                            <span>
                                                <asp:Literal ID="litViewers" runat="server" /></span>
                                        </span>
                                    </a>
                                    <asp:LinkButton ID="lnkAddToFavorite" runat="server" OnClick="lnkAddToFavorite_Click">
                                        <span class="uk-margin-left">
                                            <i class="fa fa-heart fa-2x" title=""></i>
                                            <span>
                                                <asp:Literal ID="litFav" runat="server" /></span>
                                        </span>
                                    </asp:LinkButton>
                                    <a onclick="return flase;">
                                        <span class="uk-margin-left">
                                            <span class="icon-calendar2"></span>
                                            <span>
                                                <asp:Literal ID="litDuration" runat="server" /></span>
                                        </span>
                                    </a>
                                </div>
                                <div class="detailsIconsContainer uk-margin-top">
                                    <asp:LinkButton ID="lnkBtnReport" runat="server" OnClick="lnkBtnReport_Click" CssClass="uk-button buttonStyle">

                                        <span class="uk-margin-left">
                                            <span class="icon-report-symbol"></span>
                                            <span><%= GetGlobalResourceObject("Resource","report_abuse") %></span>
                                            (
                                            <asp:Literal ID="litReports" runat="server" />
                                            )
                                        </span>
                                    </asp:LinkButton>
                                    <a id="aOther" runat="server" href="#" class="uk-button buttonStyle uk-margin-right">
                                        <span class="uk-margin-left">
                                            <span class="icon-megaphone"></span>
                                            <span><%= GetGlobalResourceObject("Resource","other_pro_advr") %></span>
                                            (
                                            <asp:Literal ID="litOthers" runat="server" />
                                            )
                                        </span>
                                    </a>
                                </div>
                                <div class="uk-child-width-1-2@m" uk-grid>
                                    <div>
                                        <table class="uk-table uk-table-divider buildDetails">
                                            <tbody>
                                                <tr id="tr_SellingPrice" runat="server" visible="false">
                                                    <td>
                                                        <i class="fa fa-money fa-2x" title="<%= GetGlobalResourceObject("Resource","selling_price") %>"></i>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Literal ID="litSellingPrice" runat="server" /></span>
                                                        <%= GeneralSetting.DefaultCurrency.SymbolEN %></td>
                                                </tr>
                                                <tr id="tr_RentPrice" runat="server" visible="false">
                                                    <td>
                                                        <i class="fa fa-money fa-2x" title="<%= GetGlobalResourceObject("Resource","rent_price") %>"></i>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Literal ID="litRentPrice" runat="server" /></span>
                                                        <%= GeneralSetting.DefaultCurrency.SymbolEN %></td>
                                                </tr>
                                                <tr id="tr_Area" runat="server" visible="false">
                                                    <td>
                                                        <i class="fa fa-home fa-2x" title="<%= GetGlobalResourceObject("Resource","area_m2") %>"></i>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Literal ID="litArea" runat="server" /></span>
                                                        <%= GetGlobalResourceObject("Resource","m2") %>
                                                    </td>
                                                </tr>
                                                <tr id="tr_BuildingAge" runat="server" visible="false">
                                                    <td>
                                                        <i class="fa fa-building fa-2x" title="<%= GetGlobalResourceObject("Resource","building_age") %>"></i>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Literal ID="litBuildingAge" runat="server" /></span>
                                                        <%= GetGlobalResourceObject("Resource","years") %>
                                                    </td>
                                                </tr>
                                                <tr id="tr_MonthlyIncome" runat="server" visible="false">
                                                    <td>
                                                        <i class="fa fa-credit-card fa-2x" title="<%= GetGlobalResourceObject("Resource","monthly_income") %>"></i>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Literal ID="litMonthlyIncome" runat="server" /></span>
                                                        <%= GeneralSetting.DefaultCurrency.SymbolEN %></td>
                                                </tr>
                                                <tr id="tr_OtherPhones" runat="server" visible="false">
                                                    <td>
                                                        <i class="fa fa-address-book fa-2x" title="<%= GetGlobalResourceObject("Resource","other_phones") %>"></i>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Literal ID="litOtherPhones" runat="server" /></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <i class="fa fa-map-marker fa-2x" title="<%= GetGlobalResourceObject("Resource","address") %>"></i>
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litAddress" runat="server" />
                                                    </td>
                                                </tr>
                                                <asp:Repeater ID="rComponents" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <img src="<%# Eval("Component.FinalPhoto") %>" style="width: 32px;" alt="" title="<%# Eval("Component."+Resources.Resource.db_title_col) %>" />
                                                            </td>
                                                            <td>
                                                                <span><%# Eval("Count") %></span>
                                                                <%# Eval("Component." + Resources.Resource.db_title_col) %>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:Repeater ID="rFeatures" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <i class="fa fa-check fa-2x" title="<%# Eval("Feature." + Resources.Resource.db_title_col) %>"></i>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Feature." + Resources.Resource.db_title_col) %>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                            </tbody>
                                        </table>

                                        <h2><%= GetGlobalResourceObject("Resource","details") %>
                                        </h2>
                                        <div>
                                            <h5>
                                                <asp:Literal ID="litDetails" runat="server" />
                                            </h5>
                                        </div>
                                    </div>

                                    <div>
                                        <div class="uk-position-relative uk-visible-toggle uk-light" uk-slideshow>
                                            <ul class="uk-slideshow-items">
                                                <asp:Repeater ID="rPhotos" runat="server">
                                                    <ItemTemplate>
                                                        <li>
                                                            <img src='<%# MapIt.Lib.AppSettings.PropertyWMPhotos + Eval("Photo") %>' alt='<%#Eval("Property." + Resources.Resource.db_title_col) %>' uk-cover>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>

                                            <a class="uk-position-center-left uk-position-small uk-hidden-hover" href="#" uk-slidenav-previous uk-slideshow-item="previous"></a>
                                            <a class="uk-position-center-right uk-position-small uk-hidden-hover" href="#" uk-slidenav-next uk-slideshow-item="next"></a>

                                        </div>
                                    </div>
                                </div>
                                <div id="ui-map">
                                </div>
                                <%--<iframe id="iframeMap" runat="server" width="100%" height="450" frameborder="0" style="border: 0" allowfullscreen></iframe>--%>
                            </div>

                            <div id="div_related" runat="server" style="padding-top: 20px;">
                                <hr class="uk-divider-icon">
                                <div class="uk-child-width-1-4@m uk-grid uk-margin-top" uk-sortable="handle: .uk-sortable-handle" uk-grid>

                                    <div class="searchResultTitle uk-margin-top">
                                        <h3>
                                            <span class="icon-megaphone"></span>
                                            <%= GetGlobalResourceObject("Resource","related_properties") %>
                                        </h3>
                                    </div>

                                    <asp:Repeater ID="rRelatedPros" runat="server" OnItemCommand="rRelatedPros_ItemCommand">
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
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
