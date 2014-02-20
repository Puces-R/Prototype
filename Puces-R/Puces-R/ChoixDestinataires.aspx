<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChoixDestinataires.aspx.cs"
    Inherits="Puces_R.ChoixDestinataires" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Choix des destinataires</title>
    <link href="CSS/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        ul
        {
            font-size: small;
            list-style: none;
        }
        body 
        {
            min-width: 900px;
            background: white;
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
            var option = $("#<%#ddlFiltre.ClientID%>").val() == null ? -1 : $("#<%#ddlFiltre.ClientID%>").val();
            $.ajax({
                type: "POST",
                url: "ChoixDestinataires.aspx/GetResultats",
                data: '{name: "' + $("#<%#tbRecherche.ClientID%>")[0].value + '", id: "' + arrDestinataires + '", courant: <%#Session["ID"]%>, type: \'' + type + '\', option: ' + option + '}',
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
            $('#<%#tbRecherche.ClientID%>').keyup(autocomplete);
            $("#<%#ddlFiltre.ClientID%>").hide();
            $("#<%#ddlFiltre.ClientID%>").change(function () {
                autocomplete();
            });

            autocomplete();
        });

        function onSuccess(response) {
            lstVendeurs.innerHTML = response.d.split(";;;")[0];
            lstDestinataires.innerHTML = response.d.split(";;;")[1];
        }

        function onError() {
            alert("Erreur");
        }

        function retour() {
            opener.postback(arrDestinataires.join(","));
            window.close();
        }

        function selectionner(id) {
            arrDestinataires.push(id);
            autocomplete()
        }

        function changeType(id) {
            if (type != id) {
                var mySelect = $("#<%#ddlFiltre.ClientID%>");
                mySelect.show();
                mySelect.empty();
                var myOptions;

                switch (id) {
                    case 'GG':
                    case 'GC':
                    case 'GV':
                        myOptions = {
                            0: "Tous les gestionnaires"
                        };
                        break;
                    case 'VC':
                        myOptions = {
                            0: "Tous les vendeurs",
                            1: "Vendeurs dont je possède des paniers",
                            2: "Vendeurs desquels j'ai commandé"
                        }
                        break;
                    case 'CV':
                        myOptions = {
                            0: "Tous les clients",
                            1: "Clients qui ont des paniers",
                            2: "Clients qui ont déjà commandé"
                        };
                        break;
                    case 'CG':
                        myOptions = {
                            0: "Tous les clients"
                        };
                        break;
                    case 'VG':
                        myOptions = {
                            0: "Tous les vendeurs"
                        };
                }

                $.each(myOptions,
                    function (val, text) {
                        mySelect.append($('<option></option>').val(val).html(text));
                    }
                );
                mySelect.val(0);
                type = id;
                autocomplete();
            }
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
        <div class="panneau pnlGauche" style="height: 650px; width: 425px;">
            <div class="lignePointilleHorizontale" style="width: 100%;">
                <table style="margin: 0 auto;">
                    <tr>
                        <td>
                            Recherche&nbsp;<asp:TextBox runat="server" ID="tbRecherche" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Menu runat="server" ID="menuType" Orientation="Horizontal">
                            </asp:Menu>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlFiltre">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <ul runat="server" id="lstVendeurs" style="text-decoration: none; height: 550px;
                overflow: scroll;">
            </ul>
        </div>
        <div class="panneau pnlDroite" style="height: 650px; width: 425px;">
            <ul runat="server" id="lstDestinataires" style="text-decoration: none; height: 625px;
                overflow: scroll;">
            </ul>
            <div style="float: right; margin: 0 1em;">
                <asp:Button runat="server" ID="btnRecherche" CausesValidation="false" Text="Confirmer la sélection"
                    OnClientClick="retour();" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
