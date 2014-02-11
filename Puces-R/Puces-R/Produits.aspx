<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produits.aspx.cs" Inherits="Puces_R.Produits" MasterPageFile="~/NavigationItems.Master" %>
<%@ MasterType VirtualPath="~/NavigationItems.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <link href="CSS/Produits.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BarreCriteres">
    <span class="boiteListeDeroulante">
        Recherche:
        <asp:DropDownList ID="ddlTypeRecherche" runat="server" Font-Size="X-Small">
            <asp:ListItem Text="Date de parution" />
            <asp:ListItem Text="Numéro" />
            <asp:ListItem Text="Description" />
        </asp:DropDownList>
        <asp:TextBox ID="txtCritereRecherche" runat="server" Width="50" Font-Size="X-Small" />
        <asp:Button runat="server" Text="Go" ID="btnRecherche" OnClick="AfficherPremierePage" Font-Size="X-Small" />
    </span>
    <span class="boiteListeDeroulante">
        Trier par:
        <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" Font-Size="X-Small" >
            <asp:ListItem Text="Numéro" />
            <asp:ListItem Text="Catégorie" />
            <asp:ListItem Text="Date de parution" />
            <asp:ListItem Text="Évaluations" />
        </asp:DropDownList>
    </span>
    <span class="boiteListeDeroulante">
        Par page:
        <asp:DropDownList ID="ddlParPage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" Font-Size="X-Small" >
            <asp:ListItem Value="5" />
            <asp:ListItem Value="10" />
            <asp:ListItem Value="15" Selected="True" />
            <asp:ListItem Value="20" />
            <asp:ListItem Value="25" />
            <asp:ListItem Value="50" />
        </asp:DropDownList>
    </span>
    <div class="boiteListeDeroulante">
        <asp:MultiView runat="server" ID="mvCategorie" ActiveViewIndex="0">
            <asp:View runat="server">
                Catégorie:
                <asp:DropDownList ID="ddlCategorie" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" Font-Size="X-Small" />
            </asp:View>
            <asp:View runat="server">
                Catégories:
                <div class="listeCochable">
                    <asp:CheckBoxList ID="cblCategorie" runat="server" RepeatLayout="Table" />
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
    <div class="boiteListeDeroulante">
        <asp:MultiView runat="server" ID="mvVendeur" ActiveViewIndex="0">
            <asp:View runat="server">
                Vendeur:
                <asp:DropDownList ID="ddlVendeur" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" Font-Size="X-Small" />
            </asp:View>
            <asp:View runat="server">
                Vendeurs:
                <div class="listeCochable">
                    <asp:CheckBoxList ID="cblVendeur" runat="server" RepeatLayout="Table" />
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
    <div class="boiteListeDeroulante">
        <asp:Button runat="server" Text="Avancé" ID="btnRechercheAvance" OnClick="btnRechercheAvance_OnClick" Font-Size="X-Small" />
    </div>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Items">
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
                        <asp:Label runat="server" ID="lblEvaluation" />
                        <asp:Label runat="server" ID="lblQuantite" />
                    </div>
                </div>
            </ItemTemplate>
        </ASP:DataList>
    </div>
</asp:Content>