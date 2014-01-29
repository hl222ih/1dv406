<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Labb1_2.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>1DV406 - hl222ih</title>
    <link rel="stylesheet" href="~/Content/Site.css"/>
</head>
<body>
    <form id="MainForm" runat="server" defaultbutton="btnCalculateDiscount" defaultfocus="txtAmount">
        <header>
            <h1>
                <asp:Label ID="lblHeader" runat="server" Text="Kassakvitto"></asp:Label>
            </h1>
        </header>
        <div id="content">
            <div>
                <asp:Label ID="lblAmount" runat="server" Text="Total köpesumma:"></asp:Label>
            </div>
            <div>
                <asp:TextBox ID="txtAmount" runat="server" Columns="10" MaxLength="9" TextMode="SingleLine" placeholder="ange summa"></asp:TextBox>
                <asp:Label ID="lblCurrency" runat="server" Text=" kr"></asp:Label>
            </div>
            <div>
                <asp:Button ID="btnCalculateDiscount" runat="server" Text="Beräkna rabatt"/>
            </div>
            <div>
                <!--kvittot-->
            </div>
        </div>
    </form>
</body>
</html>
