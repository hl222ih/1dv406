<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Labb1_4.Default"  ViewStateMode="Disabled" %>

<!DOCTYPE html>

<!-- attributions:
prevguess.png:
CC BY-NC-ND 3.0 Tatice http://www.iconarchive.com/artist/tatice.html
correct.png, higher.png, lower.png:
CC BY-NC-SA 3.0 Ahmad Hania http://www.iconarchive.com/artist/ahmadhania.html
-->

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>1DV406 - hl222ih</title>
    <link rel="stylesheet" href="~/Content/Site.css"/>
<!-- jquery-test
    <script src="Scripts/jquery-2.1.0.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#infoBox").slideDown();
        });
    </script> -->
</head>
<body>
    <form id="MainForm" runat="server" defaultbutton="btnGuess" defaultfocus="txtGuess">
        <header>
            <h1>
                <asp:Label ID="lblHeader" runat="server" Text="Gissa det hemliga talet"></asp:Label>
            </h1>
        </header>
        <div id="content">
            <div id="infoBox">
                <div> <!-- gissningsraden -->
                    <asp:Label ID="lblGuess" runat="server" Text="Ange ett tal mellan 1 och 100:"></asp:Label>
                    <asp:TextBox ID="txtGuess" runat="server" AutoCompleteType="Disabled" MaxLength="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvGuess" runat="server" ErrorMessage="Fältet får inte vara tomt" ControlToValidate="txtGuess" Text="*"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rvGuess" runat="server" ErrorMessage="Talet måste vara ett heltal inom det slutna intervallet 1 och 100" ControlToValidate="txtGuess" MinimumValue="1" MaximumValue="100" Type="Integer" Text="*"></asp:RangeValidator>
                    <asp:Button ID="btnGuess" runat="server" Text="Gissa" OnClick="btnGuess_Click" />
                </div>
                <div> <!-- tidigare gissnignar -->
                    <asp:Label ID="lblPreviousGuesses" runat="server" Text="Dina tidigare gissningar: {0}." Visible="False"></asp:Label>
                </div>
                <div> <!-- svarsresultat på gissningen -->
                <asp:PlaceHolder ID="phGuessResult" runat="server" Visible="False"></asp:PlaceHolder>
                </div>
                <div> <!-- om antal gissningar uppgått till 7 -->
                    <asp:Label ID="lblFailed" runat="server" Text="Du har tyvärr förbrukat dina 7 gissningar. Det hemliga talet var {0}." Visible="False"></asp:Label>
                </div>
                <div> <!-- om samma gissning gjorts tidigare -->
                    <asp:Label ID="lblPreviousGuess" runat="server" Text="Du har redan gissat på {0}. Försök igen!" Visible="False"></asp:Label>
                </div>
                <div> <!-- om gissningen är korrekt -->
                    <asp:Label ID="lblEndNumberOfGuesses" runat="server" Text="Grattis! Du klarade det på {0} gissningar!" Visible="False"></asp:Label>
                </div>
                <div> <!-- generera nytt hemligt nummer -->
                    <asp:Button ID="btnNewNumber" runat="server" Text="Slumpa nytt hemligt tal" CausesValidation="False" OnClick="btnNewNumber_Click" />
                </div>
            </div>
            <!-- visa valideringsfel -->
            <asp:ValidationSummary ID="vsErrors" runat="server" HeaderText="Valideringen misslyckades. Åtgärda felen och försök igen." />
        </div>
    </form>
</body>
</html>
