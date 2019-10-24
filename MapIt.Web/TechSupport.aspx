<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TechSupport.aspx.cs" Inherits="MapIt.Web.TechSupport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%= GetGlobalResourceObject("Resource","web_title") %> | <%= GetGlobalResourceObject("Resource","tech_support") %></title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="uk-background-fixed uk-background-fixed uk-background-center-center">
        <div class="uk-container">
            <div class="uk-position-relative">
                <div class="BodyContainer">
                    <h1 class="uk-heading-line uk-text-left pageTitle">
                        <span>
                            <%= GetGlobalResourceObject("Resource","tech_support") %>
                        </span></h1>
                    <asp:UpdatePanel ID="upReportAbuse" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <fieldset class="uk-fieldset uk-width-1-2@m uk-width-1-1@s">

                                <div class="uk-margin">
                                    <label for="txtNotes">
                                        <%= GetGlobalResourceObject("Resource","message") %></label>
                                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Columns="5" ClientIDMode="Static" Placeholder="<%$ Resources:Resource,message %>"
                                        CssClass="uk-input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNotes" SetFocusOnError="true"
                                        EnableClientScript="true" Display="Dynamic" ValidationGroup="MSrv" Text='<%$ Resources:Resource,required_field %>' CssClass="alert-text"
                                        InitialValue=""></asp:RequiredFieldValidator>

                                    <div style="clear: both"></div>
                                </div>

                                <asp:Button ID="btnSubmit" runat="server" CssClass="uk-button uk-button-primary uk-width-1-1 uk-margin-small-bottom"
                                    Text='<%$ Resources:Resource,submit %>' OnClick="btnSubmit_Click" ValidationGroup="MSrv" CausesValidation="true" />
                            </fieldset>


                            <%if (gvMessages.Rows.Count > 0)
                                { %>
                            <div class="uk-background-fixed uk-background-fixed uk-background-center-center  ">
                                <div class="uk-container ">
                                    <div class="uk-position-relative">

                                        <div class="BodyContainer">
                                            <h1 class="uk-heading-line uk-text-left pageTitle">
                                                <span>
                                                    <%= GetGlobalResourceObject("Resource","messagesList") %>
                                                </span>
                                            </h1>
                                            <div class="uk-container ">
                                                <div class="uk-child-width-1-1 uk-child-width-1-1@s uk-grid-match uk-grid-small" uk-grid>


                                                    <asp:GridView runat="server" ID="gvMessages" CssClass="table"
                                                        ShowFooter="false" AutoGenerateColumns="false"
                                                        GridLines="None" AllowPaging="false" PagerStyle-Visible="false"
                                                        DataKeyField="Id" EmptyDataText="No Data" ShowHeaderWhenEmpty="true"
                                                        AllowSorting="true" OnSorting="gvMessages_Sorting">
                                                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                                        <RowStyle VerticalAlign="Middle" />
                                                        <AlternatingRowStyle CssClass="alt-table-data" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Id" Visible="false" />
                                                            <asp:BoundField DataField="Sender" HeaderText="Sender" SortExpression="Sender" />
                                                            <asp:BoundField DataField="TextMessage" HeaderText="Text Message" SortExpression="TextMessage" />
                                                            <asp:BoundField DataField="AddedOn" HeaderText="Added On" SortExpression="AddedOn" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
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
                                </div>
                            </div>
                            <%} %>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
