<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MotDePasse.ascx.cs"
    Inherits="Puces_R.MotDePasse" %>
    <td>
        <asp:Label runat="server" ID="lbl">Mot de passe: </asp:Label>
    </td>
    <td>
        <asp:TextBox runat="server" ID="tbMDP" TextMode="Password" MaxLength="50" Width="150" />
        <asp:RequiredFieldValidator ID="reqMDP" runat="server" ControlToValidate="tbMDP" Text="Le mot de passe est obligatoire" Display="Dynamic" CssClass="erreur" />
    </td>