<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MapIt.Web.Admin._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="row">
        <ol class="breadcrumb">
            <li><a href=".">
                <i class="fa fa-home"></i>
            </a></li>
            <li class="active">Dashboard</li>
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
            <h1 class="page-header">Dashboard</h1>
        </div>
    </div>
    <!--/.row-->

    <div class="row">
        <div class="col-xs-12 col-md-4 col-lg-4">
            <div class="panel panel-red panel-widget ">
                <div class="row no-padding">
                    <div class="col-sm-3 col-lg-5 widget-left glyphicon-l">
                        <i class="fa fa-id-card-o"></i>
                    </div>
                    <div class="col-sm-9 col-lg-7 widget-right">
                        <div class="large">
                            <asp:Literal ID="litUserCredits" runat="server" />
                        </div>
                        <div class="text-muted">Users Credits</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-md-4 col-lg-4">
            <div class="panel panel-teal panel-widget">
                <div class="row no-padding">
                    <div class="col-sm-3 col-lg-5 widget-left glyphicon-l">
                        <i class="fa fa-home"></i>
                    </div>
                    <div class="col-sm-9 col-lg-7 widget-right">
                        <div class="large">
                            <asp:Literal ID="litProperties" runat="server" />
                        </div>
                        <div class="text-muted">Properties</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-md-4 col-lg-4">
            <div class="panel panel-orange panel-widget">
                <div class="row no-padding">
                    <div class="col-sm-3 col-lg-5 widget-left glyphicon-l">
                        <i class="fa fa-home"></i>
                    </div>
                    <div class="col-sm-9 col-lg-7 widget-right">
                        <div class="large">
                            <asp:Literal ID="litPendingProperties" runat="server" />
                        </div>
                        <div class="text-muted">Pending Properties</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/.row-->

    <div class="row">
        <div class="col-xs-12 col-md-6 col-lg-3">
            <div class="panel panel-blue panel-widget ">
                <div class="row no-padding">
                    <div class="col-sm-3 col-lg-5 widget-left glyphicon-l">
                        <i class="fa fa-id-card"></i>
                    </div>
                    <div class="col-sm-9 col-lg-7 widget-right">
                        <div class="large">
                            <asp:Literal ID="litCreUsers" runat="server" />
                        </div>
                        <div class="text-muted">Created Users</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-md-6 col-lg-3">
            <div class="panel panel-teal panel-widget">
                <div class="row no-padding">
                    <div class="col-sm-3 col-lg-5 widget-left glyphicon-l">
                        <i class="fa fa-users"></i>
                    </div>
                    <div class="col-sm-9 col-lg-7 widget-right">
                        <div class="large">
                            <asp:Literal ID="litPUsers" runat="server" />
                        </div>
                        <div class="text-muted">Paid Users</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-md-6 col-lg-3">
            <div class="panel panel-red panel-widget">
                <div class="row no-padding">
                    <div class="col-sm-3 col-lg-5 widget-left glyphicon-l">
                        <i class="fa fa-users"></i>
                    </div>
                    <div class="col-sm-9 col-lg-7 widget-right">
                        <div class="large">
                            <asp:Literal ID="litNPUsers" runat="server" />
                        </div>
                        <div class="text-muted">NPaidUsers</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-md-6 col-lg-3">
            <div class="panel panel-orange panel-widget">
                <div class="row no-padding">
                    <div class="col-sm-3 col-lg-5 widget-left glyphicon-l">
                        <i class="fa fa-users"></i>
                    </div>
                    <div class="col-sm-9 col-lg-7 widget-right">
                        <div class="large">
                            <asp:Literal ID="litAcUsers" runat="server" />
                        </div>
                        <div class="text-muted">Active Users</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/.row-->

</asp:Content>
