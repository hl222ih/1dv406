<%@ Page Title="BlissKom" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Project._Default" %>

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

            <asp:ImageButton ID="ImageButton2" runat="server" CssClass="item" BackColor="#f9c7af" ImageUrl="~/Images/Blissymbols/hjarta.svg"/>
            <asp:ImageButton ID="ImageButton3" runat="server" CssClass="item" BackColor="#dce8b9" ImageUrl="~/Images/Blissymbols/hus.svg" Height="100" Width="150" />
            <asp:ImageButton ID="ImageButton4" runat="server" CssClass="item" BackColor="#d6ecf7" ImageUrl="~/Images/Blissymbols/sjo.svg" Height="100" Width="150" />
            <asp:ImageButton ID="ImageButton5" runat="server" CssClass="item" BackColor="#dad5d2" ImageUrl="~/Images/Blissymbols/sjukhus.svg" Height="100" Width="150" />
            <asp:ImageButton ID="ImageButton6" runat="server" CssClass="item" BackColor="#ffffff" ImageUrl="~/Images/Blissymbols/spegel.svg" Height="100" Width="150" />
        </asp:Panel>
        
    </asp:Panel>
</asp:Content>
