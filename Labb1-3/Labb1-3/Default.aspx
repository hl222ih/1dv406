<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Labb1_3.WebForm1"  ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="sv">
<head runat="server">
    <title>1DV406 - hl222ih</title>
    <link rel="stylesheet" href="~/Content/Site.css"/>
</head>
<body>
    <form id="MainForm" runat="server" defaultbutton="btnConvert" defaultfocus="txtStart">
        <header>
            <h1>
                <asp:Label ID="lblHeader" runat="server" Text="Konvertera temperaturer"></asp:Label>
            </h1>
        </header>
        <div id="content">
            <div id="column1">
                <asp:ValidationSummary ID="vsErrors" runat="server" ForeColor="Red" HeaderText="Valideringen misslyckades. Åtgärda felen och försök igen." />
                <div>
                    <asp:Label ID="lblStart" runat="server" Text="Starttemperatur:"></asp:Label>
                    <asp:TextBox ID="txtStart" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvStart" runat="server" ErrorMessage="En starttemperatur måste anges" ControlToValidate="txtStart" Text="*" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cpvStartInteger" runat="server" ErrorMessage="Startvärdet måste vara ett heltal" ControlToValidate="txtStart" Type="Integer" Operator="DataTypeCheck" Text="*" ForeColor="Red">*</asp:CompareValidator>
                </div>
                <div>
                    <asp:Label ID="lblEnd" runat="server" Text="Sluttemperatur:"></asp:Label>
                    <asp:TextBox ID="txtEnd" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEnd" runat="server" ErrorMessage="En sluttemperatur måste anges" ControlToValidate="txtEnd" Text="*" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cpvEndInteger" runat="server" ErrorMessage="Slutvärdet måste vara ett heltal" ControlToValidate="txtEnd" Operator="DataTypeCheck" Type="Integer" Text="*" ForeColor="Red">*</asp:CompareValidator>
                    <asp:CompareValidator ID="cpvEndGreaterThanStart" runat="server" ErrorMessage="Slutvärdet måste vara större än startvärdet" ControlToValidate="txtEnd" Type="Integer" ControlToCompare="txtStart" Operator="GreaterThan" Text="*" ForeColor="Red">*</asp:CompareValidator>
                </div>
                <div>
                    <asp:Label ID="lblStep" runat="server" Text="Temperatursteg:"></asp:Label>
                    <asp:TextBox ID="txtStep" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvStep" runat="server" ErrorMessage="Ett stegvärde måste anges" ControlToValidate="txtStep" Text="*" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rvStep" runat="server" ErrorMessage="Stegvärdet måste vara ett heltal inom det slutna intervallet 1 till 100" Type="Integer" MinimumValue="1" MaximumValue="100" ControlToValidate="txtStep" Text="*" ForeColor="Red">*</asp:RangeValidator>
                </div>
                <div>
                    <asp:Label ID="lblConvertType" runat="server" Text="Typ av konvertering:"></asp:Label>
                    <asp:RadioButton ID="rdbC2F" runat="server" Checked="True" Text="Celcius till Fahrenheit" GroupName="rdbGroupDegreeUnit" /><br />
                    <asp:RadioButton ID="rdbF2C" runat="server" Text="Fahrenheit till Celcius" GroupName="rdbGroupDegreeUnit" />
                </div>
                <div>
                    <asp:Button ID="btnConvert" runat="server" Text="Konvertera" OnClick="btnConvert_Click" />
                </div>
            </div>
            <div id="column2">
                <asp:Table ID="tblResults" runat="server">
                    <asp:TableHeaderRow ID="tblhrResults" runat="server"></asp:TableHeaderRow>
                </asp:Table>
            </div>
        </div>
    </form>
</body>
</html>
