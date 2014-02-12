<%@ Page Title="Détail/Confirmation de la désactivation des vendeurs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="verdict_desactiver.aspx.cs" Inherits="Puces_R.verdict_desactiver" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Site.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Produits.css" />
    <script type="text/javascript" src="lib/js/librairie.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">  
    <div>
    <asp:MultiView runat="server" ID="mv_verdict" >
        <asp:View runat="server" ID="view_un_vendeur">
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
                        <th>Poids maximal:</th>
                        <td><asp:Label runat="server" ID="charge_max_demande" /></td>
                    </tr>
                    <tr>
                        <th>Livraison gratuite:</th>
                        <td><asp:Label runat="server" ID="livraison_gratuite" /></td>
                    </tr>
                    <tr>
                        <th>Date de demande:</th>
                        <td><asp:Label runat="server" ID="date_demande" /></td>
                    </tr>                        
                    <tr>
                        <td colspan="2" class="verdict_vendeur">                                        
                            <p class="center">
                                <asp:Button id="btn_desactiver" runat="server" text="Désactiver" OnCommand="desactiver_un_vendeur"/>
                            </p>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
        <asp:View runat="server" ID="view_liste">
            <div class="rectangleItem hautRectangle" style="width: 500px;" >
                Liste des vendeurs à désactiver
            </div>
            <div class="rectangleItem basRectangle" >
           <ul>            
                <asp:Repeater runat="server" ID="rptInnactifs1" OnItemDataBound="rptInnactifs1_ItemDataBound" >
                    <ItemTemplate>                        
                        <li><asp:Label ID="lbl_num" runat="server" /> - <asp:Label ID="item_a_desactiver" runat="server" /></li>
                    </ItemTemplate>
                </asp:Repeater>
           </ul>
               <p class="center">
                    <asp:Button id="btn_desactiver_liste" runat="server" text="Désactiver ces vendeurs" OnCommand="desactiver_liste_vendeur"/>
                </p>
           </div>
        </asp:View>
    </asp:MultiView>
    </div>
</asp:Content>
