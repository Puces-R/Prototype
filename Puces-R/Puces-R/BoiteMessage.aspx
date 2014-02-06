<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoiteMessage.aspx.cs" Inherits="Puces_R.BoiteMessage"
    MasterPageFile="~/Site.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        .sBoite
        {
            border-collapse: collapse;
            table-layout: fixed;
            width: 0;
        }
        .sBoite td, .sBoite th
        {
            height: 30px;
            text-align: left;
        }
        
        .sBoite td
        {
            border-top: 1px solid gray;
        }
        
        .sBoite .sLigneMessage:hover
        {
            background-color: white;
        }
        
        .sBoite .sLigneMessage a:hover
        {
            text-decoration: underline;
        }
        
        .sBoite .sCheckbox
        {
            width: 30px;
        }
        
        .sBoite .sDe
        {
            width: 250px;
        }
        
        .sBoite .sSujet
        {
            width: 600px;
            overflow: hidden;
        }
        
        .sBoite .sDate
        {
            width: 200px;
        }
        
        .sMenuBoite
        {
            font-size: 0.75em;
        }
    </style>
    <div>
        <div>
            <div style="float: left;">
                <asp:Menu ID="menuAction" runat="server" Orientation="Horizontal" CssClass="sMenuBoite"
                    OnMenuItemClick="clickOption">
                    <StaticMenuItemStyle HorizontalPadding="10" />
                </asp:Menu>
            </div>
            <div style="float: right;">
                <asp:DropDownList runat="server" ID="ddlBoite" OnSelectedIndexChanged="changeBoite"
                    AutoPostBack="true">
                    <asp:ListItem Text="Boîte principale" Value="1" />
                    <asp:ListItem Text="Archive" Value="2" />
                    <asp:ListItem Text="Corbeille" Value="3" />
                    <asp:ListItem Text="Envoyé" Value="-1" />
                    <asp:ListItem Text="Brouillon" Value="-2" />
                </asp:DropDownList>
            </div>
        </div>
        <div runat="server" class="rectangleComplet rectangleItem" style="float: left;">
            <table class="sBoite">
                <thead>
                    <tr>
                        <th class="sCheckbox">
                            <asp:CheckBox runat="server" ID="cbAll" OnClick="checkAll(this)" />
                        </th>
                        <th class="sDe">
                            <asp:LinkButton runat="server" ID="linkDe" OnClick="ordre" Text="De" />
                        </th>
                        <th class="sSujet">
                            <asp:LinkButton runat="server" ID="linkSujet" OnClick="ordre" Text="Sujet" />
                        </th>
                        <th class="sDate">
                            <asp:LinkButton runat="server" ID="linkDate" OnClick="ordre" Text="Date" />
                        </th>
                    </tr>
                </thead>
                <tbody runat="server" ID="ListeMessage">
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
