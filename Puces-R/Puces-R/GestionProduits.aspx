<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionProduits.aspx.cs" Inherits="Puces_R.GestionProduits" %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuVendeur.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient ID="MenuClient1" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/Produits.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <div>
    <asp:Button ID="btnAjouter" runat="server" PostBackUrl="InsertionProduits.aspx" Text="Ajouter un produit " />
        <ASP:DataList id="dtlProduits" RepeatColumns="5" RepeatDirection="Horizontal" runat="server" OnItemDataBound="dtlProduits_ItemDataBound" OnItemCommand="dtlProduits_ItemCommand">
            <ItemTemplate>
                <div class="rectangleItem rectangleComplet">
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

                    <br />
                    <asp:Button ID="btnSupprimer" runat="server" Text="Supprimer"  CommandName="Supprimer"/>
                    <asp:Button ID="btnModifier" runat="server" Text="Modifier" CommandName="Modifier"/>
                    
                </div>
            </ItemTemplate>
        </ASP:DataList>
    </div>

</asp:Content>