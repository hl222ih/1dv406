<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Labb2_2.Default" ViewStateMode="Disabled"%>

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
            <asp:Label ID="lblHeader" runat="server" Text="Äventyrliga kontakter"></asp:Label>
        </h1>
    </header>
    <div id="content">
        <%-- bekräftelseruta --%>
        <asp:Panel ID="pnlConfirmBox" runat="server" Visible="False" ViewStateMode="Enabled">
             <p>...</p>
        </asp:Panel>
        <asp:Panel ID="pnlErrorBox" runat="server">
            <asp:ValidationSummary ID="vsErrors" runat="server" HeaderText="Fel inträffade." />
        </asp:Panel>
        <asp:ListView ID="lvContact" runat="server" ItemType="Labb2_2.BLL.Contact" 
            SelectMethod="lvContact_GetData" 
            UpdateMethod="lvContact_UpdateItem" 
            InsertMethod="lvContact_InsertItem" 
            DeleteMethod="lvContact_DeleteItem" 
            DataKeyNames="ContactID" 
            InsertItemPosition="FirstItem">
            <LayoutTemplate>
                <table class="grid">
                    <tr>
                        <th>
                            Förnamn
                        </th>
                        <th>
                            Efternamn
                        </th>
                        <th>
                            E-post
                        </th>
                        <th>

                        </th>
                    </tr>
                    <%-- Platshållare för nya rader --%>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <%-- Mall för nya rader --%>
                <tr>
                    <td>
                        <%#: Item.FirstName %>
                    </td>
                    <td>
                        <%#: Item.LastName %>
                    </td>
                    <td>
                        <%#: Item.EmailAddress %>
                    </td>
                    <td class="command">
                        <asp:LinkButton CommandName="Delete" Text="Ta bort" runat="server" CausesValidation="False"></asp:LinkButton>
                        <asp:LinkButton CommandName="Edit" Text="Redigera" runat="server" CausesValidation="False"></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>                                                 
            <EmptyDataTemplate>
                <table class="grid">
                    <tr>
                         <td>
                             Kontaktuppgifter saknas.
                         </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <InsertItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="lvEditFirstName" Text='<%#: BindItem.FirstName %>' runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="lvEditLastName" Text='<%#: BindItem.LastName %>' runat="server"></asp:TextBox>
                        
                    </td>
                    <td>
                        <asp:TextBox ID="lvEditEmailAddress" Text='<%#: BindItem.EmailAddress %>' runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Insert" Text="Lägg till"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="False"></asp:LinkButton>
                    </td>
                </tr>
            </InsertItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="lvEditFirstName" Text='<%#: BindItem.FirstName %>' runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="lvEditLastName" Text='<%#: BindItem.LastName %>' runat="server"></asp:TextBox>
                        
                    </td>
                    <td>
                        <asp:TextBox ID="lvEditEmailAddress" Text='<%#: BindItem.EmailAddress %>' runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:LinkButton runat="server" CommandName="Update" Text="Spara">LinkButton</asp:LinkButton>
                        <asp:LinkButton runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="False">LinkButton</asp:LinkButton>
                    </td>
                </tr>
            </EditItemTemplate>
        </asp:ListView> 
    </div>
    </form>
</body>
</html>
