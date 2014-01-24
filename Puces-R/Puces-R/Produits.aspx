<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produits.aspx.cs" Inherits="Puces_R.Produits" MasterPageFile="~/Site.Master"  %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuClient.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient runat="server" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <link href="CSS/Produits.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="lignePointilleHorizontale barreListesDeroulantes">
        <span class="boiteListeDeroulante">
            Recherche:
            <asp:DropDownList ID="ddlTypeRecherche" runat="server">
                <asp:ListItem Text="Date de parution" />
                <asp:ListItem Text="Numéro" />
                <asp:ListItem Text="Catégorie" />
                <asp:ListItem Text="Description" />
            </asp:DropDownList>
            <asp:TextBox ID="txtCritereRecherche" runat="server" />
            <asp:Button runat="server" Text="Go" ID="btnRecherche" />
        </span>
        <span class="boiteListeDeroulante">
            Trier par:
            <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true">
                <asp:ListItem Text="Numéro" />
                <asp:ListItem Text="Catégorie" />
                <asp:ListItem Text="Date de parution" />
            </asp:DropDownList>
        </span>
        <span class="boiteListeDeroulante">
            Par page:
            <asp:DropDownList ID="ddlParPage" runat="server" AutoPostBack="true">
                <asp:ListItem Value="5" />
                <asp:ListItem Value="10" />
                <asp:ListItem Value="15" Selected="True" />
                <asp:ListItem Value="20" />
                <asp:ListItem Value="25" />
                <asp:ListItem Value="50" />
            </asp:DropDownList>
        </span>
    </div>
    <ASP:DataList id="dtlProduits" RepeatColumns="5" RepeatDirection="Horizontal" runat="server" OnItemDataBound="dtlProduits_ItemDataBound">
        <ItemTemplate>
            <div class="rectangleStylise rectangleProduits">
                <asp:Label runat="server" ID="lblNoProduit" />
                <div class="boiteImageProduit">
                    <div>
                        <asp:HyperLink runat="server" ID="hypProduit" />
                    </div>
                </div>
                <asp:Label runat="server" ID="lblDescriptionAbregee" />
                <asp:Label runat="server" ID="lblCategorie" />
                <asp:Label runat="server" ID="lblPrixDemande" />
                <asp:Label runat="server" ID="lblQuantite" />
            </div>
        </ItemTemplate>
    </ASP:DataList>
</asp:Content>