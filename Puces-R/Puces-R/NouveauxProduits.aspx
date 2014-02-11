<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NouveauxProduits.aspx.cs" Inherits="Puces_R.Controles.NouveauxProduits" MasterPageFile="~/Site.Master" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <link href="CSS/Produits.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="lignePointilleHorizontale pleineLargeur">
        <asp:DataList id="dtlProduits" RepeatColumns="5" RepeatDirection="Horizontal" runat="server" OnItemDataBound="dtlProduits_ItemDataBound" >
            <ItemTemplate>
                <div class="rectangleProduits rectangleComplet rectangleItem">
                    <div class="titreRectangle">
                        <div>
                            <asp:HyperLink runat="server" ID="hypDescriptionAbregee" />
                        </div>
                    </div>
                    <div class="boiteImageProduit">
                        <div>
                            <asp:Image runat="server" ID="imgProduit" />
                        </div>
                    </div>
                    <div class="detailsProduit">
                        <asp:Label runat="server" ID="lblNoProduit" />
                        <asp:Label runat="server" ID="lblCategorie" />
                        <asp:Label runat="server" ID="lblPrixDemande" />
                        <asp:Label runat="server" ID="lblQuantite" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <div>
        <p>Vous devez <asp:HyperLink runat="server" NavigateUrl="~/InscriptionClient.aspx">vous inscrire</asp:HyperLink> et <asp:HyperLink runat="server" NavigateUrl="~/Default.aspx">vous connecter</asp:HyperLink> pour voir tous les produits disponibles et commander</p>
    </div>
</asp:Content>