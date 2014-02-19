<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormulaireProduit.ascx.cs"
    Inherits="Puces_R.FormulaireProduit" %>
<tr>
    <td style="width: 150px;">
        <asp:Label ID="lblCategorieProduits" runat="server">Catégorie</asp:Label>
    </td>
    <td style="width: 350px;">
        <asp:DropDownList ID="ddlCategorieProduits" runat="server">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="lblDesc" runat="server">Description abrégée</asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tbDescAbregee" runat="server" MaxLength="50" />
    </td>
    <td class="erreur">
        <asp:RequiredFieldValidator ID="RequiredAbregee" ControlToValidate="tbDescAbregee"
            EnableClientScript="false" ErrorMessage="La description abrégée est obligatoire"
            runat="server" />
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="lblPrix" runat="server">Prix demandé</asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tbPrix" runat="server" MaxLength="9" />
    </td>
    <td class="erreur">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="tbPrix"
            EnableClientScript="false" ErrorMessage="Le prix demandé est obligatoire" runat="server" />
        <asp:RegularExpressionValidator ID="rePrixDemande" ControlToValidate="tbPrix" EnableClientScript="false"
            runat="server" ErrorMessage="Le format du prix demandé est invalide" ValidationExpression="^\d{1,6}([\.\,]\d{0,2})?$" />
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="lblDescComplete" runat="server">Description complète</asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tbDescComplete" TextMode="multiline" runat="server" CssClass="DescComplete"></asp:TextBox>
    </td>
    <td class="erreur">
        <asp:RequiredFieldValidator ID="RequiredComplete" ControlToValidate="tbDescComplete"
            EnableClientScript="false" ErrorMessage="La description complète est obligatoire"
            runat="server" />
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="lblPhoto" runat="server">Photo du produit</asp:Label>
    </td>
    <td>
        <asp:FileUpload ID="uplNomFichier" runat="server" CssClass="" />
    </td>
    <td class="erreur">
        <asp:CustomValidator ID="CustomStyleImage" ControlToValidate="uplNomFichier" runat="server"
            OnServerValidate="verifierFormat" ErrorMessage="Le Format de l'image doit être jpg,png ou gif"></asp:CustomValidator>
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="lblNbItems" runat="server">Nombre d'items</asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tbNbItems" runat="server" MaxLength="5" />
    </td>
    <td class="erreur">
        <asp:RequiredFieldValidator ID="RequiredNbItems" ControlToValidate="tbNbItems" EnableClientScript="false"
            ErrorMessage="Le nombre d'items est obligatoire" runat="server" />
        <asp:RangeValidator ID="RangeNbItems" MinimumValue="0" MaximumValue="32767" Type="Double"
            ControlToValidate="tbNbItems" EnableClientScript="false" ErrorMessage="Le nombre d'items doit être un nombre entre 0 et 32767"
            runat="server" />
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="lblprixVente" runat="server">Prix de vente</asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tbPrixVente" runat="server" MaxLength="9" />
    </td>
    <td class="erreur">
        <asp:RegularExpressionValidator ID="rePrixVente" ControlToValidate="tbPrixVente"
            EnableClientScript="false" runat="server" ErrorMessage="Le format du prix de vente est invalide"
            ValidationExpression="^\d{1,6}([\.\,]\d{0,2})?$"> 
        </asp:RegularExpressionValidator>
        <asp:CustomValidator runat="server" ID="adresseExiste" ControlToValidate="tbPrixVente"
            OnServerValidate="validerPrixVente" ErrorMessage="Le prix de vente doit être plus petit que le prix demandé" />
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="lblPois" runat="server">Pois de l'article</asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tbPois" runat="server" MaxLength="9" />
    </td>
    <td class="erreur">
        <asp:RequiredFieldValidator ID="RequiredPois" ControlToValidate="tbPois" EnableClientScript="false"
            ErrorMessage="Le pois est obligatoire" runat="server" />
        <asp:RegularExpressionValidator ID="rePoids" ControlToValidate="tbPois" EnableClientScript="false"
            runat="server" ErrorMessage="Le format du poids est invalide" ValidationExpression="^\d{1,7}([\.\,]\d{0,1})?$"> 
        </asp:RegularExpressionValidator>
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="lblDisponibilité" runat="server">Disponibilité</asp:Label>
    </td>
    <td>
        <asp:CheckBox ID="cbDisponibilite" runat="server" Text="Le produit est-il visible par les clients?"
            AutoPostBack="false" />
    </td>
</tr>
