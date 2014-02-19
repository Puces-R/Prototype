﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetailsCommandes.aspx.cs" Inherits="Puces_R.DetailsCommandes" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<%@ Register TagPrefix="se" TagName="BoiteDetails" Src="~/Controles/TableDetailsCommande.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/AccueilClient.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<div class="boiteDetailsProduit">
                        <div>

                        <table>

                            <tr><td><asp:Label runat="server" ID="lblNoCommande" Text="No Commande"/></td>
                            <td><asp:Label runat="server" ID="tbNoCommande" enabled="false"/></td></tr>

                            <tr><td><asp:Label runat="server" ID="lblNoClient" Text="No Client"/></td>
                            <td><asp:Label runat="server" ID="tbNoClient" enabled="false"/></td></tr>

                             <tr><td><asp:Label runat="server" ID="lblVendeur" Text="No Vendeur"/></td>
                            <td><asp:Label runat="server" ID="tbNoVendeur" enabled="false"/></td></tr>

                            <tr><td><asp:Label runat="server" ID="lblDate" Text="Date de la Commande"/></td>
                            <td><asp:Label runat="server" ID="tbDate" enabled="false"/></td></tr>

                             <tr><td><asp:Label runat="server" ID="Label1" Text="Total de la livraison"/></td>
                            <td><asp:Label runat="server" ID="tbLivraisonM" enabled="false"/></td></tr>

                            <tr><td><asp:Label runat="server" ID="LblLivraison" Text="Type de livraison"/></td>
                            <td><asp:Label runat="server" ID="tbLivraisonType" enabled="false"/></td></tr>

                            <tr><td><asp:Label runat="server" ID="lblMontant" Text="Montant total de la commande"/></td>
                            <td><asp:Label runat="server" ID="tbMontant" enabled="false"/></td></tr>


                            <tr><td><asp:Label runat="server" ID="lblTPS" Text="TPS"/></td>
                            <td><asp:Label runat="server" ID="TbTps" enabled="false"/></td></tr>

                            <tr><td><asp:Label runat="server" ID="lblTVQ" Text="TVQ"/></td>
                            <td><asp:Label runat="server" ID="tbTvq" enabled="false"/></td></tr>

                            <tr><td><asp:Label runat="server" ID="lblPoids" Text="Poids total"/></td>
                            <td><asp:Label runat="server" ID="tbPoids" enabled="false"/></td></tr>

                             <tr><td><asp:Label runat="server" ID="lblStatut" Text="Statut Actuel"/></td>
                            <td><asp:Label runat="server" ID="tbStatut" enabled="false"/></td></tr>

                            <tr><td><asp:Label runat="server" ID="lblAutorisation" Text="No Autorisation"/></td>
                            <td><asp:Label runat="server" ID="tbNoAutorisation" enabled="false"/></td></tr>

                          </table>  
                            
                            <asp:Button ID="btnChangerStatut" runat="server" Text="Changer le staut de la commande" />  <asp:Button ID="btnRetour" runat="server" Text=" Retour"/>
                        </div>
                    </div>
<div>
                     <se:BoiteDetails runat="server" ID="ctrBoitePanier" />
</div>

 </asp:Content>