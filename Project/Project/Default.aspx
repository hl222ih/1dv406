<%@ Page Title="BlissKom" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Project._Default" ViewStateMode="Disabled" %>

<%-- attributions
    New Moon http://findicons.com/icon/229953/emblem_ok    ok.png  (GNU/GPL)
    New Moon http://findicons.com/icon/229887/gnome_status   info.png (GNU/GPL)
    Gnome Icon Artists http://findicons.com/icon/67135/gnome_go_home   info.png (GNU/GPL)
    Asher http://findicons.com/icon/64629/stop  close.png   (Creative Commons ShareAlike)
    capital18 (Jugal Paryani) http://findicons.com/icon/13745/forward  right.png (freeware)
    capital18 (Jugal Paryani) http://findicons.com/icon/13715/back  left.png (freeware) 
    Oxygen Team http://findicons.com/icon/238341/preferences_system settings.png (GNU/GPL)
    Blissymboler tillhörande Blissymbolics Communication International, 
        Copyright för den kompletta samlingen, men fritt att använda delar av vokabuläret eller
        hela vokabuläret för enskilt bruk eller för icke-kommersiella ändamål (Creative Commons ShareAlike).
        http://www.blissymbolics.org/
    Teckenillustrationer tillhörande Specialpedagogiska Skolmyndigheten, Copyright, med tillåtelse. 
        http://www.ritadetecken.se/ --%>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="Content">
    <%-- Symboliserar plattan --%>
    <asp:Panel ID="pnlTablet" runat="server" BackImageUrl="~/Images/tablet-PD.svg" Height="600px"
         HorizontalAlign="Center" Width="900px" ViewStateMode="Disabled">

        <%-- Symboliserar plattans skärm --%>
        <asp:Panel ID="pnlInnerTablet" runat="server">

            <%-- Felruta (visas när något går fel eller inte kan genomföras) --%>
            <asp:Panel ID="pnlErrorBox" runat="server" CssClass="boxes">
                <asp:Image ID="imgFailed" runat="server" ImageUrl="~/Images/failed.png" CssClass="boximages" />
                <asp:ValidationSummary ID="vsErrorsMeaning" runat="server" ValidationGroup="MeaningGroup" />
                <asp:ValidationSummary ID="vsErrorsItem" runat="server" ValidationGroup="ItemGroup" />
                <asp:Button ID="btnOKError" runat="server" Text="OK" CausesValidation="False"  
                    OnClientClick="BlissKom.hideControl('Content_pnlErrorBox'); return false;" UseSubmitBehavior="False"
                    CssClass="okbutton" />
            </asp:Panel>

            <%-- Bekräftelseruta (visas när användaren ombeds bekräfta att en betydelse eller bild med position etc ska tas bort) --%>
            <asp:Panel ID="pnlConfirmBox" runat="server" CssClass="boxes">
                <asp:Image ID="imgWarning" runat="server" 
                    ImageUrl="~/Images/warning.png" CssClass="boximages" />
                <asp:Button ID="btnCancelConfirm" runat="server" Text="Avbryt" CausesValidation="False"  
                    OnClientClick="BlissKom.hideControl('Content_pnlConfirmBox'); return false;" UseSubmitBehavior="False"
                    CssClass="cancelbutton" />
                <asp:Button ID="btnOKConfirmDeleteMeaning" runat="server" Text="OK" CausesValidation="False"  
                    OnClientClick="BlissKom.confirmDeleteMeaning(); return false;" UseSubmitBehavior="False"
                    CssClass="okbutton" />
                <asp:Button ID="btnOKConfirmDeleteItem" runat="server" Text="OK" CausesValidation="False"  
                    OnClientClick="BlissKom.confirmDeleteItem(); return false;" UseSubmitBehavior="False"
                    CssClass="okbutton" />
                <asp:Label ID="lblConfirm" runat="server"></asp:Label>
            </asp:Panel>

            <%-- "Lyckad operation"-ruta (visas när en betydelse har uppdaterats eller när en bild med position etc framgångsrikt lagts till eller uppdaterats) --%>
            <asp:Panel ID="pnlSuccessBox" runat="server" CssClass="boxes">
                <asp:Image ID="imgSuccess" runat="server"
                    ImageUrl="~/Images/success.png" CssClass="boximages" />
                <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                <asp:Button ID="btnOKSuccess" runat="server" Text="OK" CausesValidation="False"  
                    OnClientClick="BlissKom.hideControl('Content_pnlSuccessBox'); return false;" UseSubmitBehavior="False"
                    CssClass="okbutton" />
            </asp:Panel>

            <%-- Navigeringsknappar --%>
            <asp:ImageButton ID="imbOK" runat="server" ImageUrl="~/Images/ok.png" CssClass="navImbs" CausesValidation="False" />
            <asp:ImageButton ID="imbCancel" runat="server" ImageUrl="~/Images/close.png" CssClass="navImbs" CausesValidation="False" OnClientClick="return false;" />
            <asp:ImageButton ID="imbLeft" runat="server" ImageUrl="~/Images/left.png" CssClass="navImbs" CausesValidation="False" OnClientClick="return false;" />
            <asp:ImageButton ID="imbRight" runat="server" ImageUrl="~/Images/right.png" CssClass="navImbs" CausesValidation="False" OnClientClick="return false;" />
            <asp:ImageButton ID="imbHome" runat="server" ImageUrl="~/Images/home.png" CssClass="navImbs" CausesValidation="False" />
            <asp:ImageButton ID="imbInfo" runat="server" ImageUrl="~/Images/info.png" CssClass="navImbs" CausesValidation="False" OnClientClick="return false;" />
            <asp:ImageButton ID="imbSettings" runat="server" ImageUrl="~/Images/settings.png" CssClass="navImbs" CausesValidation="false" OnClick="imbSettings_Click" />
            <asp:ImageButton ID="imbHome2" runat="server" ImageUrl="~/Images/home.png" CssClass="navImbs" CausesValidation="False" OnClick="imbHome_Click" />
            
            <%-- Platshållare för items, alltså bilderna på "kartan".--%>
            <asp:PlaceHolder ID="phItems" runat="server" />
            
            <%-- Formulär för kommunikation med databasen --%>
            <asp:Panel ID="pnlForm" runat="server">

                <%-- Lista med alla betydelser. Representeras av ordet för betydelsen. --%>
                <asp:Label ID="lblMeaning" runat="server" Text="Betydelse"></asp:Label>
                <asp:ListBox ID="lstMeaning" runat="server"
                    DataValueField="MeaningId" ItemType="Project.Model.Meaning"
                    DataTextField="Word" SelectMethod="GetMeaningData" 
                    AutoPostBack="True" OnSelectedIndexChanged="lstMeaning_SelectedIndexChanged" >
                </asp:ListBox>

                <%-- DropDown-lista för ordtyper. AutoPostBack för att SelectedIndexChanged ska köras.
                Renderar färgerna vid varje PostBack eftersom ListItems inte har någon ViewStateMode att aktivera.
                Färgerna laddas vid DataBound och vid PostBack.
                DropDownListans värden binds dock bara en gång, ViewStateMode behöver inte vara aktiverad. --%>
                <asp:Label ID="lblWordType" runat="server" Text="Ordtyp"></asp:Label>                                 
                <asp:DropDownList ID="ddlPageWordType" runat="server"      
                    DataValueField="WTypeId" ItemType="Project.PageModel.PageWordType"
                    DataTextField="WType" OnDataBound="ddlPageWordType_DataBound"
                    SelectMethod="GetPageWordTypeData">
                </asp:DropDownList>

                <%-- Ordet för betydelsen. --%>
                <asp:Label ID="lblWord" runat="server" Text="Ord"></asp:Label>
                <asp:TextBox ID="txtWord" runat="server" MaxLength="30" AutoCompleteType="None"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvWord" runat="server" 
                     ErrorMessage="Fältet för 'Ord' får inte vara tomt."
                     ControlToValidate="txtWord" Display="None" ValidationGroup="MeaningGroup"></asp:RequiredFieldValidator>  
                
                <%-- Valfri kommentar för betydelsen. --%>
                <asp:Label ID="lblWordComment" runat="server" Text="Kommentar"></asp:Label>
                <asp:TextBox ID="txtWordComment" runat="server" MaxLength="100"></asp:TextBox>
                <asp:Label ID="lblItem" runat="server" Text="Vald betydelses bildfiler"></asp:Label>                                 
                
                <%-- Knapp för att lägga till eller uppdatera en betydelse --%>
                <%-- Om en betydelse är vald uppdateras betydelsen, annars skapas en ny --%>
                <asp:Button ID="btnUpdateMeaning" runat="server" Text="Spara" OnClick="btnUpdateMeaning_Click" 
                    OnClientClick="BlissKom.removeDisplayNoneIfNotValid();" ValidationGroup="MeaningGroup"/>

                <%-- Knapp för att rensa vald betydelse så att eget formulärdata kan läggas till. --%>
                <asp:Button ID="btnAddNewMeaning" runat="server" Text="Skapa ny" OnClick="btnAddNewMeaning_Click" CausesValidation="false" />
                
                <%-- Knapp för att radera en betydelse (inklusive länkade bildfiler med positionsangivelser osv.) --%>
                <%-- returnerar false och orsakar därför inte postback förrän användaren klickat på "ok"-knappen i pnlConfirmBox --%>
                <asp:Button ID="btnDeleteMeaning" runat="server" Text="Radera" OnClick="btnDeleteMeaning_Click" 
                    OnClientClick="return BlissKom.deleteMeaningIfConfirmed('Är du säker på att du vill radera betydelsen inklusive tillhörande bildfiler?');" 
                    CausesValidation="False"/>
                <%-- Knapp för att återställa formuläret till ursprungsläget, som det var innan ev. ändringar gjorts. --%>
                <asp:Button ID="btnResetMeaning" runat="server" Text="Återställ" OnClick="btnResetMeaning_Click" 
                    CausesValidation="False" />

                <%-- Horisontell avgränsare. Symboliserar skillnaden mellan betydelsen och dess tillhörande "items" (bildfiler med positionsangivelser osv.) --%>
                <asp:Panel ID="pnlHorizontalRule" runat="server"></asp:Panel>

                <%-- Lista med "items", representerad av de tillhörande bildfilernas namn. --%>
                <asp:ListBox ID="lstItem" runat="server"
                    DataValueField="Key" DataTextField="Value" AutoPostBack="True" 
                    OnSelectedIndexChanged="lstItem_SelectedIndexChanged" SelectMethod="GetImageFileNameDataOfPage" >
                    <asp:ListItem Value="" Text="" Enabled="false" />
                </asp:ListBox>

                <%-- OK! Lite roligt med dropdownlistor som går utanför "surfplatteskärmen"! Men... Det får vara så. --%>
                <%-- DropDown-lista med bildens position. Inte riktigt användarvänliga namn, men det blev så i den här applikationen. --%>
                <%-- Parent x. Parent betyder att det är den bilden som kommer att visas. (Finns flera parents, visas inget). --%>
                <%-- x är positionen på sidan. Rutan längst upp till vänster är position 1 osv. --%>
                <%-- LeftChild x. LeftChild betyder att när en bild väl är förstorad, så kan man navigera till vänster för att se denna bild. --%>
                <%-- Om det finns två LeftChild-bilder så visas först LeftChild 1 och sen vid vidare navigering åt vänster visas LeftChild 2 osv. --%>
                <%-- Om LeftChild-bild saknas så visas heller ingen navigeringspil. --%>
                <%-- RightChild x. Samma som LeftChild men åt andra hållet. --%>
                <asp:DropDownList ID="ddlPosition" runat="server"
                    DataValueField="Key" DataTextField="Value" SelectMethod="GetPositionData" OnDataBound="ddlPosition_DataBound">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvPosition" runat="server" 
                    ErrorMessage="En position måste anges."
                    ControlToValidate="ddlPosition" Display="None" ValidationGroup="ItemGroup" />

                <%-- DropDown-lista för vilken kategori bilden ska tillhöra (vilken sida bilden ska visas på). --%>
                <%-- Bör vara samma för samtliga bilder som tillhör vald betydelse. (ev. icke-optimalt databas-upplägg) --%>
                <asp:DropDownList ID="ddlCategory" runat="server"
                    DataValueField="Key" DataTextField="Value" SelectMethod="GetCategoryData" OnDataBound="ddlCategory_DataBound">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCategory" runat="server" 
                    ErrorMessage="En kategori måste anges."
                    ControlToValidate="ddlCategory" Display="None" ValidationGroup="ItemGroup" />
                
                <%-- Om bildfilen har Parent-position så kan den antingen ha en egen betydelse, eller symbolisera en kategori. --%>
                <%-- Om den symboliserar en kategori ska rutan vara ikryssad, och då länkas man till den kategori
                     som valts i DropDown-listan. 
                     Om en symboliserar en kategori utan har en vanlig betydelse, ska den inte vara ikryssad. --%>
                <asp:CheckBox ID="chkIsCategory" runat="server" AutoPostBack="True" />
                <asp:Label ID="lblIsCategory" runat="server" Text="Är en kategorilänk"></asp:Label>
                <asp:DropDownList ID="ddlCategoryLink" runat="server" Enabled="False"
                    DataValueField="Key" DataTextField="Value" SelectMethod="IsCategoryGetCategoryData" OnDataBound="ddlCategoryLink_DataBound">
                </asp:DropDownList>

                <%-- Knapp för att uppdatera en "item", dvs bild med position, kategori/inte kategori etc. --%>
                <asp:Button ID="btnUpdateItem" runat="server" Text="Spara" OnClick="btnUpdateItem_Click"
                    ValidationGroup="ItemGroup" OnClientClick="BlissKom.removeDisplayNoneIfNotValid();"/>
                <%-- Knapp för att avmarkera valda bildfiler så att ett nytt item (bildfil, position etc) ska kunna läggas till. --%>
                <asp:Button ID="btnAddNewItem" runat="server" Text="Skapa ny" OnClick="btnAddNewItem_Click" CausesValidation="false"/>
                <%-- Knapp för att radera markerad bild från vald betydelse. --%>
                <asp:Button ID="btnDeleteItem" runat="server" Text="Radera" OnClick="btnDeleteItem_Click" 
                    OnClientClick="return BlissKom.deleteItemIfConfirmed('Är du säker på att du vill ta bort bildfilen från betydelsen?');"
                    CausesValidation="False" />
                <%-- Knapp för att nollställa den undre delen av formuläret till ursprungsläget för valt item (dropdown-listan till vänster unders strecket).--%>
                <asp:Button ID="btnResetItem" runat="server" Text="Återställ" OnClick="btnResetItem_Click" CausesValidation="False" />
                
                <%-- Bild som visar vald bildfil i bildfilslistan lstFileName. --%>
                <asp:Image ID="imgImage" runat="server" />

                <%-- Lista med alla tillgängliga bildfilers filnamn. --%>
                <%-- "bliss-" är blissymboler. "rt-" är 'ritade tecken', alltså teckenillustrationer. --%>
                <%-- Jag har inte med några exempelfotografier i applikationen, men hade de funnits hade
                     de typiskt placerats med positionsangivelse RightChild. Syftet med fotografier
                     är inlärning av blisstecken. Syftet med teckenillustrationer är hjälp för personal
                     att teckna betydelser som ges av aktuell blissymbol. --%>
                <asp:ListBox ID="lstFileName" runat="server"
                    DataValueField="Key" DataTextField="Value" AutoPostBack="True"
                    SelectMethod="GetImageFileNameData" OnDataBound="lstFileName_DataBound" 
                    OnSelectedIndexChanged="lstFileName_SelectedIndexChanged">
                    <%-- Ett tomt listitem för att "ingen bild" ska kunna vara vald då inget "item" valts. --%>
                    <asp:ListItem Value="" Text="" Enabled="false" />
                </asp:ListBox>
                <asp:RequiredFieldValidator ID="rfvFileName" runat="server" 
                    ErrorMessage="En bildfil i listan över alla bildfiler måste väljas."
                    ControlToValidate="ddlPosition" Display="None" ValidationGroup="ItemGroup" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
