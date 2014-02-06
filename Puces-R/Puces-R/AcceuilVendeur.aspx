<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AcceuilVendeur.aspx.cs" Inherits="Puces_R.AcceuilVendeur" %>

<%@ Register TagPrefix="lp" TagName="PanierProduits" Src="~/Controles/TablePanier.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/AccueilClient.css" />
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">




<div class="panneau ">
<asp:Label ID="nbVisite" runat="server" Text="Nombre de visites sur votre catalogue " ></asp:Label>
    <div class="panneau pnlGauche">
        <h2>Paniers en Cours</h2>

               
        <ASP:Repeater id="rptPaniers" runat="server" OnItemDataBound="rptPaniers_ItemDataBound">
            <ItemTemplate>
                <div class="  boiteDetailsProduit rectangleComplet rectangleItem">

                

                    <asp:HyperLink runat="server" ID="hypVendeur" CssClass="titreRectangle" /><br />
               
               <table class="tableProduits">
            <tr>
                <th>Nom du Client</th>
                <th>Date de mise à jour</th>
                
            </tr>
                  <tr><td><asp:Label Id="lblNom" runat="server"></asp:Label></td>
                   <td> <asp:Label Id="lblDateMAJ" runat="server"></asp:Label></td></tr>  <br />
<tr>
                <th>No client</th>
                <th>Prix total</th>
</tr>
                   <tr> <td><asp:Label ID="lblNumero" runat="server"></asp:Label></td>
                    <td>Sous-Total: <asp:Label runat="server" ID="lblSousTotal" /></td></tr>
                    </table>
                   
                  
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:HyperLink ID="hplPanier" runat="server" Text="Voir plus..." CssClass="catalogueGlobal" ></asp:HyperLink>
        </div>

         <div class="panneau pnlDroite">
         <h2>Commandes non traitées </h2>
         <asp:Repeater runat="server" ID="rptCommandes" OnItemDataBound="rptCommandes_ItemDataBound" OnItemCommand="rptCommandes_ItemCommand">
            <ItemTemplate>
                <div class="rectangleItem rectangleComplet">
                   
                    <div class="boiteDetailsProduit">
                        <div>

        <table class="tableProduits">

            <tr>
                <th>No Commande</th>
                <th>Numéro Client</th>
                <th>Date de commande</th>
                <th>Frais de livraison</th>
                <th>Montant total</th>
                <th>Frais TPS</th>
                <th>Frais TVQ</th>
                <th>Poids de la Commande</th>
                <th>Statut</th>
            </tr>
            <tr>
                          <td> <asp:HyperLink runat="server" ID="hypCommande"/></td>
                             <td> <asp:Label runat="server" ID="lblNoClient" /></td>
                             <td> <asp:Label runat="server" ID="lblDateCommande" /></td>
                             <td> <asp:Label runat="server" ID="lblTypeLivraison" /></td>
                             <td> <asp:Label runat="server" ID="lblMontantTotal" /></td>
                            <td>  <asp:Label runat="server" ID="lblTPS" /></td>
                          <td>  <asp:Label runat="server" ID="lblTVQ" /></td>
                           <td>  <asp:Label runat="server" ID="lblPoidsTotal" /></td>
                          <td>  <asp:Label runat="server" ID="lblStatut" /></td>
                           
           </tr>

        </table>   
                            
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:HyperLink ID="hplToutesCommandes" runat="server" Text="Voir plus..." CssClass="catalogueGlobal" ></asp:HyperLink>
        </div>
    </div>
 </asp:Content>
