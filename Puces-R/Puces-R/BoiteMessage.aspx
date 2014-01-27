<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoiteMessage.aspx.cs" Inherits="Puces_R.BoiteMessage"
    MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="yc" TagName="MenuInvite" Src="~/Controles/MenuInvite.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <yc:MenuInvite runat="server" />
</asp:Content>
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
            border-bottom: 1px solid gray;
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
        <div style="float: left;">
            <asp:Menu runat="server" Orientation="Horizontal" OnMenuItemClick="clickOption" CssClass="sMenuBoite">
                <StaticMenuItemStyle HorizontalPadding="10" />
                <Items>
                    <asp:MenuItem Text="Nouveau message" Value="New" />
                    <asp:MenuItem Text="Marquer comme lu" Value="Lu" />
                    <asp:MenuItem Text="Marquer comme non-lu" Value="Non-lu" />
                    <asp:MenuItem Text="Archiver" Value="Archiver" />
                    <asp:MenuItem Text="Supprimer" Value="Supprimer" />
                </Items>
            </asp:Menu>
        </div>
        <div style="float: right;">
            <asp:Menu runat="server" Orientation="Horizontal" OnMenuItemClick="voirMessage" CssClass="sMenuBoite">
                <StaticMenuItemStyle HorizontalPadding="10" />
                <Items>
                <asp:MenuItem Text="Boîte principale" Value="Box" />
                    <asp:MenuItem Text="Archivé" Value="Archived" />
                    <asp:MenuItem Text="Corbeil" Value="Deleted" />
                    <asp:MenuItem Text="Envoyé" Value="Sent" />
                    <asp:MenuItem Text="Brouillon" Value="Draft" />
                </Items>
            </asp:Menu>
        </div>
    </div>
    <div runat="server" ID="divMessages" class="rectangleStylise" style="float: left;">
        <table class="sBoite">
            <thead>
                <tr>
                    <th class="sCheckbox">
                    </th>
                    <th class="sDe">
                        De
                    </th>
                    <th class="sSujet">
                        Sujet
                    </th>
                    <th class="sDate">
                        Date
                    </th>
                </tr>
            </thead>
            <tbody runat="server" id="ListeMessage">
            </tbody>
        </table>
    </div>
    <div runat="server" id="divEnvoyes" class="rectangleStylise" style="float: left;" Visible="false">
        <table class="sBoite">
            <thead>
                <tr>
                    <th class="sCheckbox">
                    </th>
                    <th class="sDe">
                        À
                    </th>
                    <th class="sSujet">
                        Sujet
                    </th>
                    <th class="sDate">
                        Date
                    </th>
                </tr>
            </thead>
            <tbody runat="server" id="ListeEnvoye">
            </tbody>
        </table>
    </div>
</asp:Content>
