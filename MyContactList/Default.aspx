<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyContactList.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <%-- Default.aspx --%>
<div>
    <h3>Add a New Contact</h3>
    <table>
        <tr>
            <td>First Name:</td>
            <td><asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Last Name:</td>
            <td><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Phone Number:</td>
            <td><asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Email:</td>
            <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="btnSave" runat="server" Text="Save Contact" OnClick="btnSave_Click" /></td>
        </tr>
    </table>
    <br />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
    <hr />

    <h3>Saved Contacts</h3>
   <asp:GridView ID="gvContacts" runat="server" 
    AutoGenerateColumns="false"
    DataKeyNames="ContactID"
    OnRowEditing="gvContacts_RowEditing"
    OnRowUpdating="gvContacts_RowUpdating"
    OnRowCancelingEdit="gvContacts_RowCancelingEdit"
    OnRowDeleting="gvContacts_RowDeleting"> <%-- ADD THIS EVENT HANDLER --%>
    <Columns>
        <%-- Add ShowDeleteButton="true" to this line --%>
        <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />

        <%-- (The rest of your TemplateFields remain unchanged) --%>
        <asp:TemplateField HeaderText="First Name">
            <ItemTemplate>
                <asp:Label Text='<%# Eval("FirstName") %>' runat="server" /><br />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditFirstName" Text='<%# Bind("FirstName") %>' runat="server" />
            </EditItemTemplate>
        </asp:TemplateField>
       <asp:TemplateField HeaderText="Last Name">
         <ItemTemplate>
              <asp:Label Text='<%# Eval("LastName") %>' runat="server" /><br />
         </ItemTemplate>
         <EditItemTemplate>
             <asp:TextBox ID="txtEditLastName" Text='<%# Bind("LastName") %>' runat="server" />
         </EditItemTemplate>
     </asp:TemplateField>
          <asp:TemplateField HeaderText="Phone No.">
    <ItemTemplate>
         <asp:Label Text='<%# Eval("PhoneNumber") %>' runat="server" /><br />
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtEditPhoneNumber" Text='<%# Bind("PhoneNumber") %>' runat="server" />
    </EditItemTemplate>
</asp:TemplateField>
          <asp:TemplateField HeaderText="Email">
    <ItemTemplate>
         <asp:Label Text='<%# Eval("Email") %>' runat="server" />
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtEditEmail" Text='<%# Bind("Email") %>' runat="server" />
    </EditItemTemplate>
</asp:TemplateField>
        
        <%-- ... other TemplateFields ... --%>

    </Columns>
</asp:GridView>
</div>
    </main>
</asp:Content>
