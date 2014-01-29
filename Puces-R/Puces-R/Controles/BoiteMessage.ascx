<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoiteMessage.ascx.cs"
    Inherits="Puces_R.BoiteMessageControle" %>

<div runat="server" ID="divBoite" class="rectangleComplet rectangleItem" style="float: left;" visible="false">
    <table class="sBoite">
        <thead>
            <tr>
                <th class="sCheckbox">
                <asp:CheckBox runat="server" ID="cbAll" OnClick="checkAll(this)" />
                </th>
                <th class="sDe">
                    <asp:Label runat="server" ID="lblLabel">De</asp:Label>
                </th>
                <th class="sSujet">
                    Sujet
                </th>
                <th class="sDate">
                    Date
                </th>
            </tr>
        </thead>
        <tbody runat="server" id="Liste">
        </tbody>
    </table>
</div>
