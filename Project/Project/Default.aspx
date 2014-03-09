<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Project._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <asp:Panel ID="pnlTablet" runat="server" BackImageUrl="~/Images/tablet-PD.svg" Height="600px" HorizontalAlign="Center" Width="900px">
    <%-- DropDownList för ordtyper. AutoPostBack för att SelectedIndexChanged ska köras.
        Renderar färgerna vid varje PostBack eftersom ListItems inte har någon ViewStateMode att aktivera.
        DropDownList toppfärg samma som vald ListItem. Färgerna laddas vid DataBound och vid PostBack.
        DropDownListans värden binds dock bara en gång, ViewStateMode behöver inte vara aktiverad. --%>
    <asp:DropDownList ID="ddlPageWordType" runat="server"      
        DataValueField="WTypeId" ItemType="Project.PageModel.PageWordType"
        DataTextField="WType"
        SelectMethod="GetPageWordTypeData">
    </asp:DropDownList>                                                           
              
    
  <%--  <asp:Repeater ID="rptItems" runat="server"
        ItemType=""
        >
        <ItemTemplate>
            <asp:ImageButton ID="myButton" runat="server" /> <!-- ImageUrl='<%# "~/Images/Blissymbols/" + Item.FileName %>' />
            <asp:ImageButton ID="ImageButton1" runat="server" CssClass="item" BackColor="#fde885" ImageUrl="~/Images/Blissymbols/God.svg" Height="200" Width="300" />
        </ItemTemplate>
    </asp:Repeater>       
    <asp:ListView ID="ListView1" runat="server">
        <ItemTemplate>

        </ItemTemplate>
    </asp:ListView>
    
    <div runat="server" id="divControl" class="item">...</div>
    <%-- Platshållare för items, alltså bilderna på "kartan".--%>
    <asp:Panel ID="pnlInnerTablet" runat="server">
    <asp:PlaceHolder ID="phItems" runat="server"></asp:PlaceHolder>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ImageButton ID="ImageButton1" runat="server" CssClass="item" BackColor="#fde885" ImageUrl="~/Images/Blissymbols/God.svg" Height="100" Width="150" OnClick="ImageButton1_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>

    <asp:ImageButton ID="ImageButton2" runat="server" CssClass="item" BackColor="#f9c7af" ImageUrl="~/Images/Blissymbols/hjarta.svg" Height="100" Width="150" />
    <asp:ImageButton ID="ImageButton3" runat="server" CssClass="item" BackColor="#dce8b9" ImageUrl="~/Images/Blissymbols/hus.svg" Height="100" Width="150" />
    <asp:ImageButton ID="ImageButton4" runat="server" CssClass="item" BackColor="#d6ecf7" ImageUrl="~/Images/Blissymbols/sjo.svg" Height="100" Width="150" />
    <asp:ImageButton ID="ImageButton5" runat="server" CssClass="item" BackColor="#dad5d2" ImageUrl="~/Images/Blissymbols/sjukhus.svg" Height="100" Width="150" />
    <asp:ImageButton ID="ImageButton6" runat="server" CssClass="item" BackColor="#ffffff" ImageUrl="~/Images/Blissymbols/spegel.svg" Height="100" Width="150" />
    </asp:Panel>
        
        <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>Modify this template to jump-start your ASP.NET application.</h2>
            </hgroup>
            <p>
                To learn more about ASP.NET, visit <a href="http://asp.net" title="ASP.NET Website">http://asp.net</a>.
                The page features <mark>videos, tutorials, and samples</mark> to help you get the most from ASP.NET.
                If you have any questions about ASP.NET visit
                <a href="http://forums.asp.net/18.aspx" title="ASP.NET Forum">our forums</a>.
            </p>
        </div>
    </section>
    </asp:Panel>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>We suggest the following:</h3>
    <ol class="round">
        <li class="one">
            <h5>Getting Started</h5>
            ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245146">Learn more…</a>
        </li>
        <li class="two">
            <h5>Add NuGet packages and jump-start your coding</h5>
            NuGet makes it easy to install and update free libraries and tools.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245147">Learn more…</a>
        </li>
        <li class="three">
            <h5>Find Web Hosting</h5>
            You can easily find a web hosting company that offers the right mix of features and price for your applications.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245143">Learn more…</a>
        </li>
    </ol>
</asp:Content>
