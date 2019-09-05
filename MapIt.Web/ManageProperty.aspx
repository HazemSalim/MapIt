<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="ManageProperty.aspx.cs" Inherits="MapIt.Web.ManageProperty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="/Scripts/jquery-1.7.1.js"></script>
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
                    <asp:UpdatePanel ID="upMPro" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div class="uk-fieldset uk-width-1-2@m uk-width-1-1@s">
                                <div class="uk-margin">
                                    <label for="purpose">
                                        <%= GetGlobalResourceObject("Resource","purpose") %></label>
                                    <asp:DropDownList ID="ddlPurpose" runat="server" CssClass="uk-input" AppendDataBoundItems="true">
                                        <asp:ListItem Text='<%$ Resources:Resource,purpose %>' Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvPurpose" runat="server" ControlToValidate="ddlPurpose" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro" Text='<%$ Resources:Resource,required_field %>'
                                        CssClass="alert-text" InitialValue=""></asp:RequiredFieldValidator>
                                </div>

                                <div class="uk-margin">
                                    <label for="type">
                                        <%= GetGlobalResourceObject("Resource","type") %></label>
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="uk-input"
                                        AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                        <asp:ListItem Text='<%$ Resources:Resource,type %>' Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro" Text='<%$ Resources:Resource,required_field %>'
                                        CssClass="alert-text" InitialValue=""></asp:RequiredFieldValidator>
                                </div>

                                <div class="uk-margin">
                                    <label for="paci">
                                        <%= GetGlobalResourceObject("Resource","paci_no") %></label>
                                    <asp:TextBox ID="txtPACI" runat="server" CssClass="uk-input" OnTextChanged="txtPACI_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>

                                <div class="uk-margin">
                                    <label for="country">
                                        <%= GetGlobalResourceObject("Resource","country") %></label>
                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="uk-input" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                        AppendDataBoundItems="true" AutoPostBack="True">
                                        <asp:ListItem Text='<%$ Resources:Resource,country %>' Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro"
                                        Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text" InitialValue=""></asp:RequiredFieldValidator>

                                </div>

                                <div class="uk-margin">
                                    <label for="city">
                                        <%= GetGlobalResourceObject("Resource","city") %></label>
                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="uk-input"
                                        AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                                        <asp:ListItem Text='<%$ Resources:Resource,city %>' Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro"
                                        Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text" InitialValue=""></asp:RequiredFieldValidator>

                                </div>

                                <div class="uk-margin">
                                    <label for="area">
                                        <%= GetGlobalResourceObject("Resource","area") %></label>
                                    <asp:DropDownList ID="ddlArea" runat="server" CssClass="uk-input"
                                        AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                                        <asp:ListItem Text='<%$ Resources:Resource,area %>' Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvArea" runat="server" ControlToValidate="ddlArea"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro"
                                        Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text" InitialValue=""></asp:RequiredFieldValidator>

                                </div>

                                <div class="uk-margin">
                                    <label for="block">
                                        <%= GetGlobalResourceObject("Resource","block") %></label>
                                    <asp:DropDownList ID="ddlBlock" runat="server" CssClass="uk-input" AppendDataBoundItems="true">
                                        <asp:ListItem Text='<%$ Resources:Resource,block %>' Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBlock" runat="server" ControlToValidate="ddlBlock"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro"
                                        Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text" InitialValue=""></asp:RequiredFieldValidator>

                                </div>

                                <div class="uk-margin">
                                    <label for="street">
                                        <%= GetGlobalResourceObject("Resource","street") %></label>
                                    <asp:TextBox ID="txtStreet" runat="server" CssClass="uk-input"></asp:TextBox>
                                </div>

                                <%--<div id="div_Components" class="uk-margin" runat="server" visible="false">
                                    <label for="components">
                                        <%= GetGlobalResourceObject("Resource","components") %>
                                    </label>
                                    <div class="checkbox checkboxlist" style="padding-left: 25px;">
                                        <asp:CheckBoxList ID="cblComponents" runat="server">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>--%>
                                <div id="div_Components" class="uk-margin" runat="server" visible="false">
                                    <label for="components">
                                        <b><%= GetGlobalResourceObject("Resource","components") %>:</b></label>
                                    <asp:Repeater ID="rComponents" runat="server">
                                        <ItemTemplate>

                                            <div class="uk-margin">
                                                <label>
                                                    <asp:HiddenField ID="hdnComponentId" runat="server" Value='<%# Eval("ComponentId") %>' />
                                                    <%# Eval("TitleEN") %>
                                                </label>
                                                <asp:TextBox ID="txtCount" runat="server" TextMode="Number" CssClass="uk-input" Text='<%# Eval("Count") %>' min="0" oninput="validity.valid||(value='');"></asp:TextBox></td>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                                <div id="div_Aream2" class="uk-margin" runat="server" visible="false">
                                    <label for="area_m2">
                                        <%= GetGlobalResourceObject("Resource","area_m2") %></label>
                                    <asp:TextBox ID="txtAream2" runat="server" TextMode="Number" CssClass="uk-input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAream2" runat="server" ControlToValidate="txtAream2"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro"
                                        Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvAream2" runat="server" ControlToValidate="txtAream2"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro"
                                        Text="Error" CssClass="alert-text" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                </div>

                                <div id="div_SellingPrice" class="uk-margin" runat="server" visible="false">
                                    <label for="selling_price">
                                        <%= GetGlobalResourceObject("Resource","selling_price") %></label>
                                    <asp:TextBox ID="txtSellingPrice" runat="server" TextMode="Number" CssClass="uk-input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSellingPrice" runat="server" ControlToValidate="txtSellingPrice"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro"
                                        Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvSellingPrice" runat="server" ControlToValidate="txtSellingPrice"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro"
                                        Text="Error" CssClass="alert-text" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                </div>

                                <div id="div_RentPrice" class="uk-margin" runat="server" visible="false">
                                    <label for="rent_price">
                                        <%= GetGlobalResourceObject("Resource","rent_price") %></label>
                                    <asp:TextBox ID="txtRentPrice" runat="server" TextMode="Number" CssClass="uk-input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvRentPrice" runat="server" ControlToValidate="txtRentPrice"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro"
                                        Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvRentPrice" runat="server" ControlToValidate="txtRentPrice"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro"
                                        Text="Error" CssClass="alert-text" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                </div>

                                <div id="div_BuildingAge" class="uk-margin" runat="server" visible="false">
                                    <label for="building_age">
                                        <%= GetGlobalResourceObject("Resource","building_age") %></label>
                                    <asp:TextBox ID="txtBuildingAge" runat="server" TextMode="Number" CssClass="uk-input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvBuildingAge" runat="server" ControlToValidate="txtBuildingAge"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro"
                                        Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvBuildingAge" runat="server" ControlToValidate="txtBuildingAge"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro"
                                        Text="Error" CssClass="alert-text" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                </div>

                                <div id="div_Details" class="uk-margin" runat="server" visible="false">
                                    <label for="details">
                                        <%= GetGlobalResourceObject("Resource","details") %></label>
                                    <asp:TextBox ID="txtDetails" runat="server" TextMode="MultiLine" CssClass="uk-input" Style="height: 100px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDetails" runat="server" ControlToValidate="txtDetails"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro"
                                        Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                </div>

                                <div id="div_Features" class="uk-margin" runat="server" visible="false">
                                    <label for="features">
                                        <b><%= GetGlobalResourceObject("Resource","features") %>:</b></label>
                                    <asp:Repeater ID="rFeatures" runat="server">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnFeatureId" runat="server" Value='<%# Eval("FeatureId") %>' />
                                            <asp:CheckBox ID="chkFeature" runat="server" Checked='<%# Eval("IsChecked") %>' Text='<%# Eval("TitleEN") %>' />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                                <div id="div_MonthlyIncome" class="uk-margin" runat="server" visible="false">
                                    <label for="monthly_income">
                                        <%= GetGlobalResourceObject("Resource","monthly_income") %></label>
                                    <asp:TextBox ID="txtMonthlyIncome" runat="server" TextMode="Number" CssClass="uk-input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvMonthlyIncome" runat="server" ControlToValidate="txtMonthlyIncome"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro"
                                        Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvMonthlyIncome" runat="server" ControlToValidate="txtMonthlyIncome"
                                        SetFocusOnError="true" EnableClientScript="true" Display="Dynamic" ValidationGroup="MPro"
                                        Text="Error" CssClass="alert-text" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                </div>

                                <div class="uk-margin" style="position: relative;">
                                    <label for="location">
                                        <%= GetGlobalResourceObject("Resource","location") %>
                                    </label>
                                    <asp:TextBox ID="txtLocation" runat="server" Enabled="false" ClientIDMode="Static" CssClass="uk-input" />
                                    <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="txtLocation"
                                        EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="MPro"
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
                                        <%= GetGlobalResourceObject("Resource","other_phones") %></label>
                                    <asp:Repeater ID="rOtherPhones" runat="server" OnItemCommand="rOtherPhones_ItemCommand" OnItemDataBound="rOtherPhones_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="option_row">
                                                <div class="uk-margin">
                                                    <asp:HiddenField ID="hfCode1" runat="server" Value='<%# Eval("Code") %>' />
                                                    <asp:DropDownList ID="ddlCode1" runat="server" CssClass="uk-input l_tel">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtPhone1" runat="server" TextMode="Phone" CssClass="uk-input r_tel" Text='<%# Eval("Phone") %>'></asp:TextBox>
                                                </div>

                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteItem" ToolTip="Delete">
                                                            <img src="/Images/delete.png" alt="Delete"  class="option_row_btn_image" /></asp:LinkButton>
                                                <div class="clearfix"></div>

                                            </div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div class="option_row">
                                                <div class="uk-margin">
                                                    <asp:HiddenField ID="hfCode1" runat="server" Value='<%# Eval("Code") %>' />
                                                    <asp:DropDownList ID="ddlCode1" runat="server" CssClass="uk-input l_tel">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtPhone1" runat="server" TextMode="Phone" CssClass="uk-input r_tel" Text='<%# Eval("Phone") %>'></asp:TextBox>
                                                </div>

                                                <asp:LinkButton ID="lnkAdd" runat="server" CommandName="AddItem" ToolTip="Add">
                                                    <img src="/Images/plus.png" alt="Add" class="option_row_btn_image" /></asp:LinkButton>
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
                                    Style="margin-top: 20px;" Text='<%$ Resources:Resource,save %>' OnClick="btnSave_Click" ValidationGroup="MPro" CausesValidation="true" />

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
