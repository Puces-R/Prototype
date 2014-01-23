<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LigneMessage.ascx.cs"
    Inherits="Puces_R.LigneMessage" ClassName="LigneMessage" %>
<tr runat="server" id="trLu">
    <td>
        <asp:CheckBox ID="cb" runat="server" />
    </td>
    <td>
        <asp:Label runat="server" ID="lblFrom">De</asp:Label>
    </td>
    <td>
        <asp:LinkButton runat="server" OnClick="voirMessage" >
            <asp:Label runat="server" ID="lblSubject">Sujet</asp:Label>
        </asp:LinkButton>
    </td>
    <td>
        <asp:Label runat="server" ID="lblDate">Date</asp:Label>
    </td>
</tr>
