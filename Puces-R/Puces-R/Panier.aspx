<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Panier.aspx.cs" Inherits="Puces_R.Panier" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Panier</title>
    <link rel="stylesheet" type="text/css" href="CSS/Site.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Panier.css" />
</head>
<body>
    <form runat="server">
        <div id="pnlProduits">
            <div>
                <div class="pnlGauche">
                    <asp:Repeater runat="server" ID="rptProduits" OnItemDataBound="rptProduits_ItemDataBound">
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
                                        <span>
                                            Quantité: <asp:TextBox runat="server" ID="txtQuantite" Width="30"/>
                                            <asp:Button runat="server" ID="btnMAJQuantite" Text="Changer" OnClick="btnMAJQuantite_OnClick" />
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="pnlDroite">
                    <table>
                        <tr>
                            <td>Poids Total: </td>
                            <td><asp:Label ID="lblPoidsTotal" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Sous-Total: </td>
                            <td><asp:Label ID="lblSousTotal" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Livraison: </td>
                            <td><asp:Label ID="lblLivraison" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>TPS <asp:Label ID="lblTauxTPS" class="tauxTaxes" runat="server" />: </td>
                            <td><asp:Label ID="lblTPS" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>TVQ <asp:Label ID="lblTauxTVQ" class="tauxTaxes" runat="server" />: </td>
                            <td><asp:Label ID="lblTVQ" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Grand-Total: </td>
                            <td><asp:Label ID="lblGrandTotal" runat="server" CssClass="grandTotal" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>