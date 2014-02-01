<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Telephone.ascx.cs" Inherits="Puces_R.Telephone" %>
<tr>
    <td>
        <asp:Label runat="server" ID="lbl">Téléphone: </asp:Label>
    </td>
    <td>
        (
        <asp:TextBox runat="server" ID="tbReg" MaxLength="3" Width="25px" />
        )
        <asp:RequiredFieldValidator runat="server" ID="reqReg" ControlToValidate="tbReg" EnableClientScript="false" Display="None"/>
        <asp:RegularExpressionValidator runat="server" ID="formatReg" ControlToValidate="tbReg" ValidationExpression="^\d{3}$" EnableClientScript="false" Display="None" />
        &nbsp;
        <asp:TextBox runat="server" ID="tbPart1" MaxLength="3" Width="25px" />
        <asp:RequiredFieldValidator runat="server" ID="reqPart1" ControlToValidate="tbPart1" EnableClientScript="false" Display="None"/>
        <asp:RegularExpressionValidator runat="server" ID="formatPart1" ControlToValidate="tbPart1" ValidationExpression="^\d{3}$" EnableClientScript="false" Display="None"/>
        -
        <asp:TextBox runat="server" ID="tbPart2" MaxLength="4" Width="35px" />
        <asp:RequiredFieldValidator runat="server" ID="reqPart2" ControlToValidate="tbPart2" EnableClientScript="false" Display="None"/>
        <asp:RegularExpressionValidator runat="server" ID="formatPart2" ControlToValidate="tbPart2" ValidationExpression="^\d{4}$" EnableClientScript="false" Display="None"/>
    </td>
    <td class="erreur">
        <asp:CustomValidator runat="server" ID="reqTel" OnServerValidate="validerObligatoire" ErrorMessage="Ce champ est obligatoire" Visible="false" Display="Dynamic"/>
        <asp:CustomValidator runat="server" OnServerValidate="validerTelephone" ErrorMessage="Le format du numéro de téléphone est incorrect" Display="Dynamic" />
    </td>
</tr>
