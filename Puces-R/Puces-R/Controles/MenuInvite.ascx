<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuInvite.ascx.cs" Inherits="Puces_R.Controles.MenuInvite" %>

<asp:Menu ID="ctrMenu" runat="server" Orientation="Horizontal">
    <StaticMenuItemStyle HorizontalPadding="10" />
    <StaticSelectedStyle ForeColor="#6AC331" />
    <LevelSubMenuStyles>
        <asp:SubMenuStyle />
        <asp:SubMenuStyle BackColor="White" BorderColor="Gray" BorderWidth="2" BorderStyle="Dashed" HorizontalPadding="10" />
    </LevelSubMenuStyles>
    <Items>
        <asp:MenuItem Text="Accueil" />
        <asp:MenuItem Text="S'inscrire..." Selectable="false">
            <asp:MenuItem Text="Client" NavigateUrl="~/InscriptionClient.aspx" />
            <asp:MenuItem Text="Vendeur" NavigateUrl="~/InscriptionVendeur.aspx" />
        </asp:MenuItem>
        <asp:MenuItem Text="Connexion" NavigateUrl="~/Connexion.aspx" />
        <asp:MenuItem Text="Mot de passe oublié ?" NavigateUrl="~/RecupererMotDePasse.aspx" />
    </Items>
</asp:Menu>