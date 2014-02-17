<%@ Page Language="C#" MasterPageFile="~/NavigationItems.Master" AutoEventWireup="true" CodeBehind="GestionProduits.aspx.cs" Inherits="Puces_R.GestionProduits" %>

<%@ MasterType VirtualPath="~/NavigationItems.Master" %>

<%@ Register TagPrefix="lp" TagName="BoiteProduit" Src="~/Controles/BoiteProduit.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/Produits.css" />
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="BarreCriteres">
    <span class="boiteListeDeroulante">Recherche:
        <asp:DropDownList ID="ddlTypeRecherche" runat="server">
            <asp:ListItem Text="Date de parution" />
            <asp:ListItem Text="Numéro" />
            <asp:ListItem Text="Description" />
        </asp:DropDownList>
        <asp:TextBox ID="txtCritereRecherche" runat="server" />
        <asp:Button runat="server" Text="Go" ID="btnRecherche" OnClick="AfficherPremierePage" />
    </span>
    <span class="boiteListeDeroulante">Trier par:
        <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage">
            <asp:ListItem Text="Numéro" />
            <asp:ListItem Text="Catégorie" />
            <asp:ListItem Text="Date de parution" />
        </asp:DropDownList>
    </span>
    <span class="boiteListeDeroulante">Par page:
        <asp:DropDownList ID="ddlParPage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage">
            <asp:ListItem Value="5" />
            <asp:ListItem Value="10" />
            <asp:ListItem Value="15" Selected="True" />
            <asp:ListItem Value="20" />
            <asp:ListItem Value="25" />
            <asp:ListItem Value="50" />
        </asp:DropDownList>
    </span>
    <span class="boiteListeDeroulante">Catégorie:
        <asp:DropDownList ID="ddlCategorie" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" />
    </span>
    <span class="boiteListeDeroulante">
        <asp:Button ID="btnAjouter" runat="server" PostBackUrl="InsertionProduits.aspx" Text="Ajouter un produit " />
    </span>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Items" runat="server">
    <div>
        <asp:DataList ID="dtlProduits" RepeatColumns="5" RepeatDirection="Horizontal" runat="server" OnItemDataBound="dtlProduits_ItemDataBound" OnItemCommand="dtlProduits_ItemCommand">
            <ItemTemplate>
                <lp:BoiteProduit runat="server" ID="ctrProduit" LienActive="true" AfficherBoutonsActions="true" />
            </ItemTemplate>
        </asp:DataList>
    </div>
</asp:Content>
