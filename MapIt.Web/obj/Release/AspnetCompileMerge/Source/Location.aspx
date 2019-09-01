<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Location.aspx.cs" Inherits="MapIt.Web.Location" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="http://maps.googleapis.com/maps/api/js?key=AIzaSyD3wRValWNTNYTUerUltwMlpKzcf0VKtfU&sensor=false"></script>
    <script type="text/javascript">
        function isNumber(n) { return !isNaN(n); }
        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
            return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        var map;
        var markersArray = [];
        var longitude;
        var latitude;
        var zoom = 8;
        var marker = getParameterByName('marker');
        var latId = getParameterByName('latId');
        var lngId = getParameterByName('lngId');

        function initMap() {

            var latlng;

            //editable
            if (marker == '1') {

                if (latId == null || latId == '')
                    latId = 'hfLatitude';

                if (lngId == null || lngId == '')
                    lngId = 'hfLongitude';

                latitude = parent.document.getElementById(latId);
                longitude = parent.document.getElementById(lngId)

                if (longitude.value == '' || !isNumber(longitude.value) || latitude.value == '' || !isNumber(latitude.value)) {
                    latitude.value = '29.448389';
                    longitude.value = '47.544431';
                    zoom = 5;
                }

                latlng = new google.maps.LatLng(latitude.value, longitude.value);
            }
            else {
                longitude = getParameterByName('lng');
                latitude = getParameterByName('lat');

                latlng = new google.maps.LatLng(latitude, longitude);
            }

            var myOptions = { zoom: zoom, center: latlng, mapTypeId: google.maps.MapTypeId.ROADMAP };
            map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
            placeMarker(latlng);

            // add a click event handler to the map object
            if (marker == '1') {
                google.maps.event.addListener(map, "click", function (event) {
                    // place a marker
                    placeMarker(event.latLng);

                    // display the lat/lng in your form's lat/lng fields
                    longitude.value = event.latLng.lng();
                    latitude.value = event.latLng.lat();
                });
            }
        }

        function placeMarker(location) {

            // first remove all markers if there are any
            deleteOverlays();

            var marker = new google.maps.Marker({
                position: location,
                map: map
            });

            // add marker in markers array
            markersArray.push(marker);
        }

        // Deletes all markers in the array by removing references to them
        function deleteOverlays() {
            if (markersArray) {
                for (i in markersArray) {
                    markersArray[i].setMap(null);
                }
                markersArray.length = 0;
            }
        }

    </script>
</head>
<body onload="initMap();">
    <form id="form1" runat="server">
        <div id="map_canvas" style="margin: auto; width: 100%; height: 480px;">
        </div>
    </form>
</body>
</html>
