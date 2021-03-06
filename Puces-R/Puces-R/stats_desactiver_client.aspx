﻿<%@ Page Title="Statistiques sur la désactivation des clients" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="stats_desactiver_client.aspx.cs" Inherits="Puces_R.stats_desactiver_client" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Site.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Produits.css" />
    <script type="text/javascript" src="lib/js/librairie.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">  
    <div>
    <div id="div_msg" runat="server"></div>
    <asp:MultiView runat="server" ID="mv_verdict" >
        <asp:View runat="server" ID="view_un_client">
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
                        <th>Adresse:</th>
                        <td><asp:Label runat="server" ID="addr_demande" /></td>
                    </tr>
                    <tr>
                        <th>Téléphone:</th>
                        <td><asp:Label runat="server" ID="tels_demande" /></td>
                    </tr>
                    <tr>
                        <th>Courriel:</th>
                        <td><asp:Label runat="server" ID="courriel_demande" /></td>
                    </tr>
                    <tr>
                        <th>Date d'inscription:</th>
                        <td><asp:Label runat="server" ID="date_demande" /></td>
                    </tr>    
                    <tr>
                        <th>Nombre de connexion:</th>
                        <td><asp:Label runat="server" ID="nb_connexions" /></td>
                    </tr>   
                    <tr>
                        <th>Nombre de liens Vendeur-Client:</th>
                        <td><asp:Label runat="server" ID="lbl_nb_lien" /></td>
                    </tr>                   
                    <tr>
                        <td colspan="2" class="verdict_client">                                        
                            <p class="center">
                                <asp:Button id="btn_voir_plus" runat="server" text="Voir plus" OnCommand="voir_plus" ToolTip="Cliquez pour voir les commandes de ce client" />
                            </p>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
        <asp:View runat="server" ID="view_liste">
            <div class="rectangleItem hautRectangle" style="width: 500px;" >
                Liste des clients désactivés
            </div>
            <div class="rectangleItem basRectangle" >
           <ul>            
                <asp:Repeater runat="server" ID="rptInnactifs1" OnItemDataBound="rptInnactifs1_ItemDataBound" >
                    <ItemTemplate>                        
                        <li><asp:LinkButton ID="lbl_client" runat="server" OnCommand="voir_plus" ToolTip="Cliquez pour voir les commandes de ce client" /></li>
                    </ItemTemplate>
                </asp:Repeater>
           </ul>
           </div>
        </asp:View>

        <asp:View  runat="server" ID="view_commandes">
            
        </asp:View>
    </asp:MultiView>
    </div>
</asp:Content>
