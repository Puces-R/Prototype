<%@ Page Title="Nouveaux produits" Language="C#" AutoEventWireup="true" CodeBehind="NouveauxProduits.aspx.cs" Inherits="Puces_R.Controles.NouveauxProduits" MasterPageFile="~/Site.Master" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<%@ Register TagPrefix="lp" TagName="BoiteProduit" Src="~/Controles/BoiteProduit.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <link href="CSS/Produits.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <asp:DataList id="dtlProduits" RepeatColumns="5" RepeatDirection="Horizontal" runat="server" OnItemDataBound="dtlProduits_ItemDataBound" >
            <ItemTemplate>
                <lp:BoiteProduit runat="server" ID="ctrProduit" LienActive="false" />
            </ItemTemplate>
        </asp:DataList>
    </div>
    <div class="lignePointilleHorizontale pleineLargeur"></div>
    <div>
        <p>Vous devez <asp:HyperLink runat="server" NavigateUrl="~/InscriptionClient.aspx">vous inscrire</asp:HyperLink> et <asp:HyperLink runat="server" NavigateUrl="~/Default.aspx">vous connecter</asp:HyperLink> pour voir tous les produits disponibles et commander</p>
    </div>
</asp:Content>