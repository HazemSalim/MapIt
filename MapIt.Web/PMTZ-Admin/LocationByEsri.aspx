<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocationByEsri.aspx.cs" Inherits="MapIt.Web.Admin.LocationByEsri" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1, user-scalable=no" />
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
    <script src="https://js.arcgis.com/3.20/"></script>
    <script src="https://js.arcgis.com/3.24/"></script>
    <script>
        function isNumber(n) { return !isNaN(n); }
        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
            return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        var longitude;
        var latitude;
        var marker = getParameterByName('marker');
        var latId = getParameterByName('hfLatitude');
        var lngId = getParameterByName('hfLongitude');

        require(["esri/map", "esri/layers/ArcGISTiledMapServiceLayer", "esri/geometry/Point", "esri/SpatialReference",
          "esri/symbols/SimpleMarkerSymbol", "esri/graphic",
          "dojo/_base/array", "dojo/dom-style", "dojox/widget/ColorPicker", "esri/symbols/PictureMarkerSymbol", "dojo/domReady!"], function (Map, ArcGISTiledMapServiceLayer, Point, SpatialReference,
          SimpleMarkerSymbol, Graphic,
          arrayUtils, domStyle, ColorPicker, PictureMarkerSymbol) {

              var initialExtent = new esri.geometry.Extent
                            ({ "xmin": 5190124.0769009115, "ymin": 3335306.073392077, "xmax": 5437155.058767752, "ymax": 3495933.660671074, "spatialReference": { "wkid": 102100 } });
              var paciMap = new Map("ui-map", {
                  extent: initialExtent,
                  center: [47.99275025, 29.38837345], // long, lat
                  minZoom: 9,
                  maxZoom: 17
              });

              var symbol = new PictureMarkerSymbol({
                  "url": "Images/marker.png",
                  "height": 20,
                  "width": 20,
                  "type": "esriPMS",
                  "angle": -30,
              });

              var BaseMap = new esri.layers.ArcGISTiledMapServiceLayer("https://kuwaitportal.paci.gov.kw/arcgisportal/rest/services/Hosted/ArabicMap/MapServer/")
              paciMap.addLayer(BaseMap);

              if (latId == null || latId == '')
                  latId = 'hfLatitude';

              if (lngId == null || lngId == '')
                  lngId = 'hfLongitude';

              latitude = parent.document.getElementById(latId);
              longitude = parent.document.getElementById(lngId)

              if (marker == '1') {
                  paciMap.on("load", function () {
                      if (latitude.value != "" && longitude.value != "") {
                          var points = new Point(longitude.value, latitude.value);
                          paciMap.graphics.add(new Graphic(points, symbol));
                          paciMap.infoWindow.setContent("Longitude : " + points.getLongitude().toString() + " <br>Latitude : " + points.getLatitude().toString());
                          paciMap.infoWindow.show(points)
                      }
                  });
              }

              paciMap.on("click", function (evt) {
                  if (latId == null || latId == '')
                      latId = 'hfLatitude';

                  if (lngId == null || lngId == '')
                      lngId = 'hfLongitude';

                  latitude = parent.document.getElementById(latId);
                  longitude = parent.document.getElementById(lngId)


                  paciMap.graphics.clear();
                  paciMap.graphics.add(new Graphic(evt.mapPoint, symbol));
                  paciMap.infoWindow.setContent("Longitude : " + evt.mapPoint.getLongitude().toString() + " <br>Latitude : " + evt.mapPoint.getLatitude().toString());
                  paciMap.infoWindow.show(evt.mapPoint)
                  if (latitude != null && longitude != null) {
                      longitude.value = evt.mapPoint.getLongitude();
                      latitude.value = evt.mapPoint.getLatitude();
                  }
              });

          });
    </script>




</head>
<body>
    <form id="form1" runat="server">
        <div id="ui-map" style="height: 503px;"></div>
    </form>
</body>
</html>
