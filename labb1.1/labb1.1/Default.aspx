<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="labb1._1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>1DV406 - hl222ih</title>
    <link rel="stylesheet" href="~/Content/Site.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            <asp:Label ID="lblHeader" runat="server" Text="Hur många versaler?"></asp:Label>
        </h1>
        <p>
            <asp:TextBox ID="txtInput" runat="server" TextMode="MultiLine" Rows="8" Columns="80"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="lblInfo" runat="server" Text="Label"></asp:Label>
        </p>
        <p>
            <asp:Button ID="btnPost" runat="server" Text="Button" />
        </p>
    </div>
    </form>
</body>
</html>
