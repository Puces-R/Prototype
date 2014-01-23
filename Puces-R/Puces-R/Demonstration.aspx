<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demonstration.aspx.cs" Inherits="Puces_R.Demonstration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form runat="server">

    <h1>Yan</h1>
    <ul>
        <li><a href="Connexion.aspx">Connexion</a></li>
        <li><a href="InscriptionVendeur.aspx">Inscription vendeur</a></li>
        <li><a href="InscriptionClient.aspx">Inscription client</a></li>
    </ul>
    
    <h1>Louis-Pierre</h1>
    <ul>
        <li><a href="Produits.aspx">Tout les produits</a></li>
        <li><a href="Produits.aspx?novendeur=10">Produits d'un vendeur</a></li>
        <li><a href="Panier.aspx?noclient=10000">Panier d'un client</a></li>
        <li><a href="Panier.aspx?noclient=10400">Panier d'un autre client</a></li>
    </ul>
    
    <h1>Wilfried</h1>
    <ul>
        <li><a href="accueil_gestionnaire.aspx">Accueil gestionnaire</a></li>
    </ul>

    <h1>Simon</h1>
    <ul>
        <li><a href="InsertionProduits.aspx">Insertion produit</a></li>
        <li><a href="SuppressionProduits.aspx?noproduit=1000010">Suppression produit</a></li>
    </ul>
    </form>
</body>
</html>
