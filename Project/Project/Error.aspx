<%@ Page Title="Error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Project.Error" ViewStateMode="Disabled" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="Content">

    <asp:Panel ID="pnlErrorPage" runat="server">
        <h1>Serverfel</h1>
        <p>Ett oidentifierat undantagsfel har inträffat.</p>
        <p>Försök igen om en stund.</p>
        <p>Om felet kvarstår, vänligen kontakta mig gärna och berätta när och hur felet uppstod.</p>
    </asp:Panel>
</asp:Content>
