<%@ Page Title="Détail de la commande de la redevence" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="details_commande_redevance.aspx.cs" Inherits="Puces_R.details_commande_redevance" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Site.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Produits.css" />
    <script type="text/javascript" src="lib/js/librairie.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">  
    <div>
        <div class="rectangleItem hautRectangle" >
            <asp:Label runat="server" ID="titre_demande" />
        </div>
        <div class="rectangleItem basRectangle">
            <table class="tableTitreValeur">
                <colgroup>
                    <col width="50%" />
                    <col width="50%" />
                </colgroup>
                <tr >
                    <th>Vendeur :</th>
                    <td><asp:Label runat="server" ID="lbl_nom_vendeur" /></td>
                </tr>
                <tr>
                    <th>CLient:</th>
                    <td><asp:Label runat="server" ID="lbl_nom_client" /></td>
                </tr>
                <tr>
                    <th>Montant de vente:</th>
                    <td>$<asp:Label runat="server" ID="lbl_montant_vente" /></td>
                </tr>
                <tr>
                    <th>Redevence:</th>
                    <td>$<asp:Label runat="server" ID="lbl_redevance" /></td>
                </tr>
                <tr>
                    <th>TPS:</th>
                    <td>$<asp:Label runat="server" ID="lbl_tps" /></td>
                </tr>
                <tr>
                    <th>TVQ:</th>
                    <td>$<asp:Label runat="server" ID="lbl_tvq" /></td>
                </tr>                        
                <tr>
                    <th>Frais de livraison:</th>
                    <td>$<asp:Label runat="server" ID="lbl_frais_livraison" /></td>
                </tr> 
                <tr>
                    <th>Numéro d'autorisation:</th>
                    <td><asp:Label runat="server" ID="lbl_num_autorisation" /></td>
                </tr> 
                <tr>
                    <th>Frais LESi:</th>
                    <td>$<asp:Label runat="server" ID="lbl_frais_lesi" /></td>
                </tr> 
            </table>
        </div>
    </div>
</asp:Content>
