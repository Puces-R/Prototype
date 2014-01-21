<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produits.aspx.cs" Inherits="Puces_R.Produits" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Produits</title>
    <link rel="stylesheet" type="text/css" href="CSS/Produits.css" />
</head>
<body>
    <form runat="server">
        <ASP:DataList id="dtlProduits" RepeatColumns="5" RepeatDirection="Horizontal" runat="server" OnItemDataBound="dtlProduits_ItemDataBound">
            <ItemTemplate>
                <div class="productRectangle">
                    <asp:Label runat="server" ID="lblNoProduit" />
                    <asp:Image runat="server" ID="imgProduit" />
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
