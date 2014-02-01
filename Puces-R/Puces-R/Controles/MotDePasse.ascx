<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MotDePasse.ascx.cs"
    Inherits="Puces_R.MotDePasse" %>
    <td>
        <asp:Label runat="server" ID="lbl">Mot de passe: </asp:Label>
    </td>
    <td>
        <asp:TextBox runat="server" ID="tbMDP" TextMode="Password" MaxLength="50" />
    </td>
    <td runat="server" ID="tdReqMDP" Visible="false" class="erreur">
        <asp:RequiredFieldValidator ID="reqMDP" runat="server" ControlToValidate="tbMDP" ErrorMessage="Le mot de passe est obligatoire" Display="Dynamic" />
    </td>
