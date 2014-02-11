<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificationProduits.aspx.cs" Inherits="Puces_R.ModificationProduits" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuVendeur.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient ID="MenuClient1" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/AccueilClient.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<div class="boiteDetailsProduit">

<h2>Modification d'un produit </h2>

<table class="tableProfil">
    <tr>
        <td>
            Catégorie du produits:
        </td>
        <td>
            <asp:DropDownList ID="ddlCategorieProduits" runat="server" />
        </td>
        <td>
        </td>
    </tr>

    <tr>
        <td>
            Description abregée
        </td>
        <td>
            <asp:TextBox ID="tbDescAbregée" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Prix demandé
        </td>
        <td>
            <asp:TextBox ID="tbPrixDemande" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Description Complète :
        </td>
        <td>
            <asp:TextBox ID="tbComplete" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Photo
        </td>
        <td>
            <asp:TextBox ID="tbPhoto" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Date de création :
        </td>
        <td>
            <asp:TextBox ID="tbDateCreation" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Nombre d'Items
        </td>
        <td>
             <asp:TextBox ID="tbNbItems" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    
    <tr>
        <td>
            Prix de vente :
        </td>
        <td>
            <asp:TextBox ID="tbPrixVente" runat="server" />
        </td>
        <td>
        </td>
    </tr>
  
    <tr>
        <td>
            Date de Vente :
        </td>
        <td>
            <asp:TextBox ID="tbDateVente" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
           Poids de l'article :
        </td>
        <td>
            <asp:TextBox ID="tbPoids" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
           Disponibilité :
        </td>
        <td>
            <asp:CheckBox ID="cbDisponibilité" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    
    <tr>
        <td>
           Dernière mise à jour :
        </td>
        <td>
            <asp:Label ID="tbMAJ" runat="server" />
        </td>
        <td>
        </td>
    </tr>
</table>



<asp:Button ID="btnAjout" Text="Modifier le produit" runat="server" />

<asp:Button runat="server" ID="btnRetour" Text="Retour" CausesValidation="FALSE" PostBackUrl="GestionProduits.aspx" />

</div>


   
 </asp:Content>