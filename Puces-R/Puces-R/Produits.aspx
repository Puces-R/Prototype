﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produits.aspx.cs" Inherits="Puces_R.Produits" MasterPageFile="~/Site.Master"  %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuClient.ascx" %>
<%@ Register TagPrefix="lp" TagName="NavigationParPage" Src="~/Controles/NavigationParPage.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient runat="server" ID="ctrMenu" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <link href="CSS/Produits.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <div class="barreListesDeroulantes">
            <span class="boiteListeDeroulante">
                Recherche:
                <asp:DropDownList ID="ddlTypeRecherche" runat="server">
                    <asp:ListItem Text="Date de parution" />
                    <asp:ListItem Text="Numéro" />
                    <asp:ListItem Text="Description" />
                </asp:DropDownList>
                <asp:TextBox ID="txtCritereRecherche" runat="server" />
                <asp:Button runat="server" Text="Go" ID="btnRecherche" OnClick="btnRecherche_OnClick"/>
            </span>
            <span class="boiteListeDeroulante">
                Trier par:
                <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTrierPar_OnSelectedIndexChanged" >
                    <asp:ListItem Text="Numéro" />
                    <asp:ListItem Text="Catégorie" />
                    <asp:ListItem Text="Date de parution" />
                </asp:DropDownList>
            </span>
            <span class="boiteListeDeroulante">
                Par page:
                <asp:DropDownList ID="ddlParPage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlParPage_OnSelectedIndexChanged" >
                    <asp:ListItem Value="5" />
                    <asp:ListItem Value="10" />
                    <asp:ListItem Value="15" Selected="True" />
                    <asp:ListItem Value="20" />
                    <asp:ListItem Value="25" />
                    <asp:ListItem Value="50" />
                </asp:DropDownList>
            </span>
            <span class="boiteListeDeroulante">
                Catégorie:
                <asp:DropDownList ID="ddlCategorie" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategorie_OnSelectedIndexChanged" />
            </span>
        </div>
    </div>
    <lp:NavigationParPage runat="server" ID="ctrNavigationHaut" />
    <div class="lignePointilleHorizontale pleineLargeur"></div>
    <div>
        <ASP:DataList id="dtlProduits" RepeatColumns="5" RepeatDirection="Horizontal" runat="server" OnItemDataBound="dtlProduits_ItemDataBound">
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
        </ASP:DataList>
    </div>
    <lp:NavigationParPage runat="server" ID="ctrNavigationBas" />
</asp:Content>