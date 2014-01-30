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
                <asp:TextBox ID="txtAmount" runat="server" Columns="10" MaxLength="9" TextMode="SingleLine" placeholder="ange summa   " CssClass="numberTextBox"></asp:TextBox>
                <asp:Label ID="lblCurrency" runat="server" Text=" kr"></asp:Label>
                <div class="errorMessage">
                    <asp:RegularExpressionValidator ID="revAmount" runat="server" ErrorMessage="Felaktigt format." ControlToValidate="txtAmount" ValidationExpression="^\s*\d+(?:,\d+)?\s*$" CssClass="revAmount"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Rutan måste fyllas i." ControlToValidate="txtAmount" CssClass="rfvAmount"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div>
                <asp:Button ID="btnCalculateDiscount" runat="server" Text="Beräkna rabatt" OnClick="btnCalculateDiscount_Click"/>
            </div>
            <div class="reciept">
                <div class="logo">
                    <p>DeVe</p>
                </div>
                <div class="tagline">
                    <p>en del av EllenU</p>
                </div>
                <div class="address">
                    <p>Tel. 0772-28 80 00</p>
                    <p>Öppettider 8-17</p>
                    <p>EV. FEL GER UNDERKÄNT</p>
                </div>
                <div class="ruler">
                    <p>--------------------------------------------</p>
                </div>
                <div class="output">
                    <div>
                        <span>Totalt</span>
                    </div>
                    <div>
                        <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                        <span>kr</span>
                    </div>
                    <div>
                        <span>Rabattsats</span>
                    </div>
                    <div>
                        <asp:Label ID="lblDiscountRate" runat="server"></asp:Label>
                        <span>%</span>
                    </div>
                    <div>
                        <span>Rabatt</span>
                    </div>
                    <div>
                        <asp:Label ID="lblDiscount" runat="server"></asp:Label>
                        <span>kr</span>
                    </div>
                    <div>
                        <span>Att betala</span>
                    </div>
                    <div>
                        <asp:Label ID="lblTotalAmountToPay" runat="server"></asp:Label>
                        <span>kr</span>
                    </div>
                </div>
                <div class="ruler">
                    <p>--------------------------------------------</p>
                </div>
                <div class="regnr">
                    <p>ORG.NR 202100-6271</p>
                </div>
                <div class="endline">
                    <p>VÄLKOMMEN ÅTER!</p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
