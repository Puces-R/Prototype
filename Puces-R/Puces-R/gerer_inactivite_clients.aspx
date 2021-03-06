﻿<%@Page Title="Gérer l'inactivité des clients" Language="C#" MasterPageFile="~/NavigationItems.Master" AutoEventWireup="true" CodeBehind="gerer_inactivite_clients.aspx.cs" Inherits="Puces_R.gerer_inactivite_clients" EnableEventValidation="false" %>
<%@ MasterType VirtualPath="~/NavigationItems.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/site.css" />
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4_2.css" />
    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <script type="text/javascript" src="lib/js/librairie.js"></script>
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
                <asp:ListItem Text="Nom complet" />
            </asp:DropDownList>
            <asp:TextBox ID="txtCritereRecherche" runat="server" />
            <asp:Button runat="server" Text="Go" ID="btnRecherche" OnClick="AfficherPremierePage" />
        </span>
        <span class="boiteListeDeroulante">
            Trier par:
            <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
                <asp:ListItem Text="Numéro" />
                <asp:ListItem Text="Nom complet" />
                <asp:ListItem Text="Date de demande" />
            </asp:DropDownList>
            <asp:DropDownList ID="ddlOrdre" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
                <asp:ListItem Text="Croissant" Value=" ASC " />
                <asp:ListItem Text="Décroissant" Value=" DESC " />
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
        <span class="boiteListeDeroulante">
            <asp:Button ID="btn_desactiver_tout" runat="server" Text="Désactiver la sélection" ForeColor="Black" ToolTip="Désactiver tous les clients sélectionnés" disabled="true" OnClick="desactiver_liste"/>
        </span>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Items" runat="server">
    <div>
    <div id="div_msg" runat="server" class="center"></div>
        <div id="div_chck" style="font-size: small; width: 100%; margin: auto;" runat="server" >
            <table border="0" width="100%" cellpadding="5" cellspacing="2" >
                <tr class="rectangleItem hautRectangle" >
                    <th><input type="checkbox" id="cb_tout" title="Sélectionner/Desélectionner tous les items de la page" class="cocher_tout" onchange="check_desactiver_tout(this);" /></th>
                    <th>#</th>
                    <th>Nom complet </th>
                    <th>Adresse courriel</th>
                    <th>Actions</th>
                </tr>
                <asp:Repeater runat="server" ID="rptInnactifs1" OnItemDataBound="rptInnactifs1_ItemDataBound" >
                    <ItemTemplate>                        
                        <tr class="rectangleItem basRectangle">
                            <td><input type="checkbox" ID="cb_desactiver" runat="server" title="Sélectionner ce client" class="cb_selection" onchange="check_desactiver_tout(this);" /></td>
                            <td><asp:LinkButton runat="server" ID="lbl_num"  OnCommand="desactiver_client" ToolTip="Cliquez pour voir les détails ce client" /></td>
                            <td><asp:LinkButton runat="server" ID="lbl_nom_complet" OnCommand="desactiver_client" ToolTip="Cliquez pour voir les détails ce client"  /></td>
                            <td><asp:LinkButton runat="server" ID="lbl_courriel" OnCommand="desactiver_client" ToolTip="Cliquez pour voir les détails ce client"  /></td>
                            <td><asp:Button ID="btn_desactiver" runat="server" Text="Désactiver" OnCommand="desactiver_client" ToolTip="Désactiver ce client" /></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
