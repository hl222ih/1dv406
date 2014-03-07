<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Project._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
              
    <%-- DropDownList för ordtyper. AutoPostBack för att SelectedIndexChanged ska köras.
        Renderar färgerna vid varje PostBack eftersom ListItems inte har någon ViewStateMode att aktivera.
        DropDownList toppfärg samma som vald ListItem. Färgerna laddas vid DataBound och vid PostBack.
        DropDownListans värden binds dock bara en gång, ViewStateMode behöver inte vara aktiverad. --%>
    <asp:DropDownList ID="DropDownList1" runat="server"      
        DataValueField="WTypeId" ItemType="Project.PageModel.PageWordType"
        DataTextField="WType" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" 
        SelectMethod="GetPageWordTypeData" 
        OnDataBound="ddlPageWordType_DataBound" AutoPostBack="True">
    </asp:DropDownList>                                                           
              
    
  <%--  <asp:Repeater ID="rptItems" runat="server"
        ItemType=""
        >
        <ItemTemplate>
            <asp:ImageButton ID="myButton" runat="server" /> <!-- ImageUrl='<%# "~/Images/Blissymbols/" + Item.FileName %>' />
            <asp:ImageButton ID="ImageButton1" runat="server" CssClass="item" BackColor="#fde885" ImageUrl="~/Images/Blissymbols/God.svg" Height="200" Width="300" />
        </ItemTemplate>
    </asp:Repeater>           --%>
    <asp:ListView ID="ListView1" runat="server">
        <ItemTemplate>

        </ItemTemplate>
    </asp:ListView>
    
    <div runat="server" id="divControl" class="item">...</div>
    
    <asp:ImageButton ID="ImageButton1" runat="server" CssClass="item" BackColor="#fde885" ImageUrl="~/Images/Blissymbols/God.svg" Height="200" Width="300" />
    <asp:ImageButton ID="ImageButton2" runat="server" CssClass="item" BackColor="#f9c7af" ImageUrl="~/Images/Blissymbols/hjarta.svg" Height="200" Width="300" />
    <asp:ImageButton ID="ImageButton3" runat="server" CssClass="item" BackColor="#dce8b9" ImageUrl="~/Images/Blissymbols/hus.svg" Height="200" Width="300" />
    <asp:ImageButton ID="ImageButton4" runat="server" CssClass="item" BackColor="#d6ecf7" ImageUrl="~/Images/Blissymbols/sjo.svg" Height="200" Width="300" />
    <asp:ImageButton ID="ImageButton5" runat="server" CssClass="item" BackColor="#dad5d2" ImageUrl="~/Images/Blissymbols/sjukhus.svg" Height="200" Width="300" />
    <asp:ImageButton ID="ImageButton6" runat="server" CssClass="item" BackColor="#ffffff" ImageUrl="~/Images/Blissymbols/spegel.svg" Height="200" Width="300" />
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
