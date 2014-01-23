<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demonstration.aspx.cs" Inherits="Puces_R.Demonstration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form runat="server">
    <ul>
        <li><a href="Produits.aspx">Tout les produits</a></li>
        <li><a href="Produits.aspx?novendeur=10">Produits d'un vendeur</a></li>
        <li><a href="Panier.aspx?noclient=10000">Panier d'un client</a></li>
        <li><a href="Panier.aspx?noclient=10400">Panier d'un autre client</a></li>
    </ul>
    </form>
</body>
</html>
