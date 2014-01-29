<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionCommandesVendeur.aspx.cs" Inherits="Puces_R.GestionCommandesVendeur"  %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuVendeur.ascx" %>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient ID="MenuClient1" runat="server" />
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="CSS/GestionCommandesVendeur.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
    <div>
   <asp:Repeater runat="server" ID="rptProduits" OnItemDataBound="rptProduits_ItemDataBound" OnItemCommand="rptProduits_ItemCommand">
            <ItemTemplate>
                <div class="rectangleStylise rectangleProduits">
                   
                    <div class="boiteDetailsProduit">
                        <div>

                            <asp:Label runat="server" ID="lblNoCommande" />
                            <asp:Label runat="server" ID="lblNoClient" />
                            <asp:Label runat="server" ID="lblCategorie" />
                            <asp:Label runat="server" ID="lblnoVendeur" />
                            <asp:Label runat="server" ID="lblDateCommande" />
                            <asp:Label runat="server" ID="lblTypeLivraison" />
                            <asp:Label runat="server" ID="lblMontantTotal" />
                            <asp:Label runat="server" ID="lblTPS" />
                            <asp:Label runat="server" ID="lblTVQ" />
                            <asp:Label runat="server" ID="lblPoidsTotal" />
                            <asp:Label runat="server" ID="lblStatut" />
                            <asp:Label runat="server" ID="lblNoAutorisation" />

                             <asp:Button runat="server" ID="btnMAJQuantite" Text="Changer le statut de la commande" CommandName="MAJQuantite" />
                            
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

                   <%-- <div class="boiteDetailsProduit">
                        <div>
                            <asp:Label runat="server" ID="lblNoCommande" />
                            <asp:Label runat="server" ID="lblNoClient" />
                            <asp:Label runat="server" ID="lblCategorie" />
                            <asp:Label runat="server" ID="lblnoVendeur" />
                            <asp:Label runat="server" ID="lblDateCommande" />
                            <asp:Label runat="server" ID="lblTypeLivraison" />
                            <asp:Label runat="server" ID="lblMontantTotal" />
                            <asp:Label runat="server" ID="lblTPS" />
                            <asp:Label runat="server" ID="lblTVQ" />
                            <asp:Label runat="server" ID="lblPoidsTotal" />
                            <asp:Label runat="server" ID="lblStatut" />
                            <asp:Label runat="server" ID="lblNoAutorisation" />

                             <asp:Button runat="server" ID="btnMAJQuantite" Text="Changer" CommandName="MAJQuantite" />
                            
                        </div>
                    </div>--%>
          

    </div>
    </asp:Content>
