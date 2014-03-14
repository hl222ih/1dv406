<%@ Page Title="BlissKom" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Project._Default" ViewStateMode="Disabled" %>

<%-- attributions
    New Moon http://findicons.com/icon/229953/emblem_ok    ok.png  (GNU/GPL)
    New Moon http://findicons.com/icon/229887/gnome_status   info.png (GNU/GPL)
    Gnome Icon Artists http://findicons.com/icon/67135/gnome_go_home   info.png (GNU/GPL)
    Asher http://findicons.com/icon/64629/stop  close.png   (Creative Commons ShareAlike)
    capital18 (Jugal Paryani) http://findicons.com/icon/13745/forward  right.png (freeware)
    capital18 (Jugal Paryani) http://findicons.com/icon/13715/back  left.png (freeware) --%>


<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="Content">

    <%-- DropDownList för ordtyper. AutoPostBack för att SelectedIndexChanged ska köras.
        Renderar färgerna vid varje PostBack eftersom ListItems inte har någon ViewStateMode att aktivera.
        DropDownList toppfärg samma som vald ListItem. Färgerna laddas vid DataBound och vid PostBack.
        DropDownListans värden binds dock bara en gång, ViewStateMode behöver inte vara aktiverad. --%>
    <asp:DropDownList ID="ddlPageWordType" runat="server"      
        DataValueField="WTypeId" ItemType="Project.PageModel.PageWordType"
        DataTextField="WType" OnDataBound="ddlPageWordType_DataBound"
        SelectMethod="GetPageWordTypeData">
    </asp:DropDownList>                                                           

    <asp:Panel ID="pnlTablet" runat="server" BackImageUrl="~/Images/tablet-PD.svg" Height="600px" HorizontalAlign="Center" Width="900px">

        <asp:Panel ID="pnlInnerTablet" runat="server">
            <%-- Navigeringsknappar --%>
            <asp:ImageButton ID="imbOK" runat="server" ImageUrl="~/Images/ok.png" CssClass="navImbs" />
            <asp:ImageButton ID="imbCancel" runat="server" ImageUrl="~/Images/close.png" CssClass="navImbs" />
            <asp:ImageButton ID="imbLeft" runat="server" ImageUrl="~/Images/left.png" CssClass="navImbs" />
            <asp:ImageButton ID="imbRight" runat="server" ImageUrl="~/Images/right.png" CssClass="navImbs" />
            <asp:ImageButton ID="imbHome" runat="server" ImageUrl="~/Images/home.png" CssClass="navImbs" />
            <asp:ImageButton ID="imbInfo" runat="server" ImageUrl="~/Images/info.png" CssClass="navImbs" />
            <%-- Platshållare för items, alltså bilderna på "kartan".--%>
            <asp:PlaceHolder ID="phItems" runat="server" />
        </asp:Panel>
        
    </asp:Panel>
</asp:Content>
