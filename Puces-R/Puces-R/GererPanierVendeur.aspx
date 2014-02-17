<%--<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GererPanierVendeur.aspx.cs" Inherits="Puces_R.GererPanierVendeur"  %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="se" TagName="PanierNettoyer" Src="~/Controles/NettoyerPanier.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="CSS/GestionCommandesVendeur.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">

<h2>Panier(s) Inactif</h2>

               
        <ASP:Repeater id="rptPaniers" runat="server" OnItemDataBound="rptPaniers_ItemDataBound">
            <ItemTemplate>
                <div class="rectangleItem rectangleComplet">
                    <asp:HyperLink runat="server" ID="hypVendeur" CssClass="titreRectangle" />
                    <se:PanierNettoyer ID="ctrPanierN" runat="server" />
                    <%--<asp:Repeater ID="rptProduits" runat="server" OnItemDataBound="rptProduits_ItemDataBound">
                        <HeaderTemplate>
                            <table class="tableProduits">
                                <tr>
                                    <th>Produit</th>
                                    <th>Quantité</th>
                                    <th>Prix unitaire</th>
                                    <th>Prix total</th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hypProduit" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblQuantite" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblPrixUnitaire" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblPrixTotal" />
                                    </td>
                                </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>--%>
                 <%--   <div class="sousTotal">
                        Sous-Total: <asp:Label runat="server" ID="lblSousTotal" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

</asp:Content>--%>

<%@ Page Title="Gérer les paniers inactifs" Language="C#" MasterPageFile="~/NavigationItems.Master" AutoEventWireup="true" CodeBehind="GererPanierVendeur.aspx.cs" Inherits="Puces_R.GererPanierVendeur" EnableEventValidation="false"%>
<%@ MasterType VirtualPath="~/NavigationItems.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/site.css" />
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4_2.css" />
    <script src="//code.jquery.com/jquery-latest.js"></script>
    <script type="text/javascript" src="lib/js/librairie.js"></script>
    <script>

        $(document).ready(function () {
            $('#cb_tout').click(function () {
                var cases = $(".basRectangle").find(':checkbox');
                if (this.checked) {
                    cases.prop('checked', 'checked');
                } else {
                    cases.prop('checked', '');
                }
            });
        });
    </script>
    <script type="text/javascript">
        function check_desactiver_tout(cb_case) {
            if (cb_case.checked == true) {
                document.getElementById("<%=btn_desactiver_tout.ClientID %>").disabled = false;
                //alert(document.getElementById("<%=btn_desactiver_tout.ClientID %>").disabled);
            }
            else {
                var tab_cases = document.getElementsByClassName('cb_selection');
                var activer = false;

                for (var i = 0; i < tab_cases.length; i++)
                //if (tab_cases[i].checked != null)
                    activer = activer || tab_cases[i].checked;
                document.getElementById("<%=btn_desactiver_tout.ClientID %>").disabled = !activer;
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BarreCriteres" runat="server">
    <div class="barreListesDeroulantes">
        <span class="boiteListeDeroulante">
            Recherche:
            <asp:DropDownList ID="ddlTypeRecherche" runat="server">
                <asp:ListItem Text="Nom de Client" />
            </asp:DropDownList>
            <asp:TextBox ID="txtCritereRecherche" runat="server" />
            <asp:Button runat="server" Text="Go" ID="btnRecherche" OnClick="AfficherPremierePage" />
        </span>
        <span class="boiteListeDeroulante">
            Trier par:
            <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
                <asp:ListItem Text="Date de demande" />
                <asp:ListItem Text="Montant du panier" />
            </asp:DropDownList>
        </span>
        <span class="boiteListeDeroulante">
            Par page:
            <asp:DropDownList ID="ddlParPage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
                <asp:ListItem Value="20" />
                <asp:ListItem Value="25" />
                <asp:ListItem Value="50" />
                <asp:ListItem Value="75" />
                <asp:ListItem Value="100" />
            </asp:DropDownList>
        </span>
        <span class="boiteListeDeroulante">
            Temps d'inactivité:
            <asp:DropDownList ID="ddlTempsInnactivite" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
                <asp:ListItem Text="Tous" Value="0" />
                <asp:ListItem Text="1 mois" Value="1" Selected="True" />
                <asp:ListItem Text="2 mois" Value="2" />
                <asp:ListItem Text="3 mois" Value="3"/>
                <asp:ListItem Text="6 mois et +" Value="6"/>
            </asp:DropDownList>
        </span>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Items" runat="server">
    <div id="div_msg" runat="server"></div>
    <div id="div_chck">    
        <div style="font-size: small;">
        <asp:Button ID="btn_desactiver_tout" runat="server" Text="Désactiver la sélection" ForeColor="Black" ToolTip="Désactiver tous les vendeurs sélectionnés" disabled="true" OnClick="desactiver_liste"/></th>
            <table border="0" width="100%" cellpadding="5" cellspacing="2" >
                <tr class="rectangleItem hautRectangle" >

                    <th><input type="checkbox" id="cb_tout" title="Sélectionner/Desélectionner tous les items de la page" class="cocher_tout" onchange="check_desactiver_tout(this);" /></th>
                    <th>#</th>
                    <th>NoVendeur</th>
                    <th>Nom d'affaires</th>
                    <th>Nom Client</th>
                    <th>Montant Panier</th>
                    <th>Date Inactivité</th>
                    <th>Action</th>
                    
                </tr>
                <asp:Repeater runat="server" ID="rptInnactifs1" OnItemDataBound="rptInnactifs1_ItemDataBound" >
                    <ItemTemplate>                      
                        <tr class="rectangleItem basRectangle">
                            <td><input type="checkbox" ID="cb_desactiver" runat="server" title="Sélectionner ce vendeur" class="cb_selection" onchange="check_desactiver_tout(this);" /></td>
                            <td><asp:Label runat="server" ID="lbl_num" /></td>
                            <td><asp:Label runat="server" ID="lblNoVendeur" /></td>
                            <td><asp:Label runat="server" ID="lbl_nom_affaire" /></td>
                            <td><asp:Label runat="server" ID="lblNomClient" /></td>
                            <td><asp:Label runat="server" ID="lblMontant" /></td>
                            <td><asp:Label runat="server" ID="date_inactif1" /></td>
                            <td><asp:Label runat="server" ID="lblInactif"></asp:Label><asp:Button ID="btn_desactiver" runat="server" Text="Effacer le panier" OnCommand="desactiver_vendeur" ToolTip="Effacer le panier" /></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>