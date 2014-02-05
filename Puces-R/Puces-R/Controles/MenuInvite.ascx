<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuInvite.ascx.cs" Inherits="Puces_R.Controles.MenuInvite" %>

<asp:Menu ID="ctrMenu" runat="server" Orientation="Horizontal">
    <StaticMenuItemStyle HorizontalPadding="10" />
    <StaticSelectedStyle ForeColor="#6AC331" />
    <StaticHoverStyle ForeColor="#6AC331" />
    <DynamicSelectedStyle ForeColor="#6AC331" />
    <DynamicHoverStyle ForeColor="#6AC331" />
    <DynamicMenuStyle CssClass="popupMenu" />
    <Items>
        <asp:MenuItem Text="Accueil" NavigateUrl="~/Default.aspx" />
        <asp:MenuItem Text="Inscription" Selectable="false">
            <asp:MenuItem Text="Inscription d'un client" NavigateUrl="~/InscriptionClient.aspx" />
            <asp:MenuItem Text="Inscription d'un vendeur" NavigateUrl="~/InscriptionVendeur.aspx" />
        </asp:MenuItem>
        <asp:MenuItem Text="Mot de passe oublié ?" NavigateUrl="~/RecupererMotDePasse.aspx" />
    </Items>
</asp:Menu>