<%@ Page Title="Gérer les vendeurs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="gerer_vendeurs.aspx.cs" Inherits="Puces_R.gerer_vendeurs" EnableEventValidation="false" %>

<%@ Register TagPrefix="lp" TagName="NavigationParPage" Src="~/Controles/NavigationParPage.ascx" %>
<%@ Register TagPrefix="lp" TagName="MenuGestionnaire" Src="~/Controles/MenuGestionnaire.ascx" %>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuGestionnaire ID="MenuGestionnaire1" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                 $("#MainContent_datepicker3").datepicker("option", "yy-mm-dd", $(this).val());
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="div_englobant">
        <div class="panneau pnlGauche">
            <h2>Rechercher un vendeur</h2>
            <div style="height:700px;">
                <div>                    
                    <table>
                        <tr><th colspan="2">Mots clés</th></tr>
                        <tr><td colspan="2">
                            <asp:textbox runat="server" id="txtCritereRecherche" /> &nbsp;
                             <br /><br />
                        </td></tr>

                        <tr><th colspan="2">Catégories</th></tr>
                        <tr><td colspan="2">
                            <asp:DropDownList id="ddlCategorie" runat="server" AutoPostBack="true"></asp:DropDownList><br /><br />
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

                        <tr><th colspan="2"><br />Trier par:</th></tr>
                        <tr><td colspan="2">
                            <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true">
                                <asp:ListItem Text="Nom d'affaire" />
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
            <asp:DataList RepeatColumns="2" RepeatDirection="Horizontal" runat="server" ID="rptVendeurs" OnItemDataBound="rptVendeurs_ItemDataBound" >
                <ItemTemplate>
                    <div style="width:400px;">
                        <div class="rectangleItem hautRectangle">
                            <asp:LinkButton runat="server" ID="nom_affaire" />
                        </div>
                        <div class="rectangleItem basRectangle">
                            <table class="tableProduits" style="width:95%;">
                                <tr><th>Nom complet</th><td><asp:label runat="server" Text="" ID="nom_complet" /></td></tr>
                                <tr><th>Adresse courriel</th><td><asp:label runat="server" Text="" ID="adresse_courriel" /></td></tr>
                                <tr><th>Date d'inscription</th><td><asp:label runat="server" Text="" ID="date_insc" /></td></tr>
                            </table>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Datalist>
        </div>
    </div>
</asp:Content>
