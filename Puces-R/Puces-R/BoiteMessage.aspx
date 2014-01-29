<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoiteMessage.aspx.cs" Inherits="Puces_R.BoiteMessage"
    MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="yc" TagName="Boite" Src="~/Controles/BoiteMessage.ascx" %>
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
    <script type="text/javascript">
        function checkAll(checkbox) {
            var idCB = checkbox.id;
            var idSeparate = idCB.split('_');
            idSeparate.pop();
            var id = idSeparate.join('_');

            for (var i = 0; i < document.getElementById(id + '_Liste').childNodes.length; i++) {
                var cb = document.getElementById(id + '_ctl' + (i < 10 ? '0' : '') + i + '_cb');
                if (cb != null) {
                    cb.checked = checkbox.checked;
                }
            }
        }
    </script>
    <div>
        <div>
            <div style="float: left;">
                <asp:Menu ID="menuAction" runat="server" Orientation="Horizontal" OnMenuItemClick="clickOption"
                    CssClass="sMenuBoite">
                    <StaticMenuItemStyle HorizontalPadding="10" />
                    <Items>
                        <asp:MenuItem Text="Nouveau message" Value="New" />
                        <asp:MenuItem Text="Marquer comme lu" Value="Read" />
                        <asp:MenuItem Text="Marquer comme non-lu" Value="Unread" />
                        <asp:MenuItem Text="Archiver" Value="Archive" />
                        <asp:MenuItem Text="Supprimer" Value="Delete" />
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
        <yc:Boite runat="server" ID="ListeMessage" Visible="true" />
    </div>
</asp:Content>
