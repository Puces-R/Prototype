<%@ Title="Informations du client" Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CommuniquerClientPanier.aspx.cs" Inherits="Puces_R.CommuniquerClientPanier"  %>

<%@ Register TagPrefix="yc" TagName="CodePostal" Src="~/Controles/CodePostal.ascx" %>
<%@ Register TagPrefix="yc" TagName="Province" Src="~/Controles/Province.ascx" %>
<%@ Register TagPrefix="yc" TagName="Telephone" Src="~/Controles/Telephone.ascx" %>
<%@ Register TagPrefix="lp" TagName="ChangementMDP" Src="~/Controles/ChangementMDP.ascx" %>
<%@ Register TagPrefix="se" TagName="Adresse" Src="~/Controles/Adresse.ascx" %>
<%@ Register TagPrefix="lp" TagName="BoitePanier" Src="~/Controles/BoitePanier.ascx" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/InsertionProduits.css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="panneau pnlGauche">
 <lp:BoitePanier runat="server" ID="ctrBoitePanier" />

 </div>
 <div "panneau pnlDroite>
<div class="rectangleComplet rectangleItem">
       
<table class="formulaire" style="width: 700px;">
<tr>
    <td>
        Prénom
    </td>
    <td>
        <asp:TextBox ID="txtPrenom" runat="server" ReadOnly="true"/>
    </td>
    <td>
       
    </td>
</tr>
<tr>
    <td>
        Nom
    </td>
    <td>
        <asp:TextBox ID="txtNom" runat="server" ReadOnly="true"/>
    </td>
    <td>

    </td>
</tr>
<se:Adresse ID="txtRue" runat="server" Label="Rue" ReadOnly="true"/>
<tr>
    <td>
        Ville
    </td>
    <td>
        <asp:TextBox ID="txtVille" runat="server" ReadOnly="true"/>
    </td>
    <td>
       
    </td>
</tr>
<tr>
    <td>
        Province
    </td>
    <td>
        <yc:Province ID="ctrProvince" runat="server" ReadOnly="true"/>
    </td>
    <td>
    </td>
</tr>
<tr>
    <td>
        Pays
    </td>
    <td>
        <asp:TextBox ID="txtPays" runat="server" ReadOnly="true" />
    </td>
    <td>
    </td>
</tr>
<yc:CodePostal ID="ctrCodePostal" runat="server" Obligatoire="true" />
<yc:Telephone ID="ctrTelephone" runat="server" Obligatoire="true" />
<yc:Telephone ID="ctrCellulaire" Label="Cellulaire" Obligatoire="false" runat="server" />

</table>

<asp:Button ID="btnCourrierInterne" runat="server" Text="Envoyer un courriel interne" OnClick="changer" />
<asp:Button ID="btnCourrielExterne" runat="server" Text="Envoyer un courriel Externe" OnCommand="changer_view" CommandArgument="2"/>
    </div>
    </div>
</asp:Content>