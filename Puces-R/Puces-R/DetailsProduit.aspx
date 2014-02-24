<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DetailsProduit.aspx.cs" Inherits="Puces_R.DetailsProduit" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="lp" TagName="Etoiles" Src="~/Controles/Etoiles.ascx" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CSS/DetailsProduit.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="panneau pnlGauche">
            <h2>
                Photo</h2>
            <div class="boiteImageProduit">
                <div>
                    <asp:Image runat="server" ID="imgProduit" />
                </div>
            </div>
        </div>
        <div class="panneau pnlDroite pnlDetails">
            <h2>
                Description</h2>
            <table>
                <tr>
                    <td>
                        Produit:
                    </td>
                    <td>
                        <asp:Label ID="lblProduit" runat="server" CssClass="description" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Catégorie:
                    </td>
                    <td>
                        <asp:Label ID="lblCategorie" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Description:
                    </td>
                    <td>
                        <asp:Label ID="lblDescription" runat="server" CssClass="description" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Prix demandé:
                    </td>
                    <td>
                        <asp:Label ID="lblPrixDemande" runat="server" />
                    </td>
                </tr>
                <tr runat="server" id="trVente">
                    <td>
                        Prix en vente:
                    </td>
                    <td>
                        <asp:Label ID="lblPrixEnVente" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Disponible:
                    </td>
                    <td>
                        <asp:Label ID="lblQuantiteDisponible" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Création:
                    </td>
                    <td>
                        <asp:Label ID="lblDateCreation" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Mise à jour:
                    </td>
                    <td>
                        <asp:Label ID="lblDateMiseAJour" runat="server" />
                    </td>
                </tr>
                <tr runat="server" id="trQtt">
                    <td>
                        Quantité:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtQuantite" CssClass="boiteQuantite" Text="1" />
                        <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="reqQuantite" Text="Le champ est obligatoire"
                            ControlToValidate="txtQuantite" CssClass="erreur" />
                        <asp:CustomValidator runat="server" Display="Dynamic" ID="valQuantite" Text="Quantité disponible dépassée!"
                            ControlToValidate="txtQuantite" OnServerValidate="valQuantite_OnServerValidate"
                            CssClass="erreur" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:Button runat="server" ID="btnAjouterPanier" Text="Ajouter au panier" OnClick="btnAjouterPanier_Click" />
                        <asp:Button runat="server" ID="btnEnvoyerMessage" Text="Contacter le vendeur" OnClick="btnEnvoyerMessage_Click"
                            CausesValidation="false" />
                        <asp:Button runat="server" ID="btnModifierProduit" Text="Modifier le produit" OnClick="btnModifierProduit_Click"
                            CausesValidation="false" />
                        <asp:Button runat="server" ID="btnSupprimerProduit" Text="Supprimer le produit" OnClick="btnSupprimerProduit_Click"
                            CausesValidation="false" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="panneau pnlDroite pnlDetails">
            <h2>
                Évaluations (<asp:Literal ID="litNbEvaluations" runat="server" />)</h2>
            <asp:MultiView runat="server" ID="mvMoyenneOuMessage">
                <asp:View ID="View1" runat="server">
                    Moyenne:
                    <asp:Label ID="lblCoteMoyenne" runat="server" CssClass="coteMoyenne" />
                </asp:View>
                <asp:View ID="View2" runat="server">
                    Ce produit n'a pas encore été évalué.
                </asp:View>
            </asp:MultiView>
            <asp:Panel runat="server" CssClass="evaluation" ID="pnlEvaluation" Visible="false">
                <div class="rectangleItem hautRectangle">
                    <asp:Label runat="server" ID="lblClient" />
                    <lp:Etoiles runat="server" ID="ctrEtoiles" Cote="0" Modifiable="true" />
                </div>
                <div class="rectangleItem basRectangle">
                    <asp:TextBox runat="server" ID="txtCommentaire" Rows="3" TextMode="MultiLine" CssClass="commentaireEvaluation"
                        Font-Size="Small" />
                    <asp:Label runat="server" ID="lblErreurCommentaire" CssClass="erreur" />
                    <asp:Button runat="server" ID="btnSoumettre" Text="Soumettre" OnClick="btnSoumettre_OnClick"
                        CssClass="boutonSoumettre" />
                </div>
            </asp:Panel>
            <asp:Repeater runat="server" ID="rptEvaluations" OnItemDataBound="rptEvaluations_OnItemDataBound">
                <ItemTemplate>
                    <div class="evaluation">
                        <div class="rectangleItem hautRectangle">
                            <asp:Label runat="server" ID="lblClient" />
                            <lp:Etoiles runat="server" ID="ctrEtoiles" Modifiable="false" />
                        </div>
                        <div class="rectangleItem basRectangle">
                            <asp:Label runat="server" ID="lblCommentaire" Font-Size="Small" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Button runat="server" Text="Ajouter la mienne" ID="btnAjouterLaMienne" OnClick="btnAjouterLaMienne_OnClick"
                CssClass="boutonAjouterLaMienne" />
        </div>
    </div>
</asp:Content>
