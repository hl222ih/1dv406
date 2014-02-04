<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Labb1_3.Error"  ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>1DV406 - hl222ih</title>
    <link rel="stylesheet" href="~/Content/Site.css"/>
</head>
<body>
    <form id="ErrorForm" runat="server">
        <header>
        <h1>
            <asp:Label ID="lblHeader" runat="server" Text="Konvertera temperaturer"></asp:Label>
        </h1>
        </header>
        <div id="content">
            <h2>
                Serverfel
            </h2>
            <p>
                Ett oväntat fel inträffade. Förfrågan kunde inte behandlas.
            </p>
            <p>
                Tillbaka till <a href="Default.aspx">Startsidan</a>.
            </p>
        </div>
    </form>
</body>
</html>
