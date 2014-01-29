<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuVendeur.ascx.cs" Inherits="Puces_R.Controles.MenuVendeur" %>

<asp:Menu runat="server" Orientation="Horizontal" ID="ctrMenu">
    <StaticMenuItemStyle HorizontalPadding="10" />
    <StaticSelectedStyle ForeColor="#6AC331" />
    <LevelSubMenuStyles>
        <asp:SubMenuStyle />
        <asp:SubMenuStyle CssClass="popupMenu" />
    </LevelSubMenuStyles>
    <Items>
        <asp:MenuItem Text="Accueil" NavigateUrl="../AcceuilVendeur.aspx"/>
        <asp:MenuItem Text="Nettoyer les paniers" NavigateUrl="../GererPanierVendeur.aspx"/>
        <asp:MenuItem Text="Gérer les commandes" NavigateUrl="../GestionCommandesVendeur.aspx"/>
        <asp:MenuItem Text="Gérer les produits" NavigateUrl="../GestionProduits.aspx"/>
        <asp:MenuItem Text="Modifier le profil" NavigateUrl="../ProfilVendeur.aspx"/>
        <asp:MenuItem Text="Déconnecter" NavigateUrl="../Connexion.aspx" />
    </Items>
</asp:Menu>