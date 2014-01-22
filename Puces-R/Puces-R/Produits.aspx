<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produits.aspx.cs" Inherits="Puces_R.Produits" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Produits</title>
    <link rel="stylesheet" type="text/css" href="CSS/Produits.css" />
</head>
<body>
    <form runat="server">
        <div class="barreListesDeroulantes">
            <span class="boiteListeDeroulante">
                Recherche:
                <asp:DropDownList ID="ddlTypeRecherche" runat="server">
                    <asp:ListItem Text="Date de parution" />
                    <asp:ListItem Text="Numéro" />
                    <asp:ListItem Text="Catégorie" />
                    <asp:ListItem Text="Description" />
                </asp:DropDownList>
                <asp:TextBox ID="txtCritereRecherche" runat="server" />
                <asp:Button runat="server" Text="Go" ID="btnRecherche" />
            </span>
            <span class="boiteListeDeroulante">
                Trier par:
                <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true">
                    <asp:ListItem Text="Numéro" />
                    <asp:ListItem Text="Catégorie" />
                    <asp:ListItem Text="Date de parution" />
                </asp:DropDownList>
            </span>
            <span class="boiteListeDeroulante">
                Par page:
                <asp:DropDownList ID="ddlParPage" runat="server" AutoPostBack="true">
                    <asp:ListItem Value="5" />
                    <asp:ListItem Value="10" />
                    <asp:ListItem Value="15" Selected="True" />
                    <asp:ListItem Value="20" />
                    <asp:ListItem Value="25" />
                    <asp:ListItem Value="50" />
                </asp:DropDownList>
            </span>
        </div>
        <ASP:DataList id="dtlProduits" RepeatColumns="5" RepeatDirection="Horizontal" runat="server" OnItemDataBound="dtlProduits_ItemDataBound">
            <ItemTemplate>
                <div class="rectangleProduits">
                    <asp:Label runat="server" ID="lblNoProduit" />
                    <div class="boiteImageProduit">
                        <div><asp:Image runat="server" ID="imgProduit" /></div>
                    </div>
                    <asp:Label runat="server" ID="lblDescriptionAbregee" />
                    <asp:Label runat="server" ID="lblCategorie" />
                    <asp:Label runat="server" ID="lblPrixDemande" />
                    <asp:Label runat="server" ID="lblQuantite" />
                </div>
            </ItemTemplate>
        </ASP:DataList>
    </form>
</body>
</html>
