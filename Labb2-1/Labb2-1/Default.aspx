<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Labb2_1.Default" ViewStateMode="Disabled"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>1DV406 - hl222ih</title>
    <link rel="stylesheet" href="~/Content/Site.css" type="text/css" />
</head>
<body>
    <form id="MainForm" runat="server">
    <header>
        <h1>
            <asp:Label ID="lblHeader" runat="server" Text="Galleriet"></asp:Label>
        </h1>
    </header>
    <div id="content">
        <div id="confirmBox">

        </div>
        <div id="imageBox">

        </div>
        <div id="thumbImageBox">
            <asp:Repeater ID="rptImageBox" runat="server">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="hlThumbImage" runat="server">
                        <asp:Image ID="imgThumbImage" runat="server" />
                    </asp:HyperLink>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <asp:Panel ID="pnlUpload" runat="server" GroupingText="Ladda upp bild">
            <div id="errorBox">

            </div>
            <div>
                <asp:Button ID="btnChooseFile" runat="server" Text="Välj fil" />
                <asp:Label ID="lblChooseFile" runat="server" Text="Ingen fil har valts"></asp:Label>
                <asp:Button ID="btnUpload" runat="server" Text="Ladda upp" />
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
