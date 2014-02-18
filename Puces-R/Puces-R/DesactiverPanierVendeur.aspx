<%@ Page Title="Détail/Confirmation de la désactivation des paniers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="DesactiverPanierVendeur.aspx.cs" Inherits="Puces_R.DesactiverPanierVendeur" EnableEventValidation="false" %>

<%@ Register TagPrefix="lp" TagName="BoitePanier" Src="~/Controles/BoitePanier.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Site.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Produits.css" />
    <script type="text/javascript" src="lib/js/librairie.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">  
    <div>
    <asp:MultiView runat="server" ID="mv_verdict" ActiveViewIndex="1">
        <asp:View runat="server" ID="view_un_vendeur" >
            <lp:BoitePanier runat="server" ID="ctrBoitePanier" />
              <asp:Button id="btn_desactiver" runat="server" text="Désactiver" OnCommand="desactiver_un_Panier"/>
              <asp:HiddenField ID="hidNoClient" runat="server" />
        </asp:View>
        <asp:View runat="server" ID="view_liste">
            <div class="rectangleItem hautRectangle" style="width: 500px;" >
                Liste des panier(s) à supprimer
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
                    <asp:Button id="btn_desactiver_liste" runat="server" text="Supprimer ces paniers" OnCommand="desactiver_liste_vendeur"/>
                </p>
           </div>
        </asp:View>
    </asp:MultiView>
    </div>
</asp:Content>
