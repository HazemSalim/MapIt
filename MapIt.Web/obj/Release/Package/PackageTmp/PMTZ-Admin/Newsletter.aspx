<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="True" CodeBehind="Newsletter.aspx.cs" Inherits="MapIt.Web.Admin.Newsletter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <ol class="breadcrumb">
                    <li><a href=".">
                        <i class="fa fa-home"></i>
                    </a></li>
                    <li class="active">Newsletter</li>
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
                    <h1 class="page-header">Newsletter</h1>
                </div>
            </div>
            <!--/.row-->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Newsletter Form</div>
                        <div class="panel-body">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>
                                        Email Group
                                    </label>
                                    <asp:DropDownList ID="ddlEmailGroups" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Text="All Groups" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Users" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="News Subscribers" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        To <span style="font-size: 0.9em; color: #f00;">(emails separated by ;)</span>
                                    </label>
                                    <asp:TextBox ID="txtTo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label>
                                        Subject
                                    </label>
                                    <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label>
                                        Message
                                    </label>
                                    <CKEditor:CKEditorControl ID="txtMessage" runat="server" Height="200" BasePath="~/scripts/ckeditor">
                                    </CKEditor:CKEditorControl>
                                </div>

                                <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.col-->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
