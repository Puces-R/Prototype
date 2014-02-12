<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfilVendeur.ascx.cs"
    Inherits="Puces_R.Controles.ProfilVendeur" %>

<%@ Register TagPrefix="yc" TagName="CodePostal" Src="~/Controles/CodePostal.ascx" %>
<%@ Register TagPrefix="yc" TagName="Province" Src="~/Controles/Province.ascx" %>
<%@ Register TagPrefix="yc" TagName="Telephone" Src="~/Controles/Telephone.ascx" %>
<%@ Register TagPrefix="yc" TagName="Courriel" Src="~/Controles/Courriel.ascx" %>
<%@ Register TagPrefix="se" TagName="Adresse" Src="~/Controles/Adresse.ascx" %>



<h2>Profil du vendeur</h2>
<table class="tableProfil">
    <tr>
        <td>
            Nom d'affaires
        </td>
        <td>
            <asp:TextBox ID="tbNomAffaires" runat="server" />
        </td>
        <td>
        <asp:RequiredFieldValidator ID="reqNomAffaires" runat="server" ControlToValidate="tbNomAffaires"
            ErrorMessage="Le nom d'Affaires est obligatoire" Display="Dynamic"/>
        </td>
    </tr>
    <tr>
        <td>
            Prénom
        </td>
        <td>
            <asp:TextBox ID="txtPrenom" runat="server" />
        </td>
        <td>
        <asp:RequiredFieldValidator ID="reqPrenom" runat="server" ControlToValidate="txtPrenom"
            ErrorMessage="Le prénom est obligatoire" Display="Dynamic"/>
        </td>
    </tr>
    <tr>
        <td>
            Nom
        </td>
        <td>
            <asp:TextBox ID="txtNom" runat="server" />
        </td>
        <td>
        <asp:RequiredFieldValidator ID="reqNom" runat="server" ControlToValidate="txtNom"
            ErrorMessage="Le nom est obligatoire" Display="Dynamic"/>
        </td>
    </tr>
    
     <se:Adresse ID="Adresse" runat="server" Label="Rue : "/>

    <tr>
        <td>
            Ville
        </td>
        <td>
            <asp:TextBox ID="txtVille" runat="server" />
        </td>
        <td>
        <asp:RequiredFieldValidator ID="reqVille" runat="server" ControlToValidate="txtVille"
            ErrorMessage="La ville est obligatoire" Display="Dynamic"/>
        </td>
    </tr>
    <tr>
        <td>
            Province
        </td>
        <td>
            <yc:Province ID="ctrProvince" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <yc:CodePostal ID="ctrCodePostal" runat="server" Obligatoire="true" />
    <tr>
        <td>
            Pays
        </td>
        <td>
            <asp:TextBox ID="txtPays" runat="server" Enabled="false"/>
        </td>
        <td>
        </td>
    </tr>
    <yc:Telephone ID="ctrTelephone1" runat="server" Obligatoire="true" Label="Telephone 1" />
    <yc:Telephone ID="ctrTelephone2" runat="server" Label="Telephone 2" />
    <tr>
        <td>
            Courriel :
        </td>
        <td>
            <asp:Label ID="lblCourriel" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Mot de passe :
        </td>
        <td>
            <asp:Button ID="btnPassword" runat="server" Text="Changer votre mot de passe!" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Montant maximum pour livraison :
        </td>
        <td>
            <asp:TextBox ID="tbMaxLivraison" runat="server" />
        </td>
        <td>
        <asp:RequiredFieldValidator id="reqMaxLiv"
                    ControlToValidate="tbMaxLivraison"
                    EnableClientScript="false"
                    ErrorMessage="Prix absent!"
                    runat="server"/>
          
         <asp:RegularExpressionValidator ID="reMaxLiv" 
           ControlToValidate="tbMaxLivraison"   
           EnableClientScript="false" runat="server"
           ErrorMessage="Format invalide !" 
           ValidationExpression="^\d+([\.\,]\d{0,5})?$"> 
           </asp:RegularExpressionValidator> 
        </td>
    </tr>
    <tr>
        <td>
            Montant pour livraison gratuite:
        </td>
        <td>
            <asp:TextBox ID="tbLivraisonGratuite" runat="server" />
        </td>
        <td>
        <asp:RequiredFieldValidator id="reqLivGrat"
                    ControlToValidate="tbLivraisonGratuite"
                    EnableClientScript="false"
                    ErrorMessage="Prix absent!"
                    runat="server"/>
          
         <asp:RegularExpressionValidator ID="reLivGrat" 
           ControlToValidate="tbLivraisonGratuite"   
           EnableClientScript="false" runat="server"
           ErrorMessage="Format invalide !" 
           ValidationExpression="^\d+([\.\,]\d{0,5})?$"> 
           </asp:RegularExpressionValidator> 
        </td>
    </tr>
    <tr>
        <td>
            Taxes:
        </td>
        <td>
            <asp:CheckBox ID="cbTaxes" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Pourcentage :
        </td>
        <td>
            <asp:TextBox ID="tbPourcentage" runat="server" Enabled="false" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Dernière mise à jour effectuée le :
        </td>
        <td>
            <asp:Label ID="lblMAJ" runat="server" Enabled="false" />
        </td>
        <td>
        </td>
    </tr>
</table>
<asp:Button runat="server" ID="btnSauvegarder" Text="Sauvegarder" CausesValidation="FALSE"
    OnClick="sauverProfil" />