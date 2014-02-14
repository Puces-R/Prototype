<%@ Page Title="Gérer l'inactivité des vendeurs" Language="C#" MasterPageFile="~/NavigationItems.Master" AutoEventWireup="true" CodeBehind="gerer_inactivite_vendeurs.aspx.cs" Inherits="Puces_R.gerer_inactivite_vendeurs" EnableEventValidation="false"%>
<%@ MasterType VirtualPath="~/NavigationItems.Master" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
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

<asp:Content ContentPlaceHolderID="BarreCriteres" runat="server">
    <div class="barreListesDeroulantes">
        <span class="boiteListeDeroulante">
            Recherche:
            <asp:DropDownList ID="ddlTypeRecherche" runat="server">
                <asp:ListItem Text="Nom d'affaires" />
            </asp:DropDownList>
            <asp:TextBox ID="txtCritereRecherche" runat="server" />
            <asp:Button runat="server" Text="Go" ID="btnRecherche" OnClick="AfficherPremierePage" />
        </span>
        <span class="boiteListeDeroulante">
            Trier par:
            <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
                <asp:ListItem Text="Numéro" />
                <asp:ListItem Text="Nom d'affaires" />
                <asp:ListItem Text="Date de demande" />
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
                <asp:ListItem Text="1 An" Value="1" Selected="True" />
                <asp:ListItem Text="2 Ans" Value="2" />
                <asp:ListItem Text="3 Ans" Value="3"/>
            </asp:DropDownList>
        </span>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Items" runat="server">
    <div id="div_msg" runat="server"></div>
    <div id="div_chck">    
        <div style="font-size: small;">
            <table border="0" width="100%" cellpadding="5" cellspacing="2" >
                <tr class="rectangleItem hautRectangle" >
                    <th><input type="checkbox" id="cb_tout" title="Sélectionner/Desélectionner tous les items de la page" class="cocher_tout" onchange="check_desactiver_tout(this);" /></th>
                    <th>#</th>
                    <th>Nom d'affaires</th>
                    <th>Nom du vendeur</th>
                    <th><asp:Button ID="btn_desactiver_tout" runat="server" Text="Désactiver la sélection" ForeColor="Black" ToolTip="Désactiver tous les vendeurs sélectionnés" disabled="true" OnClick="desactiver_liste"/></th>
                </tr>
                <asp:Repeater runat="server" ID="rptInnactifs1" OnItemDataBound="rptInnactifs1_ItemDataBound" >
                    <ItemTemplate>                        
                        <tr class="rectangleItem basRectangle">
                            <td><input type="checkbox" ID="cb_desactiver" runat="server" title="Sélectionner ce vendeur" class="cb_selection" onchange="check_desactiver_tout(this);" /></td>
                            <td><asp:Label runat="server" ID="lbl_num" /></td>
                            <td><asp:Label runat="server" ID="lbl_nom_affaire" /></td>
                            <td><asp:Label runat="server" ID="lbl_nom_vendeur" /></td>
                            <td><asp:Button ID="btn_desactiver" runat="server" Text="Voir détails/Désactiver" OnCommand="desactiver_vendeur" ToolTip="Désactiver ce vendeur" /></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>