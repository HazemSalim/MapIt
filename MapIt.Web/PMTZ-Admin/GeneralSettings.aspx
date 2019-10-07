<%@ Page Title="" Language="C#" MasterPageFile="~/PMTZ-Admin/Site.master" AutoEventWireup="True" CodeBehind="GeneralSettings.aspx.cs" Inherits="MapIt.Web.Admin.GeneralSettings" %>

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
                    <li class="active">General Settings</li>
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
                    <h1 class="page-header">General Settings</h1>
                </div>
            </div>
            <!--/.row-->

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">General Settings Details</div>
                        <div class="panel-body">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Website Title (EN)
                                    </label>
                                    <asp:TextBox ID="txtTitleEN" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvTitleEN" runat="server" ControlToValidate="txtTitleEN" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Website Title (AR)
                                    </label>
                                    <asp:TextBox ID="txtTitleAR" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvTitleAR" runat="server" ControlToValidate="txtTitleAR" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Meta Description
                                    </label>
                                    <asp:TextBox ID="txtMetaDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50px" />
                                    <asp:RequiredFieldValidator ID="rfvMetaDescription" runat="server" ControlToValidate="txtMetaDescription"
                                        EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Meta keyWords
                                    </label>
                                    <asp:TextBox ID="txtMetaKW" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50px" />
                                    <asp:RequiredFieldValidator ID="rfvMetaKW" runat="server" ControlToValidate="txtMetaKW"
                                        EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Website
                                    </label>
                                    <asp:TextBox ID="txtWebsite" runat="server" TextMode="Url" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Facebook
                                    </label>
                                    <asp:TextBox ID="txtFacebook" runat="server" TextMode="Url" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Twitter
                                    </label>
                                    <asp:TextBox ID="txtTwitter" runat="server" TextMode="Url" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Instagram
                                    </label>
                                    <asp:TextBox ID="txtInstagram" runat="server" TextMode="Url" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Youtube
                                    </label>
                                    <asp:TextBox ID="txtYoutube" runat="server" TextMode="Url" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Linkedin
                                    </label>
                                    <asp:TextBox ID="txtLinkedin" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Snapchat
                                    </label>
                                    <asp:TextBox ID="txtSnapchat" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Tumblr
                                    </label>
                                    <asp:TextBox ID="txtTumblr" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        App Store
                                    </label>
                                    <asp:TextBox ID="txtAppStore" runat="server" TextMode="Url" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Google Play
                                    </label>
                                    <asp:TextBox ID="txtGooglePlay" runat="server" TextMode="Url" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Latitude
                                    </label>
                                    <asp:TextBox ID="txtLatitude" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvLatitude" runat="server" ControlToValidate="txtLatitude" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Longitude
                                    </label>
                                    <asp:TextBox ID="txtLongitude" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvLongitude" runat="server" ControlToValidate="txtLongitude" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Address
                                    </label>
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50px" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Email
                                    </label>
                                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Phone
                                    </label>
                                    <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Fax
                                    </label>
                                    <asp:TextBox ID="txtFax" runat="server" TextMode="Phone" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Default Country
                                    </label>
                                    <asp:DropDownList ID="ddlDefaultCountry" runat="server" AppendDataBoundItems="true" CssClass="form-control" Style="width: 100%;">
                                        <asp:ListItem Text="Select Country" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDefaultCountry" runat="server" ControlToValidate="ddlDefaultCountry"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Default Currency
                                    </label>
                                    <asp:DropDownList ID="ddlDefaultCurrency" runat="server" AppendDataBoundItems="true" CssClass="form-control" Style="width: 100%;">
                                        <asp:ListItem Text="Select Currency" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDefaultCurrency" runat="server" ControlToValidate="ddlDefaultCurrency"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Invoice Terms
                                    </label>
                                    <asp:TextBox ID="txtInvoiceTerms" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50px" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        WorkingHours
                                    </label>
                                    <asp:TextBox ID="txtWorkingHours" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50px" />
                                    <asp:RequiredFieldValidator ID="rfvWorkingHours" runat="server" ControlToValidate="txtWorkingHours" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="S" Text="* Required field" CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Def. Free Amount
                                    </label>
                                    <asp:TextBox runat="server" ID="txtDefFreeAmount" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDefFreeAmount" runat="server" ControlToValidate="txtDefFreeAmount"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Normal Ad Cost
                                    </label>
                                    <asp:TextBox runat="server" ID="txtNormalAdCost" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNormalAdCost" runat="server" ControlToValidate="txtNormalAdCost"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Special Ad Cost
                                    </label>
                                    <asp:TextBox runat="server" ID="txtSpecAdCost" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSpecAdCost" runat="server" ControlToValidate="txtSpecAdCost"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Ad Video Cost
                                    </label>
                                    <asp:TextBox runat="server" ID="txtAdVideoCost" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAdVideoCost" runat="server" ControlToValidate="txtAdVideoCost"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Normal Ad Duration (Days)
                                    </label>
                                    <asp:TextBox runat="server" ID="txtNAdDuration" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNAdDuration" runat="server" ControlToValidate="txtNAdDuration"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Special Ad Duration (Days)
                                    </label>
                                    <asp:TextBox runat="server" ID="txtSAdDuration" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSAdDuration" runat="server" ControlToValidate="txtSAdDuration"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Avalible Photos
                                    </label>
                                    <asp:TextBox runat="server" ID="txtAvPhotos" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAvPhotos" runat="server" ControlToValidate="txtAvPhotos"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Avalible Videos
                                    </label>
                                    <asp:TextBox runat="server" ID="txtAvVideos" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAvVideos" runat="server" ControlToValidate="txtAvVideos"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Similar Ad Count
                                    </label>
                                    <asp:TextBox runat="server" ID="txtSimilarAdCount" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSimilarAdCount" runat="server" ControlToValidate="txtSimilarAdCount"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Page Size
                                    </label>
                                    <asp:TextBox runat="server" ID="txtPageSize" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPageSize" runat="server" ControlToValidate="txtPageSize"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Page Size Mobile
                                    </label>
                                    <asp:TextBox runat="server" ID="txtPageSizeMob" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPageSizeMob"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">

                                <div class="form-group">
                                    <label>
                                        Version
                                    </label>
                                    <asp:TextBox runat="server" ID="txtVersion" TextMode="Number" CssClass="form-control" Text="1.0" min="1.0" step="0.1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvVersion" runat="server" ControlToValidate="txtVersion"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Message Close English
                                    </label>
                                    <asp:TextBox runat="server" ID="txtMessageCloseEnglish" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMessageCloseEnglish"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Message Close Arabic
                                    </label>
                                    <asp:TextBox runat="server" ID="txtMessageCloseArabic" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMessageCloseArabic"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        IOS App Url
                                    </label>
                                    <asp:TextBox runat="server" ID="txtIOSAppUrl" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtIOSAppUrl"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Message Update English
                                    </label>
                                    <asp:TextBox runat="server" ID="txtMessageUpdateEnglish" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMessageUpdateEnglish"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        Message Update Arabic
                                    </label>
                                    <asp:TextBox runat="server" ID="txtMessageUpdateArabic" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMessageUpdateArabic"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        IOS Version Number
                                    </label>
                                    <asp:TextBox runat="server" ID="txtIOSVersionNumber" TextMode="Number" CssClass="form-control" Text="1.0" min="1.0" step="0.1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtIOSVersionNumber"
                                        Display="Dynamic" EnableClientScript="true" Text="* Required field" ValidationGroup="S"
                                        CssClass="alert-text" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="chkAutoActiveUser" runat="server" />
                                            Auto Active User
                                           
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="chkAutoActiveAd" runat="server" />
                                            Auto Active Ad
                                           
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="chkIsForceUpdateIOS" runat="server" />
                                            Is Force Update IOS
                                           
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="chkIsForceClose" runat="server" />
                                            Is Force Close
                                           
                                        </label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="form-group">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CausesValidation="true" ValidationGroup="S"
                                CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                    </div>
                        </div>

                    </div>
                </div>
            </div>
            <!-- /.col-->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
