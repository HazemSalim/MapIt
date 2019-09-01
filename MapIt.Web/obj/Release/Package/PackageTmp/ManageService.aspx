<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="ManageService.aspx.cs" Inherits="MapIt.Web.ManageService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="/Scripts/jquery-1.7.1.js"></script>
    <link href="/Scripts/Calendar/jquery.ui.datepicker.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/Calendar/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/scripts/Calendar/jquery.ui.datepicker.js"></script>
    <link href="/Content/colorbox.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery.colorbox.js"></script>
    <script type="text/javascript">
        function pageLoad(sender, args) {

            $(document).ready(function () {
                $(".iframe").colorbox({ iframe: true, transition: "none", width: "770", height: "570" });
                $(".callbacks").colorbox({ onCleanup: function () { updateLocation(); } });
            });
        }

        function updateLocation() {
            try {
                var longitude = document.getElementById("hfLongitude");
                var latitude = document.getElementById("hfLatitude");
                var txtLocation = document.getElementById("txtLocation");
                if (longitude.value != '' && latitude.value != '')
                    txtLocation.value = latitude.value + ', ' + longitude.value;
                else
                    txtLocation.value = '';
            } catch (e) {

            }
        }

        function clearLocation() {
            try {
                var longitude = document.getElementById("hfLongitude");
                var latitude = document.getElementById("hfLatitude");
                var txtLocation = document.getElementById("txtLocation");

                longitude.value = '';
                latitude.value != '';
                txtLocation.value = '';
            } catch (e) {

            }
        }
    </script>

    <style type="text/css">
        .custom-file-input::-webkit-file-upload-button {
            visibility: hidden;
        }

        .custom-file-input::before {
            content: '';
            display: inline-block;
            background: url('Images/add_img.png') no-repeat center center;
            width: 100px;
            height: 100px;
            outline: none;
            white-space: nowrap;
            -webkit-user-select: none;
            cursor: pointer;
        }

        .custom-file-input:hover::before {
            border-color: black;
        }

        .custom-file-input:active::before {
            border: 1px solid #009999;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
        <div class="uk-container ">
            <div class="uk-position-relative">

                <!-- Start Site Content -->
                <div class="BodyContainer">
                    <h1 class="uk-heading-line uk-text-left pageTitle">
                        <span>
                            <asp:Literal ID="litTitle" runat="server" />
                        </span></h1>
                    <asp:UpdatePanel ID="upMSrv" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div class="uk-fieldset uk-width-1-2@m uk-width-1-1@s">
                                <div class="uk-margin">
                                    <label for="type">
                                        <%= GetGlobalResourceObject("Resource","type") %></label>
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="uk-input">
                                        <asp:ListItem Text="<%$ Resources:Resource,individual %>" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="<%$ Resources:Resource,company %>" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCategory" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="MSrv" Text='<%$ Resources:Resource,required_field %>'
                                        CssClass="alert-text" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group" id="div_area" runat="server" visible="false">
                                    <label><%= GetGlobalResourceObject("Resource","working_areas") %></label>
                                    <div style="display: block; padding-top: 20px;">
                                        <label>
                                            <input id="allAreas" runat="server" type="checkbox" onchange="checkAll(this,'areas')" name="chk[]" />
                                            <b><%= GetGlobalResourceObject("Resource","select_all") %></b>
                                        </label>
                                    </div>
                                    <div id="areas" style="width: 220px; height: 250px; overflow-y: auto;">
                                        <asp:Repeater ID="rAreas" runat="server">
                                            <ItemTemplate>
                                                <div class="checkbox">
                                                    <label>
                                                        <asp:HiddenField ID="hfAreaId" runat="server" Value='<%# Eval("AreaId") %>' />
                                                        <asp:CheckBox ID="chkCovered" runat="server" CssClass="chkitem" Checked='<%# ((bool)Eval("IsCovered")) %>' />
                                                        <%# Eval("Area") %>
                                                    </label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="uk-margin">
                                    <label for="type">
                                        <%= GetGlobalResourceObject("Resource","category") %></label>
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="uk-input" AppendDataBoundItems="true">
                                        <asp:ListItem Text='<%$ Resources:Resource,category %>' Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="MSrv" Text='<%$ Resources:Resource,required_field %>'
                                        CssClass="alert-text" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                                <div class="uk-margin">
                                    <label for="country">
                                        <%= GetGlobalResourceObject("Resource","country") %></label>
                                    </label>
                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="uk-input">
                                        <asp:ListItem Text='<%$ Resources:Resource,country %>' Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="MSrv" Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"
                                        InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                                <div class="uk-margin">
                                    <label for="city">
                                        <%= GetGlobalResourceObject("Resource","city") %>
                                    </label>
                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="uk-input" AppendDataBoundItems="true">
                                        <asp:ListItem Text='<%$ Resources:Resource,city %>' Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MSrv"
                                        Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                                <div class="uk-margin">
                                    <label for="title">
                                        <%= GetGlobalResourceObject("Resource","provider_name") %>
                                    </label>
                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="uk-input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MSrv"
                                        Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                                <div class="uk-margin">
                                    <label for="ex_years">
                                        <%= GetGlobalResourceObject("Resource","ex_years") %>
                                    </label>
                                    <asp:TextBox ID="txtExYears" runat="server" TextMode="Number" CssClass="uk-input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvExYears" runat="server" ControlToValidate="txtExYears"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MSrv"
                                        Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                                <div class="uk-margin">
                                    <label for="description">
                                        <%= GetGlobalResourceObject("Resource","description") %>
                                    </label>
                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="uk-input" Style="height: 100px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MSrv"
                                        Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>
                                <div class="uk-margin" style="position: relative;">
                                    <label for="location">
                                        <%= GetGlobalResourceObject("Resource","location") %>
                                    </label>
                                    <asp:TextBox ID="txtLocation" runat="server" Enabled="false" ClientIDMode="Static" CssClass="uk-input" />
                                    <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="txtLocation"
                                        EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="MSrv"
                                        Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                    <a class="callbacks iframe location_icon" href="LocationByEsri?marker=1">
                                        <img src="/images/location_icon.png" alt="Location" />
                                    </a>
                                    <a class="delete_location_icon" style="cursor: pointer;" onclick="clearLocation();">
                                        <img src="/images/delete.png" alt="Location" />
                                    </a>
                                    <input id="hfLatitude" runat="server" clientidmode="Static" type="hidden" />
                                    <input id="hfLongitude" runat="server" clientidmode="Static" type="hidden" />
                                </div>
                                <div class="uk-margin">
                                    <label for="other_phones">
                                        <%= GetGlobalResourceObject("Resource","other_phones") %>
                                    </label>
                                    <asp:Repeater ID="rOtherPhones" runat="server" OnItemCommand="rOtherPhones_ItemCommand" OnItemDataBound="rOtherPhones_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="option_row">
                                                <div class="uk-width-1-1@m uk-width-1-1@s">
                                                    <div class="uk-margin">
                                                        <asp:HiddenField ID="hfCode1" runat="server" Value='<%# Eval("Code") %>' />
                                                        <asp:DropDownList ID="ddlCode1" runat="server" CssClass="uk-input l_tel">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtPhone1" runat="server" TextMode="Phone" CssClass="uk-input r_tel" Text='<%# Eval("Phone") %>'></asp:TextBox>
                                                    </div>
                                                </div>

                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteItem" ToolTip='<%$ Resources:Resource,delete %>'>
                                                    <img src="/Images/delete.png" alt='<%= GetGlobalResourceObject("Resource","delete") %>'  class="option_row_btn_image" /></asp:LinkButton>
                                                <div class="clearfix"></div>

                                            </div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div class="option_row">

                                                <div class="uk-width-1-1@m uk-width-1-1@s">
                                                    <div class="uk-margin">
                                                        <asp:HiddenField ID="hfCode1" runat="server" Value='<%# Eval("Code") %>' />
                                                        <asp:DropDownList ID="ddlCode1" runat="server" CssClass="uk-input l_tel">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtPhone1" runat="server" TextMode="Phone" CssClass="uk-input r_tel" Text='<%# Eval("Phone") %>'></asp:TextBox>
                                                    </div>
                                                </div>

                                                <asp:LinkButton ID="lnkAdd" runat="server" CommandName="AddItem" ToolTip='<%$ Resources:Resource,add %>'>
                                                    <img src="/Images/plus.png" alt='<%= GetGlobalResourceObject("Resource","add") %>' class="option_row_btn_image" /></asp:LinkButton>
                                                <div class="clearfix"></div>
                                            </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>

                                <div id="div_Photos" runat="server" class="uk-margin">
                                    <label for="photos" style="display: block; padding-top: 60px;">
                                        <%= GetGlobalResourceObject("Resource","photos") %></label>
                                    <asp:Repeater ID="rPhotos" runat="server">
                                        <ItemTemplate>
                                            <div class="ad_photo_item">
                                                <asp:FileUpload ID="fuPhoto" runat="server" class="custom-file-input"
                                                    Style="width: 100px; height: 100px; font-size: 0px;" onchange="loadFile(event)" />
                                                <img src='<%# Eval("FullPhoto") %>' alt="" />
                                                <asp:HiddenField ID="hfPhoto" runat="server" Value='<%# Eval("Photo") %>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                                <asp:Button ID="btnSave" runat="server" CssClass="uk-button uk-button-primary uk-width-1-1 uk-margin-small-bottom"
                                    Style="margin-top: 20px;" Text='<%$ Resources:Resource,save %>' OnClick="btnSave_Click" ValidationGroup="MSrv" CausesValidation="true" />

                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSave" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
