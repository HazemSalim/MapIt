<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="true" CodeBehind="Areas.aspx.cs" Inherits="MapIt.Web.Admin.Areas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyBtVA-GpqW6yHEbyyQOEnaLH0smZ97H9FA&v=3.exp&libraries=drawing&sensor=true"></script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            initialize();
            hdnCoordinates = $('#hdnCoordinates');
            hdnTempCoordinates = $('#hdnTempCoordinates');
            if (hdnCoordinates.val() != '') {
                var points = hdnCoordinates.val().split('#');
                var vertices = new Array();
                for (var x = 0; x < points.length; x++) {
                    vertices.push(new google.maps.LatLng(points[x].split(',')[0], points[x].split(',')[1]));
                }
                oldPolygon = new google.maps.Polygon({
                    paths: vertices,
                    strokeColor: '#FF0000',
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    fillColor: '#FF0000',
                    fillOpacity: 0.35
                });
                map.setCenter(new google.maps.LatLng(vertices[0].lat(), vertices[0].lng()))
                oldPolygon.setMap(map);
            }
        }

        var hdnCoordinates, hdnTempCoordinates;
        var map, drawingManager, oldPolygon, tempPolygon;
        var overlays = [];
        function initialize() {
            var mapOptions = {
                zoom: 13,
                center: new google.maps.LatLng(29.361953, 47.979663)
            };

            map = new google.maps.Map(document.getElementById('map-canvas'),
                mapOptions);
            drawingManager = new google.maps.drawing.DrawingManager({
                drawingMode: google.maps.drawing.OverlayType.POLYGON,
                drawingControl: true,
                drawingControlOptions: {
                    position: google.maps.ControlPosition.TOP_LEFT,
                    drawingModes: [
                      google.maps.drawing.OverlayType.MARKER,
                      google.maps.drawing.OverlayType.POLYGON
                    ]
                },
                polygonOptions: {
                    fillColor: '#f00',
                    fillOpacity: 0.3,
                    strokeColor: '#f00',
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    clickable: false,
                    zIndex: 1,
                    editable: true
                }
            });

            drawingManager.setMap(map);

            google.maps.event.addListener(drawingManager, 'polygoncomplete', function (ploygon) {
                tempPolygon = ploygon;
            });

            google.maps.event.addListener(drawingManager, 'overlaycomplete', function (event) {
                overlays.push(event.overlay); // store reference to added overlay
            });
        }
        function save() {
            var vertices = tempPolygon.getPath();
            hdnTempCoordinates.val('');
            for (var x = 0; x < vertices.length; x++) {
                if (x > 0) {
                    hdnTempCoordinates.val(hdnTempCoordinates.val() + '#');
                }
                hdnTempCoordinates.val(hdnTempCoordinates.val() + vertices.getAt(x).lat() + ',' + vertices.getAt(x).lng());
            }
            if (hdnCoordinates.val() != '') {
                if (confirm('This will overwrite old boundries. Are you sure?')) {
                    hdnCoordinates.val(hdnTempCoordinates.val());
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                hdnCoordinates.val(hdnTempCoordinates.val());
                return true;
            }
        }
        function removeLine() {
            var lastOverlay = overlays.pop();
            if (lastOverlay) lastOverlay.setMap(null);
        }
        function clear() {
            hdnTempCoordinates.val('');
            tempPolygon.setMap(null);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hdnCoordinates" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="hdnTempCoordinates" ClientIDMode="Static" />
            <div class="row">
                <ol class="breadcrumb">
                    <li><a href=".">
                        <i class="fa fa-home"></i>
                    </a></li>
                    <li><a href="Countries">Countries
                    </a></li>
                    <li><a href="Cities?id=<%:new MapIt.Repository.CitiesRepository().GetByKey(this.CityId).CountryId %>">Cities
                    </a></li>
                    <li class="active">Areas</li>
                </ol>
            </div>
            <!--/.row-->

            <div class="row" style="margin-top: 10px;">
                <div class="col-xs-6">
                    <input type="button" value="<" class="btn btn-info" onclick="goBack();">
                    <input type="button" value=">" class="btn btn-info" onclick="goForward();">
                </div>
                <div class="col-xs-6">
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <h4 class="page-header">
                        <asp:Literal ID="litTitle" runat="server" />
                        Areas</h4>
                </div>
            </div>
            <!--/.row-->

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Area Details</div>
                        <div class="panel-body">
                            <div class="col-md-12">

                                <div class="form-group">
                                    <label>
                                        Title (EN)
                                    </label>
                                    <asp:TextBox ID="txtTitleEN" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvTitleEN" runat="server" ControlToValidate="txtTitleEN" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label>
                                        Title (AR)
                                    </label>
                                    <asp:TextBox ID="txtTitleAR" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvTitleAR" runat="server" ControlToValidate="txtTitleAR" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group" style="height: 400px; padding-left: 10px;">
                                    <div id="map-canvas" style="width: 98%; height: 100%">
                                    </div>
                                </div>

                                <input type="button" name="clear" value="Clear" class="btn btn-info" onclick="return removeLine();" />
                                <asp:Button ID="btnSave" runat="server" Text="Add new Area" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                    OnClientClick="return save();" ValidationGroup="S" CausesValidation="true" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false"
                                    OnClick="btnCancel_Click" Visible="false" />
                            </div>

                        </div>
                    </div>
                </div>
                <!-- /.col-->
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Areas List</div>
                        <div class="panel-body">
                            <asp:GridView runat="server" ID="gvAreas" CssClass="table"
                                ShowFooter="false" AutoGenerateColumns="false"
                                GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                DataKeyField="Id" EmptyDataText="No Data" ShowHeaderWhenEmpty="true"
                                OnRowCommand="gvAreas_RowCommand" AllowSorting="true" OnSorting="gvAreas_Sorting">
                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <RowStyle VerticalAlign="Middle" />
                                <AlternatingRowStyle CssClass="alt-table-data" />
                                <Columns>
                                    <asp:BoundField DataField="Id" Visible="false" />
                                    <asp:BoundField DataField="TitleEN" HeaderText="Title (EN)" SortExpression="TitleEN" />
                                    <asp:BoundField DataField="TitleAR" HeaderText="Title (AR)" SortExpression="TitleAR" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href="Blocks?id=<%# Eval("Id") %>" class="grid_button"><i class="fa fa-map-o" style="font-size: 25px;"></i></a>

                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditItem" CommandArgument='<%# Eval("Id")+","+ ((GridViewRow) Container).RowIndex %>'
                                                CssClass="grid_button" ToolTip="Edit">
                                                        <i class="fa fa-pencil" style="font-size:25px;"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteItem" CommandArgument='<%# Eval("Id") %>'
                                                CssClass="grid_button" ToolTip="Delete"
                                                OnClientClick="return confirm('Are you sure to delete?');">
                                                        <i class="fa fa-trash" style="font-size:25px;"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" OnPageChanged="AspNetPager1_PageChanged"
                                PageSize="15" CssClass="pages" CurrentPageButtonClass="cpb" NextPageText="Next"
                                PrevPageText="Previous" Wrap="true" Direction="RightToLeft" ShowFirstLast="False">
                            </webdiyer:AspNetPager>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>


