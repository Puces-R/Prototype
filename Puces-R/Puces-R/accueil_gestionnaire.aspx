<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="accueil_gestionnaire.aspx.cs" Inherits="Puces_R.accueil_gestionnaire" Title="Accueil gestionnaire" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table runat="server">
        <asp:TableRow  >
            <asp:TableCell CssClass="titre_tab">
                Gérer les demandes des vendeurs
            </asp:TableCell>
            <asp:TableCell CssClass="space_cell">&nbsp;</asp:TableCell>
            <asp:TableCell CssClass="titre_tab">
                Gérer l'inactivité des vendeurs
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="cont_tab">
                &nbsp;<br /><br /><br /><br />
            </asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell CssClass="cont_tab">
                &nbsp;<br /><br /><br /><br />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>&nbsp;</asp:TableCell><asp:TableCell></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="titre_tab">
                Gérer l'inactivité des clients
            </asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell CssClass="titre_tab">
                Rapports & Statistiques
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="cont_tab">
                &nbsp;<br /><br /><br /><br />
            </asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell CssClass="cont_tab">
                &nbsp;<br /><br /><br /><br />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

</asp:Content>
