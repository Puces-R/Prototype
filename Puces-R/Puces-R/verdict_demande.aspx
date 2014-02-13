<%@ Page Title="Verdict de la dmande du vendeur" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="verdict_demande.aspx.cs" Inherits="Puces_R.verdict_demande" EnableEventValidation="false" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/Site.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Produits.css" />
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <script type="text/javascript" src="lib/js/librairie.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">       
    <div style="font-size: small;" >
            <div>
                <div class="rectangleItem hautRectangle" >
                    <asp:Label runat="server" ID="titre_demande" />
                </div>
                <div class="rectangleItem basRectangle">
                    <table class="tableTitreValeur">
                        <colgroup>
                            <col width="50%" />
                            <col width="50%" />
                        </colgroup>
                        <tr >
                            <th>Adresse:</th>
                            <td><asp:Label runat="server" ID="addr_demande" /></td>
                        </tr>
                        <tr>
                            <th>Téléphone:</th>
                            <td><asp:Label runat="server" ID="tels_demande" /></td>
                        </tr>
                        <tr>
                            <th>Courriel:</th>
                            <td><asp:Label runat="server" ID="courriel_demande" /></td>
                        </tr>
                        <tr>
                            <th>Poids maximal:</th>
                            <td><asp:Label runat="server" ID="charge_max_demande" /></td>
                        </tr>
                        <tr>
                            <th>Livraison gratuite:</th>
                            <td><asp:Label runat="server" ID="livraison_gratuite" /></td>
                        </tr>
                        <tr>
                            <th>Date de demande:</th>
                            <td><asp:Label runat="server" ID="date_demande" /></td>
                        </tr>
                        <asp:MultiView runat="server" ID="mv_verdict" >
                            <asp:View runat="server" ID="view_acceptation">
                                <tr>
                                    <td colspan="2" class="verdict_vendeur center">
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
                                        </p>
                                    </td>
                                </tr>
                            </asp:View>
                            <asp:View runat="server" ID="view_refus">
                                <tr>
                                    <td colspan="2" class="verdict_vendeur">
                                        <h2 class="center">Refus de la demande:</h2>
                                        Mail de refus envoyé au vendeur <br />
                                        <asp:TextBox runat="server" id="cont_mail_refus" TextMode="MultiLine" Columns="70" Rows="15" />
                                        <p class="center">
                                            <asp:Button id="btn_refuser" runat="server" text="Envoyer le courriel de refus" OnCommand="refus_demande"/>
                                        </p>
                                    </td>
                                </tr>
                            </asp:View>
                        </asp:MultiView>
                    </table>
                </div>
            </div>
    </div>
</asp:Content>
