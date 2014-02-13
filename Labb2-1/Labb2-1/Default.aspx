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
        <!--bekräftelseruta-->
        <asp:Panel ID="pnlConfirmBox" runat="server" Visible="False">
             <p>Uppladdning av filen lyckades.</p>
        </asp:Panel>
        <!--huvudområdet där den stora bilden visas-->
        <div id="imageBox">
            <asp:Image ID="imgFull" runat="server" Visible="False" />
        </div>
        <!--området där miniatyrbilderna visas-->
        <div id="thumbImageBox">
            
            <!--hämtar ImageInfo-listan för bildfilerna i bildkatalogen
                genom anrop av metoden Repeater_GetData-->
            <asp:Repeater ID="rptImageBox" runat="server" 
                ItemType="Labb2_1.Model.ImageInfo"
                SelectMethod="Repeater_GetData">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:HyperLink ID="lbThumbImage" runat="server"
                            NavigateUrl='<%# String.Format("Default.aspx?image={0}", 
                                Server.UrlEncode(Item.FileName)) %>'>
                        
                            <asp:Image ID="imgThumbImage" runat="server"
                                ImageUrl='<%# "~/Uploads/Thumbs/" + Item.ThumbName %>' />
                        </asp:HyperLink>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <!--Uppladdningsrutan-->
        <asp:Panel ID="pnlUpload" runat="server" GroupingText="Ladda upp bild">
            <!--rutan som visas om fil inte valts eller om den har felaktig filändelse-->
            <asp:Panel ID="pnlErrorBox" runat="server">
                <asp:ValidationSummary ID="vsErrors" runat="server" HeaderText="Valideringen misslyckades. Åtgärda felen och försök igen." />
            </asp:Panel>
            <div>
                <asp:FileUpload ID="fuChooseFile" runat="server" ViewStateMode="Enabled"/>
                <asp:Button ID="btnUpload" runat="server" Text="Ladda upp" OnClick="btnUpload_Click" />
                <asp:RequiredFieldValidator ID="rfvChooseFile" runat="server" ErrorMessage="Ingen fil har valts" Text="*" ControlToValidate="fuChooseFile"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revChooseFile" runat="server" ErrorMessage="Filformatet stöds ej" Text="*" ControlToValidate="fuChooseFile" ValidationExpression=".+\.(?:jpg|gif|png)$"></asp:RegularExpressionValidator>
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
