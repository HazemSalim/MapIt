<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="True" CodeBehind="ChangePassword.aspx.cs" Inherits="MapIt.Web.Admin.ChangePassword" %>

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
                    <li class="active">Change Password</li>
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
                    <h1 class="page-header">Change Password</h1>
                </div>
            </div>
            <!--/.row-->

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Change your password ...</div>
                        <div class="panel-body">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        Old Password
                                    </label>
                                    <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="txtOldPassword"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S"
                                        Text="* Required field" CssClass="alert-text" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label>
                                        New Password
                                    </label>
                                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" SetFocusOnError="true"
                                        Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revNewPassword" runat="server" ControlToValidate="txtNewPassword"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" CssClass="alert-text" SetFocusOnError="true"
                                        ValidationExpression="^.*(?=.{6,}).*$">
                                        Password must be at least 6 character
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Confirm New Password
                                    </label>
                                    <asp:TextBox ID="txtNewPassConfirm" runat="server" TextMode="Password" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvNewPassConfirm" runat="server" ControlToValidate="txtNewPassConfirm"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" SetFocusOnError="true"
                                        Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvNewPassConfirm" runat="server" EnableClientScript="true"
                                        Display="Dynamic" ValidationGroup="S" Text="Password not matched" ControlToValidate="txtNewPassConfirm"
                                        ControlToCompare="txtNewPassword" CssClass="alert-text" SetFocusOnError="true"></asp:CompareValidator>
                                </div>

                                <asp:Button ID="btnSave" runat="server" Text="Save" CausesValidation="true" ValidationGroup="S"
                                    CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            </div>
                            <div class="col-md-6">
                            </div>

                        </div>
                    </div>
                </div>
                <!-- /.col-->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
