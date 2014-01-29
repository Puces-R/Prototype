<%@ Page Title="Statistiques & Rapports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="visualiser_stats_rapports.aspx.cs" Inherits="Puces_R.visualiser_stats_rapports" %>

<%@ Register TagPrefix="lp" TagName="MenuGestionnaire" Src="~/Controles/MenuGestionnaire.ascx" %>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuGestionnaire ID="MenuGestionnaire1" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/site.css" />
    <script type="text/javascript" src="lib/js/librairie.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
    <h2>Articles en panier</h2>
    <div class="rectangleStylise rectangleProduits">
        <div class="boiteImageProduit">
            <div>
                <asp:Image runat="server" ID="imgProduit" />
            </div>
        </div>
        <div class="boiteDetailsProduit">
            <div>
                <asp:Label runat="server" ID="lblNoProduit" />
                <asp:Label runat="server" ID="lblDescriptionAbregee" />
                <asp:Label runat="server" ID="lblCategorie" />
                <asp:Label runat="server" ID="lblPrixDemande" />
                <div>
                    Quantité: <asp:TextBox runat="server" ID="txtQuantite" CssClass="boiteQuantite" />
                    <asp:Button runat="server" ID="btnMAJQuantite" Text="Changer" CommandName="MAJQuantite" />
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
