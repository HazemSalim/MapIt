<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="true" CodeBehind="PropertySettings.aspx.cs" Inherits="MapIt.Web.Admin.PropertySettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .td_section_head {
            background-color: #ddd !important;
            font-weight: bold !important;
            color: #000 !important;
            padding: 5px !important;
            border-top: 1px solid #000 !important;
            border-bottom: 2px solid #ddd !important;
        }

        .chk input[type="checkbox"] {
            width: 20px;
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>

            <div class="row">
                <ol class="breadcrumb">
                    <li><a href=".">
                        <i class="fa fa-home"></i>
                    </a></li>
                    <li class="active">Property Settings</li>
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
                    <h1 class="page-header">Property Settings</h1>
                </div>
            </div>
            <!--/.row-->

            <div class="row">
                <div class="col-lg-12">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    Property Type
                                </label>

                                <asp:DropDownList ID="ddlPropertyType" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPropertyType_SelectedIndexChanged">
                                    <asp:ListItem Text="Choose type" Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading">Settings List</div>
                        <div class="panel-body">
                            <asp:Label ID="lblMSG" runat="server" ForeColor="Red" Font-Bold="true">Choose property type first</asp:Label>
                            <div id="dvFields" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Repeater ID="rprSettings" runat="server">
                                            <HeaderTemplate>
                                                <table class="table">
                                                    <tr>
                                                        <td>Fields</td>
                                                        <td style="width: 60px;">Available
                                                        </td>
                                                        <td style="width: 60px;">Mondatory
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfPropertyName" runat="server" Value='<%# Eval("PropertyName") %>' />
                                                <asp:HiddenField ID="hfCategory" runat="server" Value='<%# Eval("CategoryName") %>' />
                                                <tr>
                                                    <td id="td_Category" runat="server" class="td_section_head" visible='<%# Eval("PropertyId") == null %>'>
                                                        <%# Eval("CategoryText") %>
                                                    </td>
                                                    <td id="td_Property" runat="server" visible='<%# Eval("PropertyId") != null %>'>
                                                        <%# Eval("PropertyText") %>
                                                    </td>
                                                    <td class='<%# Eval("PropertyId") == null ?  "td_section_head":"" %>'>
                                                        <asp:CheckBox ID="chkAvailable" runat="server" Checked='<%# Eval("IsAvailable").ToString() == "1"? true : false %>' CssClass="chk" Visible='<%# Eval("PropertyId") != null %>' />
                                                    </td>
                                                    <td class='<%# Eval("PropertyId") == null ?  "td_section_head":"" %>'>
                                                        <asp:CheckBox ID="chkMondatory" runat="server" Checked='<%# Eval("IsMondatory").ToString() == "1"? true : false %>' CssClass="chk" Visible='<%# (IsMainCategory(Eval("CategoryName")) && Eval("PropertyId") != null) || (!IsMainCategory(Eval("CategoryName")) && Eval("PropertyId") == null) %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>

                                    <div class="col-md-12">
                                        <hr />
                                        <div class="text-center">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
                                                ValidationGroup="S" CausesValidation="true" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

