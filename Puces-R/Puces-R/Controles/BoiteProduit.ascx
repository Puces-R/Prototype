<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoiteProduit.ascx.cs" Inherits="Puces_R.Controles.BoiteProduit" %>

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
        <asp:Label runat="server" ID="lblEvaluation" />
        <asp:Label runat="server" ID="lblQuantite" />
    </div>
    <asp:PlaceHolder runat="server" ID="phBoutonsActions" Visible="false">
        <asp:Button ID="btnSupprimer" runat="server" Text="Supprimer" CommandName="Supprimer" CssClass="boutonActionProduit" />
        <asp:Button ID="btnModifier" runat="server" Text="Modifier" CommandName="Modifier" CssClass="boutonActionProduit" />
    </asp:PlaceHolder>
</div>