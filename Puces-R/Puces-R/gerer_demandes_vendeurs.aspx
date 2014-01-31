<%@ Page Title="Gérer les nouvelles demandes de vendeurs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="gerer_demandes_vendeurs.aspx.cs" Inherits="Puces_R.gerer_demandes_vendeurs" EnableEventValidation="false" %>

<%@ Register TagPrefix="lp" TagName="MenuGestionnaire" Src="~/Controles/MenuGestionnaire.ascx" %>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuGestionnaire ID="MenuGestionnaire1" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Site.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Produits.css" />
    <script type="text/javascript" src="lib/js/librairie.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
        </div>
        <div class="lignePointilleHorizontale"></div>
    <!--<div class="titre_sec">Demandes de vendeurs</div>-->
        <div>
            <asp:DataList RepeatColumns="2" RepeatDirection="Horizontal" runat="server" ID="rptDemandes" OnItemDataBound="rptDemandes_ItemDataBound" OnItemCommand="rptDemandes_ItemCommand">
                <ItemTemplate>
                    <div>
                        <div class="rectangleItem hautRectangle" onclick="afficheOuMasqueInfoVendeur(this);">
                            <asp:Label runat="server" ID="titre_demande" />
                        </div>
                        <div class="rectangleItem basRectangle">
                            <table class="tableTitreValeur">
                                <tr >
                                    <td>Adresse:</td>
                                    <td><asp:Label runat="server" ID="addr_demande" /></td>
                                </tr>
                                <tr>
                                    <td>Téléphone:</td>
                                    <td><asp:Label runat="server" ID="tels_demande" /></td>
                                </tr>
                                <tr>
                                    <td>Courriel:</td>
                                    <td><asp:Label runat="server" ID="courriel_demande" /></td>
                                </tr>
                                <tr>
                                    <td>Poids maximal:</td>
                                    <td><asp:Label runat="server" ID="charge_max_demande" /></td>
                                </tr>
                                <tr>
                                    <td>Livraison gratuite:</td>
                                    <td><asp:Label runat="server" ID="livraison_gratuite" /></td>
                                </tr>
                                <tr>
                                    <td>Date de demande:</td>
                                    <td><asp:Label runat="server" ID="date_demande" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button runat="server" Text="Accepter" OnClientClick="afficher_accepter(this);" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" Text="Refuser" OnClientClick="afficher_refuser(this);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="verdict_vendeur">
                                        <h2 class="center">Acceptation de la demande:</h2>                                
                                        <p class="center">
                                            Entrez le taux de facturation du vendeur 
                                            <span class="remarque">(Format: 00.00)</span>: 
                                            <asp:TextBox runat="server" id="taux_facturation" MaxLength="5"  Width="55" step="0.01" Min="0" Max="100" /> <br />
                                            <asp:RegularExpressionValidator
                                                runat="server"
                                                id="regex_taux"
                                                ControlToValidate="taux_facturation"
                                                Display="Dynamic"
                                                ErrorMessage="Format: 00.00"
                                                EnableClientScript="true" 
                                                ValidationExpression="^\d{1,2}\.\d{1,2}$" />
                                        </p>
                                        Mail de confirmation de l'acceptation envoyé au vendeur <br />
                                        <span class="remarque">(N'ajouter pas le taux dans le mail de confirmation, il sera automatiquement ajouté dans le mail avant l'envoi)</span><br />
                                        <asp:TextBox runat="server" id="cont_mail_acceptation" TextMode="MultiLine" Columns="70" Rows="15" />
                                        <p class="center">
                                            <asp:Button id="btn_accepter" runat="server" text="Envoyer le courriel d'acceptation" OnCommand="acceptation_demande"/>
                                            <asp:Button ID="Button3" runat="server" Text="Annuler" OnClientClick="annuler_acceptation(this);" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="verdict_vendeur">
                                        <h2 class="center">Refus de la demande:</h2>
                                        Mail de refus envoyé au vendeur <br />
                                        <asp:TextBox runat="server" id="cont_mail_refus" TextMode="MultiLine" Columns="70" Rows="15" />
                                        <p class="center">
                                            <asp:Button id="btn_refuser" runat="server" text="Envoyer le courriel de refus" OnCommand="refus_demande"/>
                                            <asp:Button ID="Button4" runat="server" Text="Annuler" OnClientClick="annuler_refus(this);" />
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
</asp:Content>
