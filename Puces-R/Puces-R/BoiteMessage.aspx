<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoiteMessage.aspx.cs" Inherits="Puces_R.BoiteMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        
    .sBoite 
    {
        border-collapse:collapse;
    }
    .sBoite td, .sBoite th
    {
        height: 30px;
        text-align:left;
        border: solid 1px black;
    }
    
    .sBoite .sCheckbox 
    {
        width: 30px;
    }
    
    .sBoite .sDe 
    {
        width: 250px;
    }
    
    .sBoite .sSujet 
    {
        width: 800px;
    }
    
    .sBoite .sDate 
    {
        width: 200px;   
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table class="sBoite">
        <thead>
            <tr>
                <th class="sCheckbox">
                </th>
                <th class="sDe">
                    De
                </th>
                <th class="sSujet">
                    Sujet
                </th>
                <th class="sDate">
                    Date
                </th>
            </tr>
        </thead>
        <tbody runat="server" id="ListeMessage">
        </tbody>
    </table>
    </form>
</body>
</html>
