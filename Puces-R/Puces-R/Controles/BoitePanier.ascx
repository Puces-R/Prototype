<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoitePanier.ascx.cs" Inherits="Puces_R.Controles.BoitePanier" %>

<%@ Register TagPrefix="lp" TagName="TablePanier" Src="~/Controles/TablePanier.ascx" %>

<div class="rectangleItem hautRectangle">
    <asp:HyperLink runat="server" ID="hypTitre" />
</div>
<div class="rectangleItem basRectangle">
    <lp:TablePanier runat="server" ID="ctrProduits" />
    <div class="sousTotal">
        Sous-Total: <asp:Label runat="server" ID="lblSousTotal" />
    </div>
</div>