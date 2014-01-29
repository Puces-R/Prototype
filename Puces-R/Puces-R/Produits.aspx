<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produits.aspx.cs" Inherits="Puces_R.Produits" MasterPageFile="~/Site.Master"  %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuClient.ascx" %>

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
    <div class="lignePointilleHorizontale"></div>
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
    <div class="lignePointilleHorizontale"></div>
    
    <asp:Panel runat="server" ID="pnlLeftNavigation" CssClass="navigation leftNavigation">
        <div>
            <asp:ImageButton runat="server" ID="imgFirst" OnClick="btnFirst_OnClick" ImageUrl="Images/Premier.png" CssClass="imageCentree" />
            <asp:LinkButton runat="server" Text="Premier" ID="btnFirst" OnClick="btnFirst_OnClick" />
        </div>
        <div>
            <asp:ImageButton runat="server" ID="imgPrevious" OnClick="btnPrevious_OnClick" ImageUrl="Images/Precedent.png" CssClass="imageCentree" />
            <asp:LinkButton runat="server" Text="Précédent" ID="btnPrevious" OnClick="btnPrevious_OnClick" />
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlRightNavigation" CssClass="navigation rightNavigation">
        <div>
            <asp:LinkButton runat="server" Text="Suivant" ID="btnNext" OnClick="btnNext_OnClick" />
            <asp:ImageButton runat="server" ID="imgNext" OnClick="btnNext_OnClick" ImageUrl="Images/Prochain.png" CssClass="imageCentree" />
        </div>   
        <div>
            <asp:LinkButton runat="server" Text="Dernier" ID="btnLast" OnClick="btnLast_OnClick" />
            <asp:ImageButton runat="server" ID="imgLast" OnClick="btnLast_OnClick" ImageUrl="Images/Dernier.png" CssClass="imageCentree" />
        </div>
    </asp:Panel>
</asp:Content>