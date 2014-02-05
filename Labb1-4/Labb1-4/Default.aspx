<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Labb1_4.Default"  ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>1DV406 - hl222ih</title>
    <link rel="stylesheet" href="~/Content/Site.css"/>
</head>
<body>
    <form id="MainForm" runat="server">
        <header>
            <h1>
                <asp:Label ID="lblHeader" runat="server" Text="Gissa det hemliga talet"></asp:Label>
            </h1>
        </header>
        <div id="content">
            <asp:ValidationSummary ID="vsErrors" runat="server" ForeColor="Red" HeaderText="Valideringen misslyckades. Åtgärda felen och försök igen." />
            <div>
                <asp:Label ID="lblGuess" runat="server" Text="Ange ett tal mellan 1 och 100:"></asp:Label>
                <asp:TextBox ID="txtGuess" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvGuess" runat="server" ErrorMessage="Fältet får inte vara tomt" ControlToValidate="txtGuess" Text="*"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="rvGuess" runat="server" ErrorMessage="Talet måste vara ett heltal inom det slutna intervallet 1 och 100" ControlToValidate="txtGuess" MinimumValue="1" MaximumValue="100" Type="Integer" Text="*"></asp:RangeValidator>
                <asp:Button ID="btnGuess" runat="server" Text="Gissa" OnClick="btnGuess_Click" />
            </div>
            <div>
                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </div>
            <div>
                <asp:Button ID="btnNewNumber" runat="server" Text="Slumpa nytt hemligt tal" />
            </div>
            <div>
                <asp:Label ID="lblEndNumberOfGuesses" runat="server" Text="Grattis! Du klarade det på {0} gissningar!"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
