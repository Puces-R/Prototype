<%@ Page Title="Gérer l'inactivité des vendeurs" Language="C#" MasterPageFile="~/NavigationItems.Master" AutoEventWireup="true" CodeBehind="gerer_inactivite_vendeurs.aspx.cs" Inherits="Puces_R.gerer_inactivite_vendeurs" EnableEventValidation="false"%>

<%@ Register TagPrefix="lp" TagName="MenuGestionnaire" Src="~/Controles/MenuGestionnaire.ascx" %>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuGestionnaire ID="MenuGestionnaire1" runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/site.css" />
    <script src="//code.jquery.com/jquery-latest.js"></script>
    <script type="text/javascript" src="lib/js/librairie.js"></script>
    <script>
        function checkAll() {
            $('input:checkbox').each(
             function () {
                 this.checked = true;  
             }
         )
         }

         function unCheckAll() {
             $('input:checkbox').each(
             function () {
                 this.checked = false;
             }
         )
         }

        function initialize() {
            $('.cocher_tout').click(checkAll);
            $('.decocher_tout').click(unCheckAll);
        }

        $(document).ready(initialize);  
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="BarreCriteres" runat="server">
    <div class="barreListesDeroulantes">
        <span class="boiteListeDeroulante">
            Recherche:
            <asp:DropDownList ID="ddlTypeRecherche" runat="server">
                <asp:ListItem Text="Nom d'affaire" />
            </asp:DropDownList>
            <asp:TextBox ID="txtCritereRecherche" runat="server" />
            <asp:Button runat="server" Text="Go" ID="btnRecherche" OnClick="AfficherPremierePage" />
        </span>
        <span class="boiteListeDeroulante">
            Trier par:
            <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
                <asp:ListItem Text="Numéro" />
                <asp:ListItem Text="Nom d'affaire" />
                <asp:ListItem Text="Date de demande" />
            </asp:DropDownList>
        </span>
        <span class="boiteListeDeroulante">
            Par page:
            <asp:DropDownList ID="ddlParPage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
                <asp:ListItem Value="6" />
                <asp:ListItem Value="12" />
                <asp:ListItem Value="18" />
                <asp:ListItem Value="24" />
                <asp:ListItem Value="30" />
                <asp:ListItem Value="50" />
            </asp:DropDownList>
        </span>
        <span class="boiteListeDeroulante">
            Temps d'inactivité:
            <asp:DropDownList ID="ddlTempsInnactivite" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
                <asp:ListItem Text="Tous" Value="0" />
                <asp:ListItem Text="1 An" Value="1" Selected="True" />
                <asp:ListItem Text="2 Ans" Value="2" />
                <asp:ListItem Text="3 Ans" Value="3"/>
            </asp:DropDownList>
        </span>
        <br />
        <input id="cocher_tout" class="cocher_tout" runat="server" type="button" value="Sélectionner tout" title="Sélectionner tous les vendeurs innactifs sur cette page" style="margin-top: 15px;" />
        <input id="decocher_tout" class="decocher_tout" type="button" runat="server" value="Effacer la sélection" title="Décocher tous le vendeurs sélectionnés" />
        <asp:Button ID="btn_desactiver_tout" runat="server" Text="Désactiver la sélection" />
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Items" runat="server">
    <div id="div_chck">    
        <div style="font-size: small;">
            <input type="hidden" id="hid_liste_a_desactiver" />
            <asp:DataList RepeatColumns="2" RepeatDirection="Horizontal" runat="server" ID="rptInnactifs1" OnItemDataBound="rptInnactifs1_ItemDataBound" >
                <ItemTemplate>
                    <div style="width:460px;" >
                        <div class="rectangleItem hautRectangle">
                            <table border="0" width="100%" >
                                <tr><td><asp:Label runat="server" ID="titre_inactif1" /></td>
                                <td align="right"><input type="checkbox" ID="cb_desactiver" runat="server" title="Sélectionner ce vendeur" class="cb_selection" /></td></tr>
                            </table>
                        </div>
                        <div class="rectangleItem basRectangle">
                            <table class="tableTitreValeur" >
                                <colgroup>
                                    <col width="50%" />
                                    <col width="50%" />
                                </colgroup>
                                <tr >
                                    <th>Adresse:</th>
                                    <td><asp:Label runat="server" ID="addr_inactif1" /></td>
                                </tr>
                                <tr>
                                    <th>Téléphone:</th>
                                    <td><asp:Label runat="server" ID="tels_inactif1" /></td>
                                </tr>
                                <tr>
                                    <th>Courriel:</th>
                                    <td><asp:Label runat="server" ID="courriel_inactif1" /></td>
                                </tr>
                                <tr>
                                    <th>Taux de facturation:</th>
                                    <td><asp:Label runat="server" ID="taux_facturation_inactif1" /></td>
                                </tr>
                                <tr>
                                    <th>Poids maximal:</th>
                                    <td><asp:Label runat="server" ID="charge_max_inactif1" /></td>
                                </tr>
                                <tr>
                                    <th>Livraison gratuite:</th>
                                    <td><asp:Label runat="server" ID="livraison_gratuite_inactif1" /></td>
                                </tr>
                                <tr>
                                    <th>Inactif depuis:</th>
                                    <td><asp:Label runat="server" ID="date_inactif1" /></td>
                                </tr>
                            </table>
                            <div class="boutonsCentre">
                                <asp:Button ID="btn_desactiver" runat="server" Text="Désactiver" OnCommand="desactiver_vendeur" OnClientClick="return(confirm_desactiver());" ToolTip="Désactiver ce vendeur" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Content>