<%@ Page Title="Gérer l'inactivité des vendeurs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="gerer_inactivite_vendeurs.aspx.cs" Inherits="Puces_R.gerer_inactivite_vendeurs" EnableEventValidation="false"%>

<%@ Register TagPrefix="lp" TagName="MenuGestionnaire" Src="~/Controles/MenuGestionnaire.ascx" %>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuGestionnaire ID="MenuGestionnaire1" runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/site.css" />
    <script type="text/javascript" src="lib/js/librairie.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="barreListesDeroulantes">
            <span class="boiteListeDeroulante">
                Recherche:
                <asp:DropDownList ID="ddlTypeRecherche" runat="server">
                    <asp:ListItem Text="Nom d'affaire" />
                </asp:DropDownList>
                <asp:TextBox ID="txtCritereRecherche" runat="server" />
                <asp:Button runat="server" Text="Go" ID="btnRecherche" />
            </span>
            <span class="boiteListeDeroulante">
                Trier par:
                <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true">
                    <asp:ListItem Text="Numéro" />
                    <asp:ListItem Text="Nom d'affaire" />
                    <asp:ListItem Text="Date de demande" />
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
            <span class="boiteListeDeroulante">
                Temps d'inactivité:
                <asp:DropDownList ID="ddlTempsInnactivite" runat="server" AutoPostBack="true">
                    <asp:ListItem Text="Tous" Value="0" />
                    <asp:ListItem Text="1 An" Value="1" Selected="True" />
                    <asp:ListItem Text="2 Ans" Value="2" />
                    <asp:ListItem Text="3 Ans" Value="3"/>
                </asp:DropDownList>
            </span>
        </div>
    <div class="lignePointilleHorizontale pleineLargeur"></div>
    <div>
        <div class="panneau pnlGauche">
            <asp:DataList RepeatColumns="2" RepeatDirection="Horizontal" runat="server" ID="rptInnactifs1" OnItemDataBound="rptInnactifs1_ItemDataBound" >
                <ItemTemplate>
                    <div>
                        <div onclick="afficheOuMasqueInfoInactif(this)" class="rectangleItem hautRectangle">
                            <asp:Label runat="server" ID="titre_inactif1" />
                        </div>
                        <div class="rectangleItem basRectangle">
                            <table class="tableTitreValeur">
                                <tr>
                                    <td colspan="2" >
                                        <h2 class="center">Voulez vous vraiment désactiver ce vendeur?</h2>
                                    </td>
                                </tr>
                                <tr >
                                    <td>Adresse:</td>
                                    <td><asp:Label runat="server" ID="addr_inactif1" /></td>
                                </tr>
                                <tr>
                                    <td>Téléphone:</td>
                                    <td><asp:Label runat="server" ID="tels_inactif1" /></td>
                                </tr>
                                <tr>
                                    <td>Courriel:</td>
                                    <td><asp:Label runat="server" ID="courriel_inactif1" /></td>
                                </tr>
                                <tr>
                                    <td>Taux de facturation:</td>
                                    <td><asp:Label runat="server" ID="taux_facturation_inactif1" /></td>
                                </tr>
                                <tr>
                                    <td>Poids maximal:</td>
                                    <td><asp:Label runat="server" ID="charge_max_inactif1" /></td>
                                </tr>
                                <tr>
                                    <td>Livraison gratuite:</td>
                                    <td><asp:Label runat="server" ID="livraison_gratuite_inactif1" /></td>
                                </tr>
                                <tr>
                                    <td>Inactif depuis:</td>
                                    <td><asp:Label runat="server" ID="date_inactif1" /></td>
                                </tr>
                            </table>
                            <div class="boutonsCentre">
                                <asp:Button ID="btn_desactiver" runat="server" Text="Désactiver" OnCommand="desactiver_vendeur"/>
                                <asp:Button ID="Button1" Text="Annuler" runat="server" OnClientClick="annuler_desactiver(this);" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Content>