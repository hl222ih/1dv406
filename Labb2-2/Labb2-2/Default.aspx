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
        <asp:Panel ID="pnlConfirmBox" runat="server" Visible="False">
         <%-- Här visas rättmeddelande --%>
            <div>
            </div>
        </asp:Panel>
         <%-- felmeddelande ruta för redigering av kontakter --%>
        <asp:Panel ID="pnlErrorBox1" runat="server" CssClass="pnlErrorBox">
             <%-- En lista med felmeddelanden visas --%>
            <asp:ValidationSummary ID="vsErrors1" runat="server" HeaderText="Fel inträffade." ValidationGroup="InsertGroup" ShowModelStateErrors="False" />
            <div>
            </div>
        </asp:Panel>
         <%-- felmeddelande ruta för redigering av kontakter --%>
        <asp:Panel ID="pnlErrorBox2" runat="server" CssClass="pnlErrorBox">
             <%-- En lista med felmeddelanden visas --%>
            <asp:ValidationSummary ID="vsErrors2" runat="server" HeaderText="Fel inträffade." ValidationGroup="EditGroup" ShowModelStateErrors="False" />
            <div>
            </div>
        </asp:Panel>
         <%-- Listan med kontakter och de metoder som körs --%>
        <asp:ListView ID="lvContact" runat="server" ItemType="Labb2_2.Model.Contact" 
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
                        <th class="emailcol">
                            E-post
                        </th>
                        <th class="buttoncol">

                        </th>
                    </tr>
                    <%-- Platshållare för nya rader --%>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                </table>
                 <%-- kontroll för att stega mellan olika sidor av kontakter --%>
                <asp:DataPager ID="dpContact" runat="server" PageSize="20">
                    <Fields>
                         <%-- till första sidan eller sidan innan aktuell sida --%>
                        <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowNextPageButton="False" />
                         <%-- sidnummer i anslutning av aktuell sida --%>
                        <asp:NumericPagerField CurrentPageLabelCssClass="numpagerfield" NumericButtonCssClass="numbutton" />
                         <%-- till sista sidan eller sidan efter aktuell sida --%>
                        <asp:NextPreviousPagerField ShowLastPageButton="True" ShowPreviousPageButton="False"/>
                    </Fields>
                </asp:DataPager>
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
                         <%-- länk för att redigera en kontakt --%>
                        <asp:LinkButton ID="lbtnEditContact" CssClass="button" CommandName="Edit" Text="Redigera" runat="server" CausesValidation="False"></asp:LinkButton>
                         <%-- länk för att ta bort en kontakt --%>
                        <asp:LinkButton ID="lbtnDeleteContact" CssClass="button" CommandName="Delete" Text="Ta bort" runat="server" CausesValidation="False"></asp:LinkButton>
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
                 <%-- Mall för att lägga till en kontakt --%>
                <asp:RequiredFieldValidator ID="rfvInsertFirstName" CssClass="validators" runat="server" ErrorMessage="Fältet för förnamn får inte vara tomt" Text="-" ValidationGroup="InsertGroup" ControlToValidate="lvInsertFirstName" Visible="True"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvInsertLastName" CssClass="validators" runat="server" ErrorMessage="Fältet för efternamn får inte vara tomt" Text="-" ValidationGroup="InsertGroup" ControlToValidate="lvInsertLastName" Visible="True"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvInsertEmailAddress" CssClass="validators" runat="server" ErrorMessage="Fältet för e-postadress får inte vara tomt" Text="-" ValidationGroup="InsertGroup" ControlToValidate="lvInsertEmailAddress" Visible="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revInsertEmailAddress" CssClass="validators" runat="server" ErrorMessage="Felaktig e-postadress angiven" Text="-" ValidationGroup="InsertGroup" ControlToValidate="lvInsertEmailAddress" Visible="True" ValidationExpression="^[a-z][a-z0-9_\-~]*(\.[a-z0-9_\-~]+)?@([a-z0-9_\-]+\.){1,2}[a-z]{2,6}$"></asp:RegularExpressionValidator>
                <tr>
                    <td>
                         <%-- BindItem för att binda fält till kontakt-objektet --%>
                        <asp:TextBox ID="lvInsertFirstName" Text='<%#: BindItem.FirstName %>' runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="lvInsertLastName" Text='<%#: BindItem.LastName %>' runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="lvInsertEmailAddress" CssClass="emailtextbox" Text='<%#: BindItem.EmailAddress %>' runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                         <%-- länk för att lägga till kontakten  --%>
                        <asp:LinkButton runat="server" CssClass="button" CommandName="Insert" Text="Lägg till" ValidationGroup="EditGroup"></asp:LinkButton>
                         <%-- länk för att avbryta tilläggande av kontakten --%>
                        <asp:LinkButton runat="server" CssClass="button" CommandName="Cancel" Text="Rensa" CausesValidation="False"></asp:LinkButton>
                    </td>
                </tr>
            </InsertItemTemplate>
            <EditItemTemplate>
                 <%-- Mall för att redigera en befintlig kontakt --%>
                <asp:RequiredFieldValidator ID="rfvEditFirstName" CssClass="validators" runat="server" ErrorMessage="Fältet för förnamn får inte vara tomt" Text="-" ValidationGroup="EditGroup" ControlToValidate="lvEditFirstName" Visible="True"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvEditLastName" CssClass="validators" runat="server" ErrorMessage="Fältet för efternamn får inte vara tomt" Text="-" ValidationGroup="EditGroup" ControlToValidate="lvEditLastName" Visible="True"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvEditEmailAddress" CssClass="validators" runat="server" ErrorMessage="Fältet för e-postadress får inte vara tomt" Text="-" ValidationGroup="EditGroup" ControlToValidate="lvEditEmailAddress" Visible="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEditEmailAddress" CssClass="validators" runat="server" ErrorMessage="Felaktig e-postadress angiven" Text="-" ValidationGroup="EditGroup" ControlToValidate="lvEditEmailAddress" Visible="True" ValidationExpression="^[a-z][a-z0-9_\-~]*(\.[a-z0-9_\-~]+)?@([a-z0-9_\-]+\.){1,2}[a-z]{2,6}$"></asp:RegularExpressionValidator>
                <tr>
                    <td>
                        <asp:TextBox ID="lvEditFirstName" Text='<%#: BindItem.FirstName %>' runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="lvEditLastName" Text='<%#: BindItem.LastName %>' runat="server" MaxLength="50"></asp:TextBox>
                        
                    </td>
                    <td>
                        <asp:TextBox ID="lvEditEmailAddress" Text='<%#: BindItem.EmailAddress %>' runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                         <%-- länk för att spara ändringar i kontakten --%>
                        <asp:LinkButton ID="lbtnUpdateContact" runat="server" CssClass="button" CommandName="Update" Text="Spara" ValidationGroup="EditGroup">Spara</asp:LinkButton>
                         <%-- länk för att avbryta ändringar i kontakten --%>
                        <asp:LinkButton ID="lbtnCancelUpdateContact" runat="server" CssClass="button" CommandName="Cancel" Text="Avbryt" CausesValidation="False">Avbryt</asp:LinkButton>
                    </td>
                </tr>
            </EditItemTemplate>
        </asp:ListView> 
    </div>
   </form>
</body>
</html>
