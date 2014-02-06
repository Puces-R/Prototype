<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChoixDestinataires.aspx.cs"
    Inherits="Puces_R.ChoixDestinataires" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="CSS/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        li
        {
            font-size: small;
        }
    </style>
    <script type="text/javascript" src="Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript">
        var arrDestinataires = new Array();
        var type;

        $.urlParam = function (name) {
            var results = new RegExp('[\\?&]' + name + '=([^&#]*)').exec(window.location.href);
            if (results == null) {
                return null;
            }
            else {
                return results[1] || 0;
            }
        }

        function autocomplete() {
            $.ajax({
                type: "POST",
                url: "ChoixDestinataires.aspx/GetResultats",
                data: '{name: "' + $("#<%=tbRecherche.ClientID%>")[0].value + '", id: "' + arrDestinataires + '", type: \'' + type + '\'}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: onSuccess,
                error: onError
            });
        }

        $(function () {
            var s = $.urlParam("Destinataire");
            if (s != null && s != 0) {
                var tmpArr = s.split(",");
                for (var i = 0; i < tmpArr.length; i++) {
                    arrDestinataires[i] = parseInt(tmpArr[i]);
                }
            }
            type = $.urlParam("Type");
            $('#<%=tbRecherche.ClientID%>').keyup(autocomplete);
            autocomplete();
        });

        function onSuccess(response) {
            lstVendeurs.innerHTML = response.d.split(";;;")[0];
            lstDestinataires.innerHTML = response.d.split(";;;")[1];
        }

        function onError() {
            alert("ERROR !");
        }

        function retour() {
            opener.postback(arrDestinataires.join(","));
            window.close();
        }

        function selectionner(id) {
            arrDestinataires.push(id);
            autocomplete()
        }

        function deselectionner(id) {
            arrDestinataires.splice(jQuery.inArray(id, arrDestinataires), 1);
            autocomplete();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:TextBox runat="server" ID="tbRecherche" />
            <asp:Button runat="server" ID="btnRecherche" CausesValidation="false" Text="Retourner"
                OnClientClick="retour();" />
        </div>
        <div class="panneau pnlGauche" style="height: 700px; width: 450px; overflow: scroll;">
            <ul runat="server" id="lstVendeurs" style="text-decoration: none;">
            </ul>
        </div>
        <div class="panneau" style="height: 700px; width: 450px; overflow: scroll;">
            <ul runat="server" id="lstDestinataires" style="text-decoration: none;">
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
