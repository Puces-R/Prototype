<%@ Page Title="Gérer les vendeurs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="gerer_vendeurs.aspx.cs" Inherits="Puces_R.gerer_vendeurs" EnableEventValidation="false" %>

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
        <div class="lignePointilleHorizontale pleineLargeur"></div>
    <!--<div class="titre_sec">Demandes de vendeurs</div>-->
        <div style="width:52%;">
            <table border="0" width="100%"> 
                <tr onclick="afficheOuMasqueInfoVendeur(this);">
                    <td colspan="2" class="titre_tab2" >Magasin La bonne Affaire, par Michel Dubé</td>
                </tr>
                <tr class="ligne_info_demande" runat="server" >
                    <td colspan="2" class="cont_tab2">
                        <table border="0" width="100%">
                            <colgroup  >
                                <col width="50%"/>
                                <col width="50%"/>
                            </colgroup>
                            <tr >
                                <td align="right">Adresse:</td>
                                <td>5054 rue Tremblay</td>
                            </tr>
                            <tr>
                                <td align="right">Téléphone:</td>
                                <td>Tel1: 514 145 4784<br />Tel2: 438 758 4858</td>
                            </tr>
                            <tr>
                                <td align="right">Courriel:</td>
                                <td>michel.dube@gmail.com</td>
                            </tr>
                            <tr>
                                <td align="right">Charge maximale de livraison:</td>
                                <td>50 kg</td>
                            </tr>
                            <tr>
                                <td align="right">Livraison gratuite:</td>
                                <td>$60</td>
                            </tr>
                            <tr>
                                <td align="right">Date de demande:</td>
                                <td>Lundi 5 Mars 1694</td>
                            </tr>
                            <tr>
                                <td align="right"><input type="button" value="Envoyer un message" onclick="afficher_accepter(this);" class="boutton" />
                                <input type="button" value="Voir statistiques" onclick="afficher_accepter(this);" class="boutton" /></td>
                                <td><input type="button" value="Autre commande" onclick="afficher_accepter(this);" class="boutton" />
                                <input type="button" value="Désactiver" onclick="afficher_refuser(this);" class="boutton" /></td>
                            </tr>
                            <tr>
                                <td colspan="2" class="verdict_vendeur">
                                    <h2 class="center">Envoi d'un message à Michel Dubé:</h2> 
                                    <asp:TextBox runat="server" id="cont_mail_acceptation" TextMode="MultiLine" Columns="70" Rows="15" />
                                    <p class="center">
                                        <asp:Button id="btn_accepter" runat="server" text="Envoyer le courriel d'acceptation" CssClass="boutton" OnCommand="acceptation_demande"/>
                                        <input type="button" value="Annuler" onclick="annuler_acceptation(this);" class="boutton" />
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr onclick="afficheOuMasqueInfoVendeur(this);">
                    <td colspan="2" class="titre_tab2" >Magasin La bonne Offre, par Josiane Marie</td>
                </tr>
                <tr onclick="afficheOuMasqueInfoVendeur(this);">
                    <td colspan="2" class="titre_tab2" >Magasin La bonne Affaire, par Lambert Martin</td>
                </tr>
                <tr onclick="afficheOuMasqueInfoVendeur(this);">
                    <td colspan="2" class="titre_tab2" >Magasin La bonne Affaire, par Claude Stéphane</td>
                </tr>
                <tr onclick="afficheOuMasqueInfoVendeur(this);">
                    <td colspan="2" class="titre_tab2" >Magasin La bonne Affaire, par Jean Pascal</td>
                </tr>                
                <tr onclick="afficheOuMasqueInfoVendeur(this);">
                    <td colspan="2" class="titre_tab2" >Magasin La bonne Aubaine, par Annie Bérubé</td>
                </tr>                
            </table>
        </div>
</asp:Content>
