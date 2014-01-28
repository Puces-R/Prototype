﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Panier.aspx.cs" Inherits="Puces_R.Panier" MasterPageFile="Site.Master" %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuClient.ascx" %>
<%@ Register TagPrefix="lp" TagName="MontantsFactures" Src="~/Controles/MontantsFactures.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient runat="server" ID="ctrMenu" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <link rel="stylesheet" type="text/css" href="CSS/Panier.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <div class="panneau pnlGauche">
            <h2>Articles en panier</h2>
            <asp:Repeater runat="server" ID="rptProduits" OnItemDataBound="rptProduits_ItemDataBound" OnItemCommand="rptProduits_ItemCommand">
                <ItemTemplate>
                    <div class="rectangleStylise rectangleProduits">
                        <div class="boiteImageProduit">
                            <div>
                                <asp:Image runat="server" ID="imgProduit" />
                            </div>
                        </div>
                        <div class="boiteDetailsProduit">
                            <div>
                                <asp:Label runat="server" ID="lblNoProduit" />
                                <asp:Label runat="server" ID="lblDescriptionAbregee" />
                                <asp:Label runat="server" ID="lblCategorie" />
                                <asp:Label runat="server" ID="lblPrixDemande" />
                                <div>
                                    Quantité: <asp:TextBox runat="server" ID="txtQuantite" CssClass="boiteQuantite" />
                                    <asp:Button runat="server" ID="btnMAJQuantite" Text="Changer" CommandName="MAJQuantite" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="panneau pnlDroite pnlDetails">
            <lp:MontantsFactures runat="server" ID="ctrMontantsFactures" />
            <asp:Button runat="server" Text="Commander" ID="btnCommander" OnClick="btnCommander_OnClick" />
        </div>
    </div>
</asp:Content>