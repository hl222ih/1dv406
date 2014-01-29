<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="labb1._1.Default" ViewStateMode="Disabled"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>1DV406 - hl222ih</title>
    <link rel="stylesheet" href="~/Content/Site.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <header>
        <h1>
            <asp:Label ID="lblHeader" runat="server" Text="Hur många versaler?"></asp:Label>
        </h1>
    </header>
    <div id="content">
        <div>
            <asp:TextBox ID="txtInput" runat="server" TextMode="MultiLine" Rows="8" Columns="80"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnCountOrReset" runat="server" Text="Bestäm antalet versaler" OnClick="btnCountOrReset_Click" ViewStateMode="Inherit" />
            <asp:Label ID="lblCountInfo" runat="server" ViewStateMode="Enabled" Visible="False"> </asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
