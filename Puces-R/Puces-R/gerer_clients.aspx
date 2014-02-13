<%@ Page Title="Gérer les clients" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="gerer_clients.aspx.cs" Inherits="Puces_R.gerer_clients" EnableEventValidation="false" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="lp" TagName="NavigationParPage" Src="~/Controles/NavigationParPage.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link rel="stylesheet" type="text/css" href="CSS/AccueilClient.css" />

    <link rel="stylesheet" type="text/css" href="CSS/Site.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Produits.css" />
    <script type="text/javascript" src="lib/js/librairie.js"></script>
    <script src="//code.jquery.com/jquery-1.9.1.js"></script>
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
    <script>
         $(function () {
             $("#MainContent_datepicker3").datepicker(); 
             $("#format").change(function () {
                 $("#MainContent_datepicker3").datepicker("format", "yy-mm-dd", $(this).val());
             });
         });
         $(function () {
             $("#MainContent_datepicker4").datepicker(); 
             $("#format").change(function () {
                 $("#MainContent_datepicker4").datepicker("option", "yy-mm-dd", $(this).val());
             });
         });
    </script>
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4_2.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="div_englobant">
        <div class="panneau pnlGauche">
            <h2>Rechercher un client</h2>
            <div style="height:700px;">
                <div>                    
                    <table>
                        <tr><th colspan="2">Mots clés</th></tr>
                        <tr><td colspan="2">
                            <asp:textbox runat="server" id="txtCritereRecherche" /> &nbsp;
                             <br /><br />
                        </td></tr>
                                                
                        <tr><th colspan="2">Date d'inscription entre</th></tr>
                        <tr><td>Début:</td><td><asp:textbox runat="server" id="datepicker3" /></td></tr>
                        <tr><td>Fin:</td><td>
                            <asp:textbox runat="server" id="datepicker4" /><br />
                           <%-- <asp:CompareValidator
                                runat="server"
                                ControlToValidate="datepicker4"
                                ControlToCompare="datepicker3"
                                Type="Date"
                                EnableClientScript="true"
                                Operator="GreaterThan"
                                ErrorMessage="La deuxième date doit être <br />supérieur à la première"
                                ForeColor="Red"
                                Font-Size="X-Small"
                                Display="Dynamic"
                            />--%>
                        </td></tr>

                        <tr><th colspan="2"><br />Statut:</th></tr>
                        <tr><td colspan="2">
                            <asp:DropDownList ID="ddlStatut" runat="server" AutoPostBack="true">                                
                                <asp:ListItem Text="Tous" Value="-1" />
                                <asp:ListItem Text="Actif" Value="0" />
                                <asp:ListItem Text="Innactifs" Value="1" />
                            </asp:DropDownList><br />
                        </td></tr>

                        <tr><th colspan="2"><br />Trier par:</th></tr>
                        <tr><td colspan="2">
                            <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true">
                                <asp:ListItem Text="Adresse courriel" />
                                <asp:ListItem Text="Nom" />
                                <asp:ListItem Text="Date de d'inscription" />
                            </asp:DropDownList><br /><br />
                        </td></tr>
         
                        <tr><th colspan="2">Nombre d'items par page:</th></tr>
                        <tr><td colspan="2">
                            <asp:DropDownList ID="ddlParPage" runat="server" AutoPostBack="true">
                                <asp:ListItem Value="5" />
                                <asp:ListItem Value="10" />
                                <asp:ListItem Value="15" Selected="True" />
                                <asp:ListItem Value="20" />
                                <asp:ListItem Value="25" />
                                <asp:ListItem Value="50" />
                            </asp:DropDownList><br /><br />
                        </td></tr>

                        <tr><th colspan="2"><asp:button runat="server" Text="Rechercher" /><br /></th></tr>

                        <tr><td colspan="2"><lp:NavigationParPage runat="server" ID="ctrNavigation" reduit="true"/></td></tr>
                    </table>
                </div>                
            </div>
        </div>
        
        <div class="panneau pnlDroite">
            <h2>Résultats de la recherche</h2>
            <asp:Panel id="no_result" runat="server" cssclass="aucunPanier rectangleItem rectangleComplet" >
                <img src="Images/Precedent.png" alt="Flèche" />
                <p>Aucun résultat pour les critères sélectionnés. Veuillez raffiner vos critères puis re-éssayer.</p>
            </asp:Panel>

            <div style="font-size: small;">
                <table border="0" width="100%" cellpadding="5" cellspacing="2" >
                    <tr class="rectangleItem hautRectangle" >
                        <th>#</th>
                        <th>Adresse courriel</th>
                        <th>Nom complet</th>
                        <th></th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptVendeurs" OnItemDataBound="rptVendeurs_ItemDataBound" >
                        <ItemTemplate>                        
                            <tr class="rectangleItem basRectangle" >
                                <td><asp:Label runat="server" ID="lbl_num" /></td>
                                <td><asp:label runat="server" ID="adresse_courriel" /></td>
                                <td><asp:label runat="server" ID="nom_complet" /></td>
                                <td><asp:Button runat="server" ID="btn_gerer" Text="Gérer" OnCommand="selectionner_client" CssClass="a_droite" ForeColor="Black" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
