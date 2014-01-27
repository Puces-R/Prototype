<%@ Page Title="Gérer les nouvelles demandes de vendeurs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="gerer_demandes_vendeurs.aspx.cs" Inherits="Puces_R.gerer_demandes_vendeurs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <script type="text/javascript" src="lib/js/librairie.js"></script>
</asp:Content>

<%@ Register TagPrefix="lp" TagName="MenuGestionnaire" Src="~/Controles/MenuGestionnaire.ascx" %>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuGestionnaire ID="MenuGestionnaire1" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="titre_sec">Demandes de vendeurs</div>
            <table border="0" width="100%"> 
    <asp:Repeater runat="server" ID="rptDemandes" OnItemDataBound="rptDemandes_ItemDataBound" OnItemCommand="rptDemandes_ItemCommand">
        <ItemTemplate>      
            <tr onclick="afficheOuMasqueInfoVendeur(this);">
                <td colspan="2" class="titre_tab2" ><asp:Label runat="server" ID="titre_demande" /></td>
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
                            <td><asp:Label runat="server" ID="addr_demande" /></td>
                        </tr>
                        <tr>
                            <td align="right">Téléphone:</td>
                            <td><asp:Label runat="server" ID="tels_demande" /></td>
                        </tr>
                        <tr>
                            <td align="right">Courriel:</td>
                            <td><asp:Label runat="server" ID="courriel_demande" /></td>
                        </tr>
                        <tr>
                            <td align="right">Charge maximale de livraison:</td>
                            <td><asp:Label runat="server" ID="charge_max_demande" /></td>
                        </tr>
                        <tr>
                            <td align="right">Livraison gratuite:</td>
                            <td><asp:Label runat="server" ID="livraison_gratuite" /></td>
                        </tr>
                        <tr>
                            <td align="right">Date de demande:</td>
                            <td><asp:Label runat="server" ID="date_demande" /></td>
                        </tr>
                        <tr>
                            <td align="right"><input type="button" value="Accepter" onclick="afficher_accepter(this);" class="boutton" /></td>
                            <td><input type="button" value="Refuser" onclick="afficher_refuser(this);" class="boutton" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="verdict_vendeur">
                                <h2 class="center">Acceptation de la demande:</h2>                                
                                <p class="center">
                                    Entrez le taux de facturation du vendeur 
                                    <span class="remarque">(Format: 00.00)</span>: 
                                    <asp:TextBox runat="server" id="taux_facturation" MaxLength="5"  Width="55" step="0.01" Min="0" Max="100" TextMode="Number" /> <br />
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
                                    <asp:Button id="btn_accepter" runat="server" text="Envoyer le courriel d'acceptation" CssClass="boutton" OnCommand="acceptation_demande"/>
                                    <input type="button" value="Annuler" onclick="annuler_acceptation(this);" class="boutton" />
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="verdict_vendeur">
                                <h2 class="center">Refus de la demande:</h2>
                                Mail de refus envoyé au vendeur <br />
                                <asp:TextBox runat="server" id="cont_mail_refus" TextMode="MultiLine" Columns="70" Rows="15" />
                                <p class="center">
                                    <asp:Button id="btn_refuser" runat="server" text="Envoyer le courriel de refus" CssClass="boutton" OnCommand="refus_demande" />
                                    <input type="button" value="Annuler" onclick="annuler_refus(this);" class="boutton" />
                                </p>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
            </table>
</asp:Content>
