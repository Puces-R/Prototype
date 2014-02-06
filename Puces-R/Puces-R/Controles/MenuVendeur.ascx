<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuVendeur.ascx.cs" Inherits="Puces_R.Controles.MenuVendeur" %>

<asp:Menu runat="server" Orientation="Horizontal" ID="ctrMenu">
    <StaticMenuItemStyle HorizontalPadding="10" />
    <StaticSelectedStyle ForeColor="#6AC331" />
    <StaticHoverStyle ForeColor="#6AC331" />
    <DynamicSelectedStyle ForeColor="#6AC331" />
    <DynamicHoverStyle ForeColor="#6AC331" />
    <DynamicMenuStyle CssClass="popupMenu" />
    <Items>
        <asp:MenuItem Text="Accueil" NavigateUrl="../AcceuilVendeur.aspx"/>
        <asp:MenuItem Text="Nettoyer les paniers" NavigateUrl="../GererPanierVendeur.aspx"/>
        <asp:MenuItem Text="Commandes" NavigateUrl="../GestionCommandesVendeur.aspx"/>
        <asp:MenuItem Text="Produits" NavigateUrl="../GestionProduits.aspx"/>
        <asp:MenuItem Text="Modifier le profil" NavigateUrl="../ProfilVendeur.aspx"/>
    </Items>
</asp:Menu>