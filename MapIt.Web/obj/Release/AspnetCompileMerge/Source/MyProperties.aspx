<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyProperties.aspx.cs" Inherits="MapIt.Web.MyProperties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%= GetGlobalResourceObject("Resource","web_title") %> | <%= GetGlobalResourceObject("Resource","my_properties") %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upPros" runat="server">
        <ContentTemplate>
            <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
                <div class="uk-container ">
                    <div class="uk-position-relative">
                        <!-- start body container -->
                        <div class="BodyContainer">
                            <h1 class="uk-heading-line uk-text-left pageTitle">
                                <span>
                                    <%= GetGlobalResourceObject("Resource","my_properties") %>
                                </span>
                            </h1>
                            <div class="uk-container ">
                                <div class="uk-child-width-1-3@m uk-grid" uk-sortable="handle: .uk-sortable-handle" uk-grid>

                                    <asp:Repeater ID="rPros" runat="server" OnItemCommand="rPros_ItemCommand">
                                        <ItemTemplate>
                                            <!-- ***************** start card **************** -->
                                            <div class=" uk-sortable-handle " uk-icon="icon: table">
                                                <div class="uk-card uk-card-default">
                                                    <div class="uk-card-media-top">
                                                        <div class="uk-position-relative uk-visible-toggle uk-light" uk-slideshow="animation: fade">
                                                            <ul class="uk-slideshow-items">
                                                                <li>
                                                                    <img src="<%# Eval("PropertyPhoto") %>" style="height: 180px;" alt='<%# Eval(Resources.Resource.db_title_col) %>' uk-cover>
                                                                </li>
                                                            </ul>
                                                            <a class="uk-position-center-left uk-position-small uk-hidden-hover" href="#" uk-slidenav-previous uk-slideshow-item="previous"></a>
                                                            <a class="uk-position-center-right uk-position-small uk-hidden-hover" href="#" uk-slidenav-next uk-slideshow-item="next"></a>
                                                        </div>
                                                    </div>
                                                    <div class="">
                                                        <div class="uk-position-top-right  sizeSpan" style="color: #fff;">
                                                            <%# Eval("Area") %> <%= GetGlobalResourceObject("Resource","m2") %>
                                                        </div>
                                                        <div class="uk-position-bottom-left  sizeSpan" style="color: #fff;">
                                                            <a style="color: #fff;" href="<%# Eval("PageName", "../Pro/{0}") %>" uk-toggle><%# Eval(Resources.Resource.db_title_col) %>
                                                            </a>

                                                            <p style="margin: 0;" style="color: #fff;">
                                                                <%# Eval(Resources.Resource.db_address_col) %>
                                                            </p>
                                                            <span class="icon-calendar2"><%# Culture.ToLower() == "ar-kw" ? MapIt.Helpers.PresentHelper.GetDurationAr((DateTime)Eval("AddedOn"))
                                                                    : MapIt.Helpers.PresentHelper.GetDurationEn((DateTime)Eval("AddedOn")) %></span>
                                                            <p>
                                                            </p>
                                                            <div>
                                                                <asp:Repeater ID="rComponents" runat="server">
                                                                    <ItemTemplate>
                                                                        <span>
                                                                            <img src="<%# Eval("Component.FinalPhoto") %>" style="width: 32px;" alt="" title="<%# Eval("Component."+Resources.Resource.db_title_col) %>" />
                                                                            <%# Eval("Count") %>
                                                                        </span>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                        <div class="uk-position-bottom-right  sizeSpan" style="padding: 5px;">
                                                            <asp:HiddenField ID="hfProId" Value='<%# Eval("Id") %>' runat="server" />
                                                            <a href="../ManageProperty?id=<%# Eval("Id") %>">
                                                                <i class="fa fa-pencil fa-2x" <%= GetGlobalResourceObject("Resource","edit_style") %>></i>
                                                            </a>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteItem" CommandArgument='<%# Eval("Id") %>'
                                                                OnClientClick="return confirm('Are you sure to delete?');" ToolTip="Delete">
                                                            <i class="fa fa-trash fa-2x" <%= GetGlobalResourceObject("Resource","delete_style") %>></i>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- ***************** end card **************** -->
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="uk-text-center sizeSpan">
                                <%--   <asp:Button ID="btnLoadMore" runat="server" CssClass="uk-button uk-button-primary" Text='<%$ Resources:Resource,load_more %>'
                                    OnClick="btnLoadMore_Click" />--%>
                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" OnPageChanged="AspNetPager1_PageChanged"
                                    PageSize="15" CssClass="pages" CurrentPageButtonClass="cpb" NextPageText="التالي"
                                    PrevPageText="السابق" Wrap="true" Direction="RightToLeft" ShowFirstLast="False">
                                </webdiyer:AspNetPager>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
