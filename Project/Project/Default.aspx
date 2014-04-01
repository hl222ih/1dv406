<%@ Page Title="BlissKom" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Project._Default" ViewStateMode="Disabled" %>

<%-- attributions
    New Moon http://findicons.com/icon/229953/emblem_ok    ok.png  (GNU/GPL)
    New Moon http://findicons.com/icon/229887/gnome_status   info.png (GNU/GPL)
    Gnome Icon Artists http://findicons.com/icon/67135/gnome_go_home   info.png (GNU/GPL)
    Asher http://findicons.com/icon/64629/stop  close.png   (Creative Commons ShareAlike)
    capital18 (Jugal Paryani) http://findicons.com/icon/13745/forward  right.png (freeware)
    capital18 (Jugal Paryani) http://findicons.com/icon/13715/back  left.png (freeware) 
    Oxygen Team http://findicons.com/icon/238341/preferences_system settings.png (GNU/GPL) --%>


<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="Content">
    <asp:Panel ID="pnlTablet" runat="server" BackImageUrl="~/Images/tablet-PD.svg" Height="600px" HorizontalAlign="Center" Width="900px" ViewStateMode="Disabled">

        <asp:Panel ID="pnlInnerTablet" runat="server">
            <asp:Panel ID="pnlErrorBox" runat="server" CssClass="boxes">
                <asp:Image ID="imgFailed" runat="server" ImageUrl="~/Images/failed.png" CssClass="boximages" />
                <asp:ValidationSummary ID="vsErrorsMeaning" runat="server" ValidationGroup="MeaningGroup" />
                <asp:ValidationSummary ID="vsErrorsItem" runat="server" ValidationGroup="ItemGroup" />
                <asp:Button ID="btnOKError" runat="server" Text="OK" CausesValidation="False"  
                    OnClientClick="BlissKom.hideControl('Content_pnlErrorBox'); return false;" UseSubmitBehavior="False"
                    CssClass="okbutton" />
            </asp:Panel>
            <asp:Panel ID="pnlConfirmBox" runat="server" CssClass="boxes">
                <asp:Image ID="imgWarning" runat="server" 
                    ImageUrl="~/Images/warning.png" CssClass="boximages" />
                <asp:Button ID="btnCancelConfirm" runat="server" Text="Avbryt" CausesValidation="False"  
                    OnClientClick="BlissKom.hideControl('Content_pnlConfirmBox'); return false;" UseSubmitBehavior="False"
                    CssClass="cancelbutton" />
                <asp:Button ID="btnOKConfirm" runat="server" Text="OK" CausesValidation="False"  
                    OnClientClick="BlissKom.confirmDeleteMeaning(); return false;" UseSubmitBehavior="False"
                    CssClass="okbutton" />
                <asp:Label ID="lblConfirm" runat="server"></asp:Label>
            </asp:Panel>
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
                <%-- DropDownList för ordtyper. AutoPostBack för att SelectedIndexChanged ska köras.
                Renderar färgerna vid varje PostBack eftersom ListItems inte har någon ViewStateMode att aktivera.
                Färgerna laddas vid DataBound och vid PostBack.
                DropDownListans värden binds dock bara en gång, ViewStateMode behöver inte vara aktiverad. --%>
                <asp:Label ID="lblMeaning" runat="server" Text="Betydelse"></asp:Label>
                <asp:DropDownList ID="ddlPageWordType" runat="server"      
                    DataValueField="WTypeId" ItemType="Project.PageModel.PageWordType"
                    DataTextField="WType" OnDataBound="ddlPageWordType_DataBound"
                    SelectMethod="GetPageWordTypeData">
                </asp:DropDownList>
                <asp:Label ID="lblWordType" runat="server" Text="Ordtyp"></asp:Label>                                 
                <asp:ListBox ID="lstMeaning" runat="server"
                    DataValueField="MeaningId" ItemType="Project.Model.Meaning"
                    DataTextField="Word" SelectMethod="GetMeaningData" 
                    AutoPostBack="True" OnSelectedIndexChanged="lstMeaning_SelectedIndexChanged" >
                </asp:ListBox>
                <asp:RequiredFieldValidator ID="rfvLstMeaning" runat="server" 
                    ErrorMessage="En betydelse måste väljas."
                    ControlToValidate="lstMeaning" Display="None" ValidationGroup="MeaningGroup"></asp:RequiredFieldValidator>
                <asp:Label ID="lblWord" runat="server" Text="Ord"></asp:Label>
                <asp:TextBox ID="txtWord" runat="server" MaxLength="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTxtWord" runat="server" 
                     ErrorMessage="Fältet för 'Ord' får inte vara tomt."
                     ControlToValidate="txtWord" Display="None" ValidationGroup="MeaningGroup"></asp:RequiredFieldValidator>  
                              
                <asp:Label ID="lblWordComment" runat="server" Text="Kommentar"></asp:Label>
                <asp:TextBox ID="txtWordComment" runat="server" MaxLength="100"></asp:TextBox>
                <asp:Label ID="lblItem" runat="server" Text="Vald betydelses bildfiler"></asp:Label>                                 
                <asp:ListBox ID="lstItem" runat="server"
                    DataValueField="Key" DataTextField="Value" AutoPostBack="True" 
                    OnSelectedIndexChanged="lstItem_SelectedIndexChanged" SelectMethod="GetImageFileNameDataOfPage" >
                    <asp:ListItem Value="" Text="" Enabled="false" />
                </asp:ListBox>
                <asp:Button ID="btnUpdateMeaning" runat="server" Text="Uppdatera" OnClick="btnUpdateMeaning_Click" 
                    OnClientClick="BlissKom.removeDisplayNoneIfNotValid();" ValidationGroup="MeaningGroup"/>
                <asp:Button ID="btnAddNewMeaning" runat="server" Text="Skapa ny" OnClick="btnAddNewMeaning_Click" 
                    ValidationGroup="MeaningGroup" />
                <%-- returnerar false och orsaker därför inte postback förrän användaren klickat på
                    "ok"-knappen i pnlConfirmBox --%>
                <asp:Button ID="btnDeleteMeaning" runat="server" Text="Radera" OnClick="btnDeleteMeaning_Click" 
                    OnClientClick="return BlissKom.deleteMeaningIfConfirmed('Är du säker på att du vill radera betydelsen?');" 
                    CausesValidation="False"/>
                <asp:Button ID="btnResetMeaning" runat="server" Text="Återställ" OnClick="btnResetMeaning_Click" 
                    CausesValidation="False" />
                <asp:Panel ID="pnlHorizontalRule" runat="server"></asp:Panel>
                <%-- OK! Lite roligt med dropdownlistor som går utanför "surfplatteskärmen"! Men... Det får vara så. --%>
                <asp:DropDownList ID="ddlPosition" runat="server"
                    DataValueField="Key" DataTextField="Value" SelectMethod="GetPositionData" OnDataBound="ddlPosition_DataBound">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlCategory" runat="server"
                    DataValueField="Key" DataTextField="Value" SelectMethod="GetCategoryData" OnDataBound="ddlCategory_DataBound">
                </asp:DropDownList>
                <asp:CheckBox ID="chkIsCategory" runat="server" AutoPostBack="True" />
                <asp:Label ID="lblIsCategory" runat="server" Text="Är en kategorilänk"></asp:Label>
                <asp:DropDownList ID="ddlCategoryLink" runat="server" Enabled="False"
                    DataValueField="Key" DataTextField="Value" SelectMethod="IsCategoryGetCategoryData" OnDataBound="ddlCategoryLink_DataBound">
                </asp:DropDownList>
                <asp:Button ID="btnUpdateItem" runat="server" Text="Uppdatera" OnClick="btnUpdateItem_Click" ValidationGroup="ItemGroup" />
                <asp:Button ID="btnAddNewItem" runat="server" Text="Skapa ny" OnClick="btnAddNewItem_Click" ValidationGroup="ItemGroup" />
                <asp:Button ID="btnDeleteItem" runat="server" Text="Radera" OnClick="btnDeleteItem_Click" CausesValidation="False" />
                <asp:Button ID="btnResetItem" runat="server" Text="Återställ" OnClick="btnResetItem_Click" CausesValidation="False" />
                <asp:Image ID="imgImage" runat="server" />
                <asp:ListBox ID="lstFileName" runat="server"
                    DataValueField="Key" DataTextField="Value" AutoPostBack="True"
                    SelectMethod="GetImageFileNameData" OnDataBound="lstFileName_DataBound" 
                    OnSelectedIndexChanged="lstFileName_SelectedIndexChanged">
                    <asp:ListItem Value="" Text="" Enabled="false" />
                </asp:ListBox>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
